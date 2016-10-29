namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IShipMethod"/> entities.
    /// </summary>
    public interface IShipMethodRepository : INPocoEntityRepository<IShipMethod>
    {
        /// <summary>
        /// Determines if a method already exists for a given provider, ship country, and service code.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="shipCountryKey">
        /// The ship country key.
        /// </param>
        /// <param name="serviceCode">
        /// The service code.
        /// </param>
        /// <returns>
        /// A value indicating whether or not a method exists.
        /// </returns>
        bool Exists(Guid providerKey, Guid shipCountryKey, string serviceCode);
    }
}