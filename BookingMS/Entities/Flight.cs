using Domain.Entities.Base;

namespace Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая конкретный авиарейс.
    /// Содержит информацию о расписании, маршруте, перевозчике и доступных местах.
    /// </summary>
    public class Flight : Entity
    {
        /// <summary>
        /// Номер рейса (только цифровая часть или вместе с кодом).
        /// Например: "123" или "SU123". Сочетание кода авиакомпании и номера уникально идентифицирует рейс.
        /// </summary>
        public string FlightNumber { get; set; } = string.Empty;

        /// <summary>
        /// Авиакомпания (перевозчик), выполняющая данный рейс.
        /// </summary>
        public Airline Airline { get; set; } = null!;

        /// <summary>
        /// Аэропорт отправления.
        /// </summary>
        public Airport DepartureAirport { get; set; } = null!;

        /// <summary>
        /// Аэропорт прибытия.
        /// </summary>
        public Airport ArrivalAirport { get; set; } = null!;

        /// <summary>
        /// Плановое время вылета по расписанию (местное время аэропорта отправления).
        /// </summary>
        public DateTime ScheduledDeparture { get; set; }

        /// <summary>
        /// Плановое время прилета по расписанию (местное время аэропорта прибытия).
        /// </summary>
        public DateTime ScheduledArrival { get; set; }

        /// <summary>
        /// Плановая длительность полета.
        /// Использует TimeSpan, так как это временной интервал (продолжительность), а не конкретная дата.
        /// </summary>
        public TimeSpan ScheduledDuration { get; set; }

        /// <summary>
        /// Трехбуквенный ИАТА-код типа воздушного судна (Aircraft Type Code).
        /// Например: "73H" (Boeing 737-800), "32A" (Airbus A320), "SU9" (Sukhoi Superjet 100).
        /// </summary>
        public string AircraftCode { get; set; } = string.Empty;

        /// <summary>
        /// Количество свободных (доступных для бронирования) мест на рейсе.
        /// Уменьшается при каждой успешной покупке билета.
        /// </summary>
        public int AvailableSeats { get; set; }
    }
}
