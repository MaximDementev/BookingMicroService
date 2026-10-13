using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Сущность, представляющая конкретный сегмент маршрута.
    /// Описывает путь из точки А в точку Б, который может выполняться как прямым рейсом, так и с пересадками.
    /// </summary>
    public class Route : Entity
    {
        /// <summary>
        /// Список рейсов, из которых состоит данный маршрут.
        /// Если рейс прямой — здесь будет один элемент, если с пересадками — несколько последовательных рейсов.
        /// </summary>
        public List<Flight> Flights { get; set; } = new();

        /// <summary>
        /// Аэропорт отправления для данного участка маршрута.
        /// </summary>
        public Airport DepartureAirport { get; set; } = null!;

        /// <summary>
        /// Аэропорт прибытия для конечной точки данного участка маршрута.
        /// </summary>
        public Airport ArrivalAirport { get; set; } = null!;
    }
}
