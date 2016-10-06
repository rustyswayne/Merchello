namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IShipCountry"/> entities.
    /// </summary>
    public interface IShipCountryRepository : INPocoEntityRepository<IShipCountry>
    {
        /// <summary>
        /// Returns a value indication if a ship country is associated with a catalog and a country.
        /// </summary>
        /// <param name="catalogKey">
        /// The catalog key.
        /// </param>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// A value indication if a ship country is associated with a catalog and a country.
        /// </returns>
        bool Exists(Guid catalogKey, string countryCode);
    }
}