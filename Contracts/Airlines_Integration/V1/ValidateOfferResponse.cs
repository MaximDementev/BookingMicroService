using System;
using System.Collections.Generic;

namespace Contracts.Airlines_Integration.V1;

public enum OfferAvailabilityStatus
{
    Available,
    Changed,
    Unavailable
}

public sealed record MoneyDto(
    decimal Amount,
    string Currency);

public sealed record OfferChangeDto(
    string Field,
    string? PreviousValue,
    string? CurrentValue);

/// <summary>
/// Result of validating an offer against the supplier's current availability and price.
/// </summary>
public sealed record ValidateOfferResponse(
    OfferAvailabilityStatus Status,
    string? ProviderOfferId,
    MoneyDto? Price,
    DateTimeOffset? ExpiresAt,
    IReadOnlyList<OfferChangeDto> Changes,
    string? ReasonCode);
