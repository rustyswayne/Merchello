namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IGatewayProviderSettings"/> entities.
    /// </summary>
    public interface IGatewayProviderSettingsRepository : INPocoEntityRepository<IGatewayProviderSettings>
    {
        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/> for a ship country.
        /// </summary>
        /// <param name="shipCountryKey">
        /// The ship country key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IGatewayProviderSettings}"/>.
        /// </returns>
        IEnumerable<IGatewayProviderSettings> GetByShipCountryKey(Guid shipCountryKey);
    }
}