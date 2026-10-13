using System.ComponentModel.DataAnnotations;

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