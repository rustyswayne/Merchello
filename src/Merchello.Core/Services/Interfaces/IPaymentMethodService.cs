namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IPaymentMethod"/>.
    /// </summary>
    public interface IPaymentMethodService : IService
    {
        /// <summary>
        /// Gets a <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="key">The unique 'key' (Guid) of the <see cref="IPaymentMethod"/></param>
        /// <returns><see cref="IPaymentMethod"/></returns>
        IPaymentMethod GetPaymentMethodByKey(Guid key);

        /// <summary>
        /// Gets a collection of <see cref="IPaymentMethod"/> for a given PaymentGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the PaymentGatewayProvider</param>
        /// <returns>A collection of <see cref="IPaymentMethod"/></returns>
        IEnumerable<IPaymentMethod> GetPaymentMethodsByProviderKey(Guid providerKey);

        /// <summary>
        /// Returns a <see cref="IPaymentMethod"/> given it's paymentCode 
        /// </summary>
        /// <param name="providerKey">
        /// The unique 'key' of the PaymentGatewayProvider
        /// </param>
        /// <param name="paymentCode">
        /// The paymentCode
        /// </param>
        /// <returns>
        /// The <see cref="IPaymentMethod"/>.
        /// </returns>
        IPaymentMethod GetPaymentMethodByPaymentCode(Guid providerKey, string paymentCode);

        /// <summary>
        /// Creates a <see cref="IPaymentMethod"/> for a given provider.  If the provider already 
        /// defines a paymentCode, the creation fails.
        /// </summary>
        /// <param name="providerKey">The unique 'key' (Guid) of the TaxationGatewayProvider</param>
        /// <param name="name">The name of the payment method</param>
        /// <param name="description">The description of the payment method</param>
        /// <param name="paymentCode">The unique 'payment code' associated with the payment method.  (e.g. visa, mc)</param>
        /// <returns>The <see cref="IPaymentMethod"/></returns>
        Attempt<IPaymentMethod> CreatePaymentMethodWithKey(Guid providerKey, string name, string description, string paymentCode);

        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be saved</param>
        void Save(IPaymentMethod paymentMethod);

        /// <summary>
        /// Saves a collection of <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="paymentMethods">A collection of <see cref="IPaymentMethod"/> to be saved</param>
        void Save(IEnumerable<IPaymentMethod> paymentMethods);

        /// <summary>
        /// Deletes a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be deleted</param>
        void Delete(IPaymentMethod paymentMethod);

        /// <summary>
        /// Deletes a collection of <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethods">The collection of <see cref="IPaymentMethod"/> to be deleted</param>
        void Delete(IEnumerable<IPaymentMethod> paymentMethods);
    }
}