using Domain.Entities.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая электронный билет.
    /// Привязывается к конкретному пассажиру и содержит информацию о его маршруте, стоимости и статусе.
    /// </summary>
    public class ETicket : Entity
    {
        /// <summary>
        /// Уникальный номер электронного билета.
        /// Обычно состоит из 13 цифр (например, первые 3 цифры — код авиакомпании) и используется для регистрации.
        /// </summary>
        public string TicketNumber { get; set; } = string.Empty;

        /// <summary>
        /// Данные пассажира, на имя которого оформлен данный билет.
        /// </summary>
        public Passenger Passenger { get; set; } = null!;

        /// <summary>
        /// Маршрут следования по данному билету (может состоять из одного или нескольких сегментов/маршрутных плеч).
        /// </summary>
        public List<Route> Route { get; set; } = new();

        /// <summary>
        /// Текущий статус жизненного цикла электронного билета (Active, Used, Void, Refunded).
        /// </summary>
        public ETicketStatus ETicketStatus { get; set; }

        /// <summary>
        /// Стоимость данного конкретного билета (включая тарифы и сборы) с привязкой к валюте.
        /// </summary>
        public Price Price { get; set; } = null!;
    }
}
