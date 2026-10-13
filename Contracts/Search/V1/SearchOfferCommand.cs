using System;

namespace BookingMS.Contracts.Search.V1
{
    /// <summary>
    /// Асинхронная команда для запуска процесса поиска предложений.
    /// </summary>
    public record SearchOfferCommand(
        Guid CorrelationId,       // ID сквозной цепочки логов (Saga Id)
        string DepartureAirport,  // Например, "VKO"
        string ArrivalAirport,    // Например, "AER"
        DateTime DepartureDate,
        int PassengersCount,
        Currency PreferredCurrency
    );
}
