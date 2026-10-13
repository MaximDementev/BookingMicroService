using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Представляет аэропорт.
    /// </summary>
    public class Airport : Entity
    {
        /// <summary>
        /// Трехбуквенный уникальный код аэропорта по классификации ИАТА (IATA Code).
        /// Например: "SVO" (Шереметьево), "JFK" (им. Кеннеди), "DXB" (Дубай).
        /// Это основной код, который видят пассажиры на билетах и в поиске.
        /// </summary>
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string IataCode { get; set; } = string.Empty;

        /// <summary>
        /// Четырехбуквенный код аэропорта по классификации ИКАО (ICAO Code).
        /// Например: "UUEE" (Шереметьево), "KJFK" (им. Кеннеди), "OMDB" (Дубай).
        /// Используется пилотами, диспетчерами и техническими авиасистемами.
        /// </summary>
        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string IcaoCode { get; set; } = string.Empty;

        /// <summary>
        /// Название аэропорта.
        /// Например: "Шереметьево имени А.С. Пушкина", "Хитроу".
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Название города, в котором находится аэропорт (или к которому он привязан).
        /// Например: "Москва", "Нью-Йорк", "Лондон".
        /// Важно для группировки рейсов при поиске по городу.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Название страны.
        /// Например: "Россия", "США", "ОАЭ". 
        /// Необходимо для проверки визовых правил и фильтрации международных/внутренних рейсов.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор часового пояса в формате IANA (например, "Europe/Moscow", "America/New_York").
        /// Позволяет корректно рассчитывать длительность перелета и отображать местное время.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string TimeZoneId { get; set; } = string.Empty;

        /// <summary>
        /// Флаг активности аэропорта.
        /// Позволяет временно или навсегда закрыть аэропорт для новых бронирований (например, на реконструкцию),
        /// не удаляя его из истории прошлых заказов.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
