using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Электронный билет, принадлежащий бронированию.
    /// PassengerId ссылается на пассажира в User Service и не является FK в локальной БД.
    /// Данные пассажира сохраняются как снимок на момент оформления билета.
    /// </summary>
    public class ETicket : Entity
    {
        public string TicketNumber { get; set; } = string.Empty;

        /// <summary>Идентификатор пассажира во внешнем User Service; локального FK нет.</summary>
        public long PassengerId { get; set; }

        public string PassengerFirstName { get; set; } = string.Empty;
        public string PassengerLastName { get; set; } = string.Empty;
        public string? PassengerMiddleName { get; set; }

        /// <summary>Снимок сегментов маршрута на момент оформления билета.</summary>
        public List<ETicketSegment> Segments { get; set; } = new();

        public ETicketStatus ETicketStatus { get; set; }

        public Price Price { get; set; } = null!;
    }
}