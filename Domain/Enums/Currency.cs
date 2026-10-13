namespace Domain.Enums
{
    public enum Currency
    {
        USD,
        EUR,
        RUB,
        GBP,
        CNY
    }

    public static class CurrencyExtensions
    {
        public static string GetSymbol(this Currency currency) => currency switch
        {
            Currency.USD => "$",
            Currency.EUR => "€",
            Currency.RUB => "₽",
            Currency.GBP => "£",
            Currency.CNY => "¥",
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };

        public static string GetFullName(this Currency currency) => currency switch
        {
            Currency.USD => "Доллар США",
            Currency.EUR => "Евро",
            Currency.RUB => "Российский рубль",
            Currency.GBP => "Британский фунт",
            Currency.CNY => "Китайский юань",
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };
    }

}