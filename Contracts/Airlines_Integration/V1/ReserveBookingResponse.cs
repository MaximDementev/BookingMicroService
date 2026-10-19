using System;
using System.Collections.Generic;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


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
