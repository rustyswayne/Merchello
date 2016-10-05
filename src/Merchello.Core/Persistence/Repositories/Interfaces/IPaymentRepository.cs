namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IPayment"/> entities.
    /// </summary>
    public interface IPaymentRepository : INPocoEntityRepository<IPayment>
    {
        /// <summary>
        /// Updates offer payment records that have payment method association with a payment method that is being deleted.
        /// </summary>
        /// <param name="paymentMethodKey">
        /// The payment method key.
        /// </param>
        /// <remarks>
        /// Sets the paymentMethodKey = NULL
        /// </remarks>
        void UpdateForPaymentMethodDelete(Guid paymentMethodKey);
    }
}