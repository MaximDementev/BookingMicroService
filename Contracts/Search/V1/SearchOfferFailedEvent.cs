using System;
using System.Collections.Generic;

namespace BookingMS.Contracts.Search.V1
{
    /// <summary>
    /// Событие, публикуемое сервисом Search, когда предложения успешно найдены.
    /// </summary>
    public record SearchOfferResponseEvent(
        Guid CorrelationId,
        List<SearchOfferDto> Offers
    );

    // Упрощенные структуры данных (DTO) для транспортировки через брокер
    public record SearchOfferDto(
        string ProviderOfferId,
        AirlineDto Airline,
        RouteDto Route,
        int PassengersCount,
        PriceDto TotalPrice,
        Currency Currency,
        DateTime? ExpiresAt
    );

    public record AirlineDto(
        string Code, // Например, "SU"
        string Name  // Например, "Aeroflot"
    );

    public record RouteDto(
        string Origin,      // "VKO"
        string Destination, // "AER"
        TimeSpan Duration   // Длительность перелета
    );

    public record PriceDto(
        decimal Amount,
        Currency Currency
    );
}
