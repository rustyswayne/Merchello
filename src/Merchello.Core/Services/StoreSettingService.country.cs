namespace Merchello.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Configuration;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <inheritdoc/>
        public ICountry GetCountryByCode(string countryCode)
        {
            return GetAllCountries().FirstOrDefault(x => x.CountryCode == countryCode);
        }

        /// <inheritdoc/>
        public IEnumerable<ICountry> GetAllCountries()
        {
            return MerchelloConfig.For.MerchelloCountries().Countries;
        }

        /// <inheritdoc/>
        public IEnumerable<ICountry> GetAllCountries(string[] excludeCountryCodes)
        {
            return GetAllCountries().Where(x => !excludeCountryCodes.Contains(x.CountryCode));
        }
    }
}
