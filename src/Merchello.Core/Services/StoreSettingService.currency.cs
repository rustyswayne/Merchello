namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <inheritdoc/>
        public IEnumerable<Currency> GetAllCurrencies()
        {
            return Currency.GetAllCurrencies();
        }

        /// <inheritdoc/>
        public Currency GetCurrencyByCode(string currencyCode)
        {
            return Currency.FromCode(currencyCode);
        }

        /// <inheritdoc/>
        public Currency GetCurrencyByCountryCode(string countryCode)
        {
            return Currency.FromRegion(countryCode);
        }
    }
}
