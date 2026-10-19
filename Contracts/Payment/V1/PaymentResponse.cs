using System;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


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
