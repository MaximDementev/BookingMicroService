// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt

namespace Contracts.Airlines_Integration.V1;

/// <summary>
/// Requests ticket issuance for a confirmed supplier booking after payment.
/// </summary>
public sealed record IssueTicketsRequest(
    long BookingId,
    string ProviderBookingId,
    string PaymentReference,
    decimal Amount,
    string Currency,
    string IdempotencyKey);
