namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="ITaxMethod"/> entities.
    /// </summary>
    public interface ITaxMethodRepository : INPocoEntityRepository<ITaxMethod>
    {
        /// <summary>
        /// Determines if a method already exists for a given provider, ship country, and service code.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// A value indicating whether or not a method exists.
        /// </returns>
        bool Exists(Guid providerKey, string countryCode);
    }
}