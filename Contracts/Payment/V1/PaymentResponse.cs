using System;

namespace Contracts.Payment.V1;

/// <summary>
/// Event published by Payment after a payment has been successfully completed.
/// </summary>
public sealed record PaymentResponse(
    Guid EventId,
    DateTimeOffset OccurredAt,
    long BookingId,
    string PaymentId,
    decimal Amount,
    string Currency,
    DateTimeOffset PaidAt);
