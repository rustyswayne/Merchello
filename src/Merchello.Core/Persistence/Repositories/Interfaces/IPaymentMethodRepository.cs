namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IPaymentMethod"/> entities.
    /// </summary>
    public interface IPaymentMethodRepository : INPocoEntityRepository<IPaymentMethod>
    {
        /// <summary>
        /// Determines if a method already exists for a given provider, ship country, and service code.
        /// </summary>
        /// <param name="providerKey">
        /// The provider key.
        /// </param>
        /// <param name="paymentCode">
        /// The payment code.
        /// </param>
        /// <returns>
        /// A value indicating whether or not a method exists.
        /// </returns>
        bool Exists(Guid providerKey, string paymentCode);
    }
}