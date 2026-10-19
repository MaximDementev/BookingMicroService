using System.Text.RegularExpressions;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt

namespace Infrastructure.Persistence;

public static class BookingModelConfiguration
{
    public static void Configure(ModelBuilder model)
    {
        var booking = Table<Booking>(model, "bookings", "booking_id");
        booking.HasIndex(x => x.BookingCode).IsUnique();
        booking.HasIndex(x => new { x.UserId, x.CreateIdempotencyKey }).IsUnique();
        booking.HasAlternateKey(x => new { x.Id, x.SupplierId });
        booking.Property(x => x.Version).IsConcurrencyToken();
        booking.Property(x => x.BookingStatus).HasConversion<string>().HasMaxLength(32);
        booking.Property(x => x.PaymentStatus).HasConversion<string>().HasMaxLength(32);
        booking.Property(x => x.IssuanceStatus).HasConversion<string>().HasMaxLength(32);
        booking.Property(x => x.BookingCode).HasMaxLength(50).IsRequired();
        booking.Property(x => x.CreateIdempotencyKey).HasMaxLength(100).IsRequired();
        booking.Property(x => x.SupplierId).HasMaxLength(100).IsRequired();
        booking.Property(x => x.ProviderOfferId).HasMaxLength(255).IsRequired();
        booking.Property(x => x.ContactEmail).HasMaxLength(254).IsRequired();
        booking.Property(x => x.ContactPhone).HasMaxLength(32);
        booking.ToTable(t => {
            t.HasCheckConstraint("ck_booking_money", "total_amount >= 0 AND currency ~ '^[A-Z]{3}$'");
            t.HasCheckConstraint("ck_booking_owner", "user_id > 0 AND offer_id > 0");
            t.HasCheckConstraint("ck_booking_code", "length(btrim(booking_code)) > 0 AND length(btrim(create_idempotency_key)) > 0");
            t.HasCheckConstraint("ck_booking_status", "booking_status IN ('Draft','Pending','Paid','Completed','PartiallyCompleted','Expired','Cancelled','Refunded','RequiresReview')");
            t.HasCheckConstraint("ck_payment_status", "payment_status IN ('NotStarted','Pending','Succeeded','Failed','RefundPending','PartiallyRefunded','Refunded','Unknown')");
            t.HasCheckConstraint("ck_issuance_status", "issuance_status IN ('NotStarted','Pending','Issued','PartiallyIssued','Rejected','Unknown')");
        });

        var passenger = Table<BookingPassenger>(model, "booking_passengers", "booking_passenger_id");
        passenger.HasAlternateKey(x => new { x.Id, x.BookingId });
        passenger.HasOne<Booking>().WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
        passenger.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        passenger.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        passenger.Property(x => x.MiddleName).HasMaxLength(100);
        passenger.Property(x => x.Gender).HasConversion<string>().HasMaxLength(32);
        passenger.Property(x => x.DocumentType).HasConversion<string>().HasMaxLength(32);
        passenger.Property(x => x.CitizenshipCode).HasMaxLength(3).IsRequired();
        passenger.Property(x => x.DocumentIssuingCountry).HasMaxLength(3).IsRequired();
        passenger.Property(x => x.DocumentNumberEncrypted).IsRequired();

        var route = Table<BookingRoute>(model, "booking_routes", "booking_route_id");
        route.HasAlternateKey(x => new { x.Id, x.BookingId });
        route.HasIndex(x => new { x.BookingId, x.SequenceNumber }).IsUnique();
        route.HasOne<Booking>().WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
        route.ToTable(t => t.HasCheckConstraint("ck_route_sequence", "sequence_number > 0"));

        var flight = Table<BookingFlight>(model, "booking_flights", "booking_flight_id");
        flight.HasAlternateKey(x => new { x.Id, x.BookingId, x.BookingRouteId });
        flight.HasIndex(x => new { x.BookingRouteId, x.SequenceNumber }).IsUnique();
        flight.HasIndex(x => new { x.BookingId, x.ProviderSegmentId }).IsUnique();
        flight.HasOne<BookingRoute>().WithMany().HasForeignKey(x => new { x.BookingRouteId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);
        flight.Property(x => x.ProviderSegmentId).HasMaxLength(255).IsRequired();
        flight.Property(x => x.DepartureAirport).HasMaxLength(3).IsRequired();
        flight.Property(x => x.ArrivalAirport).HasMaxLength(3).IsRequired();
        flight.Property(x => x.DepartureTimeZone).HasMaxLength(100);
        flight.Property(x => x.ArrivalTimeZone).HasMaxLength(100);
        flight.Property(x => x.AirlineCode).HasMaxLength(10).IsRequired();
        flight.Property(x => x.FlightNumber).HasMaxLength(20).IsRequired();
        flight.Property(x => x.AircraftCode).HasMaxLength(10);
        flight.ToTable(t => {
            t.HasCheckConstraint("ck_flight_sequence", "sequence_number > 0");
            t.HasCheckConstraint("ck_flight_time", "arrival_at > departure_at");
        });

        var item = Table<BookingItem>(model, "booking_items", "booking_item_id");
        item.HasAlternateKey(x => new { x.Id, x.BookingId, x.BookingRouteId });
        item.HasIndex(x => new { x.BookingPassengerId, x.BookingRouteId }).IsUnique();
        item.HasOne<BookingPassenger>().WithMany().HasForeignKey(x => new { x.BookingPassengerId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);
        item.HasOne<BookingRoute>().WithMany().HasForeignKey(x => new { x.BookingRouteId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);

        var reservation = Table<Reservation>(model, "reservations", "reservation_id");
        reservation.HasAlternateKey(x => new { x.Id, x.BookingId });
        reservation.HasOne<Booking>().WithMany().HasForeignKey(x => new { x.BookingId, x.SupplierId })
            .HasPrincipalKey(x => new { x.Id, x.SupplierId }).OnDelete(DeleteBehavior.Restrict);
        reservation.HasIndex(x => new { x.BookingId, x.IdempotencyKey }).IsUnique();
        reservation.HasIndex(x => new { x.SupplierId, x.ProviderBookingId }).IsUnique();
        reservation.HasIndex(x => new { x.Status, x.ExpiresAt });
        reservation.Property(x => x.Version).IsConcurrencyToken();
        reservation.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
        reservation.Property(x => x.SupplierId).HasMaxLength(100).IsRequired();
        reservation.Property(x => x.IdempotencyKey).HasMaxLength(100).IsRequired();
        reservation.Property(x => x.ProviderBookingId).HasMaxLength(255);
        reservation.Property(x => x.ProviderBookingReference).HasMaxLength(100);
        reservation.Property(x => x.ErrorCode).HasMaxLength(100);
        reservation.ToTable(t => {
            t.HasCheckConstraint("ck_reservation_status", "status IN ('Pending','Confirmed','Rejected','Expired','Cancelled','Unknown')");
            t.HasCheckConstraint("ck_reservation_money", "(total_price_amount IS NULL AND total_price_currency IS NULL) OR (total_price_amount IS NOT NULL AND total_price_currency IS NOT NULL AND total_price_amount >= 0 AND total_price_currency ~ '^[A-Z]{3}$')");
            t.HasCheckConstraint("ck_reservation_key", "length(btrim(idempotency_key)) > 0");
            t.HasCheckConstraint("ck_confirmed_reservation", "status <> 'Confirmed' OR (provider_booking_id IS NOT NULL AND confirmed_at IS NOT NULL AND expires_at IS NOT NULL AND total_price_amount IS NOT NULL)");
        });

        var reservationPassenger = Table<ReservationPassenger>(model, "reservation_passengers", "reservation_passenger_id");
        reservationPassenger.HasOne<Reservation>().WithMany().HasForeignKey(x => new { x.ReservationId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);
        reservationPassenger.HasOne<BookingPassenger>().WithMany().HasForeignKey(x => new { x.BookingPassengerId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);
        reservationPassenger.Property(x => x.ProviderPassengerId).HasMaxLength(255).IsRequired();
        reservationPassenger.HasIndex(x => new { x.ReservationId, x.BookingPassengerId }).IsUnique();
        reservationPassenger.HasIndex(x => new { x.ReservationId, x.ProviderPassengerId }).IsUnique();

        var ticket = Table<ETicket>(model, "e_tickets", "e_ticket_id");
        ticket.HasAlternateKey(x => new { x.Id, x.BookingId, x.BookingRouteId });
        ticket.HasOne<BookingItem>().WithMany().HasForeignKey(x => new { x.BookingItemId, x.BookingId, x.BookingRouteId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId, x.BookingRouteId }).OnDelete(DeleteBehavior.Restrict);
        ticket.HasOne<Reservation>().WithMany().HasForeignKey(x => new { x.ReservationId, x.BookingId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId }).OnDelete(DeleteBehavior.Restrict);
        ticket.HasIndex(x => new { x.IssuerCode, x.TicketNumber }).IsUnique();
        ticket.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
        ticket.Property(x => x.TicketNumber).HasMaxLength(50).IsRequired();
        ticket.Property(x => x.IssuerCode).HasMaxLength(100).IsRequired();
        ticket.ToTable(t => {
            t.HasCheckConstraint("ck_ticket_status", "status IN ('Active','Used','Void','Refunded')");
            t.HasCheckConstraint("ck_ticket_money", "price_amount >= 0 AND price_currency ~ '^[A-Z]{3}$'");
            t.HasCheckConstraint("ck_ticket_number", "length(btrim(ticket_number)) > 0 AND length(btrim(issuer_code)) > 0");
        });

        var segment = Table<ETicketSegment>(model, "e_ticket_segments", "e_ticket_segment_id");
        segment.HasIndex(x => new { x.ETicketId, x.BookingFlightId }).IsUnique();
        segment.HasOne<ETicket>().WithMany().HasForeignKey(x => new { x.ETicketId, x.BookingId, x.BookingRouteId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId, x.BookingRouteId }).OnDelete(DeleteBehavior.Restrict);
        segment.HasOne<BookingFlight>().WithMany().HasForeignKey(x => new { x.BookingFlightId, x.BookingId, x.BookingRouteId })
            .HasPrincipalKey(x => new { x.Id, x.BookingId, x.BookingRouteId }).OnDelete(DeleteBehavior.Restrict);

        var inbox = Table<InboxMessage>(model, "inbox_messages", "inbox_message_id");
        inbox.Property(x => x.Source).HasMaxLength(100).IsRequired();
        inbox.Property(x => x.ExternalMessageId).HasMaxLength(255).IsRequired();
        inbox.HasIndex(x => new { x.Source, x.ExternalMessageId }).IsUnique();
        var outbox = Table<OutboxMessage>(model, "outbox_messages", "outbox_message_id");
        outbox.HasOne<Booking>().WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
        outbox.Property(x => x.MessageType).HasMaxLength(255).IsRequired();
        outbox.Property(x => x.Payload).HasColumnType("jsonb").IsRequired();
        outbox.Property(x => x.LastError).HasMaxLength(2000);
        outbox.HasIndex(x => new { x.PublishedAt, x.Id }).HasFilter("published_at IS NULL");
        outbox.ToTable(t => t.HasCheckConstraint("ck_outbox_attempts", "attempts >= 0"));

        foreach (var entity in model.Model.GetEntityTypes())
        foreach (var property in entity.GetProperties())
        {
            if (property.Name != nameof(Entity.Id)) property.SetColumnName(SnakeCase(property.Name));
            if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?)) property.SetColumnType("timestamp with time zone");
            if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?)) { property.SetPrecision(18); property.SetScale(2); }
            if (property.Name.EndsWith("Currency", StringComparison.Ordinal)) property.SetMaxLength(3);
        }
    }

    private static EntityTypeBuilder<T> Table<T>(ModelBuilder model, string table, string id) where T : Entity
    {
        var entity = model.Entity<T>();
        entity.HasBaseType((Type?)null);
        entity.ToTable(table);
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName(id).UseIdentityByDefaultColumn();
        entity.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        entity.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        return entity;
    }

    private static string SnakeCase(string value) => Regex.Replace(Regex.Replace(value, "([A-Z]+)([A-Z][a-z])", "$1_$2"), "([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
}
