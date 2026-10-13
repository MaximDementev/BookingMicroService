using Domain.Entities.Base;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая бронирование (заказ) в системе.
    /// Объединяет информацию о статусе покупки, общей стоимости и выпущенных билетах.
    /// </summary>
    public class Booking : Entity
    {
        /// <summary>
        /// Уникальный буквенно-цифровой код бронирования (PNR - Passenger Name Record).
        /// Например: "A1B2CD", "X7Y8ZF". Используется клиентом для поиска заказа и регистрации на рейс.
        /// </summary>
        public string BookingCode { get; set; } = string.Empty;

        /// <summary>
        /// Список электронных билетов, выпущенных в рамках данного бронирования.
        /// Заказ может содержать один или несколько билетов (например, для группы пассажиров или составного рейса).
        /// </summary>
        public List<ETicket> ETickets { get; set; } = new();

        /// <summary>
        /// Текущий статус жизненного цикла бронирования.
        /// Управляет бизнес-логикой оплаты, отмены и возврата (Draft, Pending, Paid и т.д.).
        /// </summary>
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// Общая стоимость всего бронирования (Value Object, инкапсулирующий сумму и валюту).
        /// Складывается из стоимости всех билетов, такс, сборов и скидок.
        /// </summary>
        public Price TotalPrice { get; set; } = null!;

        /// <summary>
        /// Дата и время создания бронирования.
        /// Позволяет отслеживать время тикет-тайма (окна, в течение которого нужно оплатить бронь в статусе Pending).
        /// </summary>
        public DateTime BookingDate { get; set; }
    }
}
