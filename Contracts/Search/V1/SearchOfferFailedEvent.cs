using System;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace BookingMS.Contracts.Search.V1;

/// <summary>
/// Event published when the search operation fails.
/// </summary>
public sealed record SearchOfferFailedEvent(
    Guid CorrelationId,
    string ErrorCode,
    string ErrorMessage);
