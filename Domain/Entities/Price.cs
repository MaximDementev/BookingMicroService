using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Представляет денежную сумму в конкретной валюте (Value Object).
    /// Класс является неизменяемым (Immutable).
    /// </summary>
    public sealed class Price : IEquatable<Price>
    {
        /// <summary>
        /// Сумма (количество денежных единиц).
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Валюта из строго типизированного перечисления.
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Price"/>.
        /// </summary>
        public Price(decimal amount, Currency currency)
        {
            // Бизнес-правило: цена не может быть отрицательной (для билетов/услуг)
            if (amount < 0)
                throw new ArgumentException("Сумма не может быть отрицательной.", nameof(amount));

            // Округляем до 2 знаков после запятой (стандарт для большинства валют)
            Amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = currency;
        }

        #region Внутренняя валидация

        /// <summary>
        /// Проверяет, совпадает ли валюта с текущей, чтобы избежать некорректных операций.
        /// </summary>
        private void EnsureSameCurrency(Price other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException(
                    $"Невозможно выполнить операцию с разными валютами: {Currency} и {other.Currency}. " +
                    "Сначала необходимо выполнить конвертацию.");
            }
        }

        #endregion

        #region Математические операторы

        public static Price operator +(Price left, Price right)
        {
            left.EnsureSameCurrency(right);
            return new Price(left.Amount + right.Amount, left.Currency);
        }

        public static Price operator -(Price left, Price right)
        {
            left.EnsureSameCurrency(right);
            // Валидация на то, что результат не уйдет в минус, сработает внутри конструктора Price
            return new Price(left.Amount - right.Amount, left.Currency);
        }

        public static Price operator *(Price price, decimal multiplier)
        {
            return new Price(price.Amount * multiplier, price.Currency);
        }

        #endregion

        #region Сравнение и эквивалентность

        public bool Equals(Price? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object? obj) => Equals(obj as Price);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Price? left, Price? right) => Equals(left, right);
        public static bool operator !=(Price? left, Price? right) => !Equals(left, right);

        #endregion

        #region Форматирование (Использование ваших Extension-методов)

        /// <summary>
        /// Возвращает красивое строковое представление цены с символом валюты.
        /// Например: "1,500.00 ₽" или "$150.00".
        /// </summary>
        public override string ToString()
        {
            string symbol = Currency.GetSymbol();

            // Для англоязычных валют символ обычно ставят спереди, для рубля — сзади
            return Currency == Currency.USD || Currency == Currency.EUR || Currency == Currency.GBP
                ? $"{symbol}{Amount:N2}"
                : $"{Amount:N2} {symbol}";
        }

        /// <summary>
        /// Возвращает полное описание цены. Например: "100.00 (Российский рубль)".
        /// </summary>
        public string ToLongString()
        {
            return $"{Amount:N2} ({Currency.GetFullName()})";
        }

        #endregion
    }
}