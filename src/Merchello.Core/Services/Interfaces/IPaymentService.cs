namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IPayment"/>.
    /// </summary>
    public interface IPaymentService : IGetAllService<IPayment>, IAppliedPaymentService
    {
        /// <summary>
        /// Gets a collection of <see cref="IPayment"/> for a given payment provider.
        /// </summary>
        /// <param name="paymentMethodKey">The unique 'key' of the PaymentGatewayProvider</param>
        /// <returns>A collection of <see cref="IPayment"/></returns>
        IEnumerable<IPayment> GetByPaymentMethodKey(Guid? paymentMethodKey);

        /// <summary>
        /// Gets a collection of <see cref="IPayment"/> for a given invoice.
        /// </summary>
        /// <param name="invoiceKey">The unique 'key' of the invoice</param>
        /// <returns>A collection of <see cref="IPayment"/></returns>
        IEnumerable<IPayment> GetByInvoiceKey(Guid invoiceKey);

        /// <summary>
        /// Get a list of payments by customer key.
        /// </summary>
        /// <param name="customerKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IPayment"/>.
        /// </returns>
        IEnumerable<IPayment> GetByCustomerKey(Guid customerKey);

        /// <summary>
        /// Creates a <see cref="IPayment"/> without saving it.
        /// </summary>
        /// <param name="paymentMethodType">
        /// The <see cref="PaymentMethodType"/>.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="paymentMethodKey">
        /// The payment method key.
        /// </param>
        /// <returns>
        /// The <see cref="IPayment"/>.
        /// </returns>
        IPayment Create(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey);

        /// <summary>
        /// Creates a <see cref="IPayment"/> without saving it.
        /// </summary>
        /// <param name="paymentMethodTfKey">
        /// The payment method type field key.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="paymentMethodKey">
        /// The payment method key.
        /// </param>
        /// <returns>
        /// The <see cref="IPayment"/>.
        /// </returns>
        IPayment Create(Guid paymentMethodTfKey, Money amount, Guid? paymentMethodKey);

        /// <summary>
        /// Creates and saves a <see cref="IPayment"/>.
        /// </summary>
        /// <param name="paymentMethodType">
        /// The <see cref="PaymentMethodType"/>.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="paymentMethodKey">
        /// The payment method key.
        /// </param>
        /// <returns>
        /// The <see cref="IPayment"/>.
        /// </returns>
        IPayment CreateWithKey(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey);

    }
}