namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NodaMoney;

    /// <summary>
    /// Extension methods for Money.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Casts Money? to decimal?.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="decimal?"/>.
        /// </returns>
        public static decimal? Amount(this Money? value)
        {
            return value?.Amount;
        }

        /// <summary>
        /// Sums a collection of <see cref="Money"/> for a single currency.
        /// </summary>
        /// <param name="values">
        /// The <see cref="IEnumerable{Money}"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws if multiple currencies are detected.
        /// </exception>
        public static Money Sum(this IEnumerable<Money> values)
        {
            var monies = values as Money[] ?? values.ToArray();
            var currencyCodes = monies.Select(x => x.Currency.Code).Distinct().ToArray();
            if (currencyCodes.Any() && currencyCodes.Length > 1) throw new InvalidOperationException("Multiple currencies were detected. Use SumMultiCurrency");
            var code = currencyCodes.First();
            var total = new Money(0, code);
            return monies.Aggregate(total, (current, m) => current + m);
        }

        /// <summary>
        /// Sums a collection, returning a result of various currencies.
        /// </summary>
        /// <param name="values">
        /// The <see cref="IEnumerable{Money}"/> values.
        /// </param>
        /// <returns>
        /// The <see cref="IDictionary{Currency, Money}"/>.
        /// </returns>
        public static IDictionary<Currency, Money> SumMultiCurrency(this IEnumerable<Money> values)
        {
            var results = new Dictionary<Currency, Money>();
            var monies = values as Money[] ?? values.ToArray();
            var currencies = monies.Select(x => x.Currency).Distinct().ToArray();
            foreach (var currency in currencies)
            {
                if (!results.ContainsKey(currency)) results.Add(currency, new Money(0, currency));
                var moneyForCode = monies.Where(x => x.Currency.Code.Equals(currency.Code, StringComparison.InvariantCultureIgnoreCase));
                foreach (var mc in moneyForCode)
                {
                    results[currency] += mc; 
                }
            }

            return results;
        }
    }
}
