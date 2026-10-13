using System;

namespace BookingMS.Contracts.Search.V1
{
    /// <summary>
    /// Событие, публикуемое сервисом Search, если поиск завершился ошибкой.
    /// </summary>
    public record SearchOfferFailedEvent(
        Guid CorrelationId,
        string ErrorCode,    // Например: "SUPPLIER_TIMEOUT", "NO_FLIGHTS_FOUND"
        string ErrorMessage  // Текстовое описание для логов
    );
}
