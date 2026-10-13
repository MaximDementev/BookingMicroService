namespace Domain.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Представляет типы документов удостоверения личности пассажира или клиента.
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// Внутренний паспорт гражданина (например, паспорт РФ).
        /// Используется для внутренних поездок, покупок и регистраций внутри страны.
        /// </summary>
        [EnumMember(Value = "national_passport")]
        NationalPassport,

        /// <summary>
        /// Заграничный паспорт.
        /// Используется для международных поездок, перелетов и прохождения пограничного контроля.
        /// </summary>
        [EnumMember(Value = "international_passport")]
        InternationalPassport,

        /// <summary>
        /// Свидетельство о рождении.
        /// Применяется в качестве основного документа для детей и несовершеннолетних (обычно до 14 лет).
        /// </summary>
        [EnumMember(Value = "birth_certificate")]
        BirthCertificate,

        /// <summary>
        /// Военный билет.
        /// Документ удостоверения личности для военнослужащих, находящихся на службе или в запасе.
        /// </summary>
        [EnumMember(Value = "military_id")]
        MilitaryId,

        /// <summary>
        /// Водительское удостоверение.
        /// Может использоваться для подтверждения возраста или личности на мероприятиях (но часто не подходит для покупки ж/д и авиабилетов).
        /// </summary>
        [EnumMember(Value = "drivers_license")]
        DriversLicense,

        /// <summary>
        /// Иной документ, удостоверяющий личность.
        /// Используется как резервный вариант для редких типов документов (например, паспорт иностранного гражданина, справка об освобождении и т.д.).
        /// </summary>
        [EnumMember(Value = "other")]
        Other
    }

}