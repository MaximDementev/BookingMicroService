using System;
using System.Collections.Generic;

namespace Contracts.Notification.V1;

/// <summary>
/// Published after the booking has been completed and tickets have been issued.
/// </summary>
public sealed record BookingCompleted(
    Guid EventId,
    DateTimeOffset OccurredAt,
    long BookingId,
    long UserId,
    IReadOnlyList<long> TicketIds);
