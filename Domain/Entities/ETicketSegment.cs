using Domain.Entities.Base;

namespace Domain.Entities
{
    /// <summary>
    /// Локальный снимок сегмента маршрута в электронном билете.
    /// Все идентификаторы Flight/Airline/Airport относятся к внешним сервисам
    /// и намеренно не оформляются как внешние ключи в БД Booking Service.
    /// </summary>
    public class ETicketSegment : Entity
    {
        public int SegmentOrder { get; set; }

        public long FlightId { get; set; }
        public string FlightNumber { get; set; } = string.Empty;

        public long AirlineId { get; set; }
        public string AirlineName { get; set; } = string.Empty;

        public long DepartureAirportId { get; set; }
        public string DepartureAirportIataCode { get; set; } = string.Empty;
        public string DepartureAirportName { get; set; } = string.Empty;
        public DateTime ScheduledDeparture { get; set; }

        public long ArrivalAirportId { get; set; }
        public string ArrivalAirportIataCode { get; set; } = string.Empty;
        public string ArrivalAirportName { get; set; } = string.Empty;
        public DateTime ScheduledArrival { get; set; }
    }
}