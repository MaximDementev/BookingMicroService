using System;
using System.Collections.Generic;

namespace Contracts.Airlines_Integration.V1;

public enum ReservationStatus
{
    Confirmed,
    Rejected,
    Pending,
    Unknown
}

public sealed record ReservedPassengerDto(
    long BookingPassengerId,
    string ProviderPassengerId);

/// <summary>
/// Result of the supplier reservation attempt. Pending and Unknown are non-final outcomes.
/// </summary>
public sealed record ReserveBookingResponse(
    ReservationStatus Status,
    string? ProviderBookingId,
    string? BookingReference,
    DateTimeOffset? ExpiresAt,
    MoneyDto? TotalPrice,
    IReadOnlyList<ReservedPassengerDto> Passengers,
    string? ErrorCode);
