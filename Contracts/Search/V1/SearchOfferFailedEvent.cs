using System;

namespace BookingMS.Contracts.Search.V1;

/// <summary>
/// Event published when the search operation fails.
/// </summary>
public sealed record SearchOfferFailedEvent(
    Guid CorrelationId,
    string ErrorCode,
    string ErrorMessage);
