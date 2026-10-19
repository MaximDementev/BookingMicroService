using System.ComponentModel.DataAnnotations;

// Автор: DementevMaxim
// Дата изменения: 2026-10-19
// EULA: legal/EULA.txt
// NOTICE: legal/NOTICE.txt


namespace Domain.Entities
{
    /// <summary>
    /// Представляет авиакомпанию (перевозчика).
    /// </summary>
    public class Airline
    {
        /// <summary>
        /// Официальное название авиакомпании для отображения пользователям.
        /// Например: "Aeroflot", "S7 Airlines", "Turkish Airlines".
        /// </summary>
        public string Name { get; set; } = string.Empty;

    }
}
