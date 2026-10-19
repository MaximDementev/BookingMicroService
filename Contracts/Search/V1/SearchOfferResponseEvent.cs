using System;
using System.Collections.Generic;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


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
    DateTimeOffset? ExpiresAt)
{
    public long OfferId { get; init; }
    public string SupplierId { get; init; } = string.Empty;
}

public sealed record AirlineDto(
    string Code,
    string Name);

public sealed record RouteDto(
    string Origin,
    string Destination,
    TimeSpan Duration)
{
    public int SequenceNumber { get; init; } = 1;
    public IReadOnlyList<FlightSegmentDto> Segments { get; init; } = Array.Empty<FlightSegmentDto>();
}

public sealed record FlightSegmentDto(
    string ProviderSegmentId,
    int SequenceNumber,
    string DepartureAirport,
    string ArrivalAirport,
    DateTimeOffset DepartureAt,
    DateTimeOffset ArrivalAt,
    string AirlineCode,
    string FlightNumber,
    string? AircraftCode,
    long? SourceFlightId);

public sealed record PriceDto(
    decimal Amount,
    string Currency);
