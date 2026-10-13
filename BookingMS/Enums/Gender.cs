using Domain.Entities.Base;
using System.Runtime.Serialization;

namespace Domain.Enums
{
    /// <summary>
    /// Представляет пол (гендер) пассажира или пользователя системы.
    /// Соответствует международным стандартам кодирования информации о поле (ISO/IEC 5218 и IATA).
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Мужской пол.
        /// </summary>
        [EnumMember(Value = "male")]
        Male,

        /// <summary>
        /// Женский пол.
        /// </summary>
        [EnumMember(Value = "female")]
        Female,

        /// <summary>
        /// Не указан / Неизвестен / Другой.
        /// Используется, если пользователь решил не раскрывать пол, либо в соответствии 
        /// со стандартом IATA (маркер "X" в загранпаспортах для некоторых стран).
        /// </summary>
        [EnumMember(Value = "unspecified")]
        Unspecified
    }
}