using System;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Contracts.Payment.V1;

/// <summary>
/// Command sent to Payment to create or initiate a payment.
/// </summary>
public sealed record PaymentRequest(
    long BookingId,
    string IdempotencyKey,
    decimal Amount,
    string Currency,
    DateTimeOffset PayBefore,
    string Description);
