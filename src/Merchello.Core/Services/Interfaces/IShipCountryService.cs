namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IShipCountry"/>.
    /// </summary>
    public interface IShipCountryService : IService
    {
        /// <summary>
        /// Gets a single <see cref="IShipCountry"/> by it's unique key (Guid)
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IShipCountry"/>.
        /// </returns>
        IShipCountry GetShipCountryByKey(Guid key);

        /// <summary>
        /// Gets a collection of all <see cref="IShipCountry"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IShipCountry}"/>.
        /// </returns>
        IEnumerable<IShipCountry> GetAllShipCountries();

        /// <summary>
        /// Gets a <see cref="IShipCountry"/> by CatalogKey and CountryCode
        /// </summary>
        /// <param name="catalogKey">The unique key of the <see cref="IWarehouseCatalog"/></param>
        /// <param name="countryCode">The two character ISO country code</param>
        /// <returns>An <see cref="IShipCountry"/></returns>
        IShipCountry GetShipCountry(Guid catalogKey, string countryCode);

        /// <summary>
        /// Gets a list of <see cref="IShipCountry"/> objects given a <see cref="IWarehouseCatalog"/> key
        /// </summary>
        /// <param name="catalogKey">The catalog key</param>
        /// <returns>A collection of <see cref="IShipCountry"/></returns>
        IEnumerable<IShipCountry> GetShipCountriesByCatalogKey(Guid catalogKey);

        /// <summary>
        /// Saves a single <see cref="shipCountry"/>
        /// </summary>
        /// <param name="shipCountry">
        /// The ship Country.
        /// </param>
        void Save(IShipCountry shipCountry);

        /// <summary>
        /// Saves a collection of <see cref="IShipCountry"/>.
        /// </summary>
        /// <param name="shipCountries">
        /// The collection of <see cref="IShipCountry"/>.
        /// </param>
        void Save(IEnumerable<IShipCountry> shipCountries);

        /// <summary>
        /// Deletes a single <see cref="IShipCountry"/> object
        /// </summary>
        /// <param name="shipCountry">
        /// The ship Country.
        /// </param>
        void Delete(IShipCountry shipCountry);

        /// <summary>
        /// Deletes a collection of <see cref="IShipCountry"/>.
        /// </summary>
        /// <param name="shipCountries">
        /// The collection of <see cref="IShipCountry"/>.
        /// </param>
        void Delete(IEnumerable<IShipCountry> shipCountries);
    }
}