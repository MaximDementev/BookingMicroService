using System;

namespace BookingMS.Contracts.Search.V1;

/// <summary>
/// Asynchronous command that starts a search for flight offers.
/// </summary>
public sealed record SearchOfferCommand(
    Guid CorrelationId,
    string DepartureAirport,
    string ArrivalAirport,
    DateTime DepartureDate,
    int PassengersCount,
    string PreferredCurrency);
