namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="ICustomerAddress"/> entities.
    /// </summary>
    public interface ICustomerAddressRepository : INPocoEntityRepository<ICustomerAddress>
    {
        /// <summary>
        /// Gets a collection of customer addresses for a specific customer.
        /// </summary>
        /// <param name="customerKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{ICustomerAddress}"/>.
        /// </returns>
        IEnumerable<ICustomerAddress> GetByCustomerKey(Guid customerKey);
    }
}