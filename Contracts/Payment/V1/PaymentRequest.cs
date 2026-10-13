using System;

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
