using Domain.Entities.Base;
using Domain.Enums;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt

namespace Domain.Entities;

/// <summary>Order owned by Booking. Authentication and the user profile belong to external services.</summary>
public sealed class Booking : Entity
{
    public long UserId { get; set; }
    public long OfferId { get; set; }
    public string SupplierId { get; set; } = string.Empty;
    public string ProviderOfferId { get; set; } = string.Empty;
    public string BookingCode { get; set; } = string.Empty;
    public string CreateIdempotencyKey { get; set; } = string.Empty;
    public BookingStatus BookingStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public IssuanceStatus IssuanceStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? ContactPhone { get; set; }
    public long? PaymentId { get; set; }
    public DateTime? OfferExpiresAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public long Version { get; set; }
}

/// <summary>Immutable after reservation: selected passenger and document snapshot, no UserService FK.</summary>
public sealed class BookingPassenger : Entity
{
    public long BookingId { get; set; }
    public long? SourcePassengerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string CitizenshipCode { get; set; } = string.Empty;
    public DocumentType DocumentType { get; set; }
    // The application must encrypt this value before persistence. EF does not encrypt it.
    public string DocumentNumberEncrypted { get; set; } = string.Empty;
    public string DocumentIssuingCountry { get; set; } = string.Empty;
    public DateOnly? DocumentExpiresAt { get; set; }
}

public sealed class BookingRoute : Entity
{
    public long BookingId { get; set; }
    public int SequenceNumber { get; set; }
}

/// <summary>Search offer snapshot, not a catalogue or local seat inventory.</summary>
public sealed class BookingFlight : Entity
{
    public long BookingId { get; set; }
    public long BookingRouteId { get; set; }
    public long? SourceFlightId { get; set; }
    public string ProviderSegmentId { get; set; } = string.Empty;
    public int SequenceNumber { get; set; }
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public string? DepartureTimeZone { get; set; }
    public string? ArrivalTimeZone { get; set; }
    public string AirlineCode { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string? AircraftCode { get; set; }
}

/// <summary>Intended purchase for a passenger and route; exists before any ticket is issued.</summary>
public sealed class BookingItem : Entity
{
    public long BookingId { get; set; }
    public long BookingPassengerId { get; set; }
    public long BookingRouteId { get; set; }
}

/// <summary>Persist Pending before a supplier call; Unknown must be reconciled before retrying.</summary>
public sealed class Reservation : Entity
{
    public long BookingId { get; set; }
    public string SupplierId { get; set; } = string.Empty;
    public string IdempotencyKey { get; set; } = string.Empty;
    public string? ProviderBookingId { get; set; }
    public string? ProviderBookingReference { get; set; }
    public ReservationStatus Status { get; set; }
    public DateTime? ReservedAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public decimal? TotalPriceAmount { get; set; }
    public string? TotalPriceCurrency { get; set; }
    public string? ErrorCode { get; set; }
    public long Version { get; set; }
}

/// <summary>Persist only tickets actually issued by the supplier, including partial issuance.</summary>
public sealed class ETicket : Entity
{
    public long BookingId { get; set; }
    public long BookingItemId { get; set; }
    public long BookingRouteId { get; set; }
    public long ReservationId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public string IssuerCode { get; set; } = string.Empty;
    public ETicketStatus Status { get; set; }
    public decimal PriceAmount { get; set; }
    public string PriceCurrency { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
}

/// <summary>Supplier passenger identity can differ between reservation attempts.</summary>
public sealed class ReservationPassenger : Entity
{
    public long BookingId { get; set; }
    public long ReservationId { get; set; }
    public long BookingPassengerId { get; set; }
    public string ProviderPassengerId { get; set; } = string.Empty;
}

/// <summary>Ticket coverage: references the same booking and route on both sides.</summary>
public sealed class ETicketSegment : Entity
{
    public long BookingId { get; set; }
    public long BookingRouteId { get; set; }
    public long ETicketId { get; set; }
    public long BookingFlightId { get; set; }
}

public sealed class InboxMessage : Entity
{
    public string Source { get; set; } = string.Empty;
    public string ExternalMessageId { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
}

/// <summary>Durable outgoing record; delivery worker is a separate application responsibility.</summary>
public sealed class OutboxMessage : Entity
{
    public long BookingId { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public string Payload { get; set; } = "{}";
    public DateTime OccurredAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int Attempts { get; set; }
    public string? LastError { get; set; }
}
