using System;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


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
