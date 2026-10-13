using System;
using System.Collections.Generic;

namespace BookingMS.Contracts.Search.V1;

/// <summary>
/// Event published when search offers have been retrieved.
/// </summary>
public sealed record SearchOfferResponseEvent(
    Guid CorrelationId,
    IReadOnlyList<SearchOfferDto> Offers);

public sealed record SearchOfferDto(
    string ProviderOfferId,
    AirlineDto Airline,
    RouteDto Route,
    int PassengersCount,
    PriceDto TotalPrice,
    string Currency,
    DateTimeOffset? ExpiresAt);

public sealed record AirlineDto(
    string Code,
    string Name);

public sealed record RouteDto(
    string Origin,
    string Destination,
    TimeSpan Duration);

public sealed record PriceDto(
    decimal Amount,
    string Currency);
