namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IPayment"/>.
    /// </summary>
    public interface IPaymentService : IService<IPayment>
    {
        IEnumerable<IPayment> GetAll();

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
        IPayment Create(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

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
        IPayment CreateWithKey(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

        /// <summary>
        /// Creates and saves an <see cref="IAppliedPayment"/> associated with an invoice.
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <param name="invoiceKey">The invoice 'key'</param>
        /// <param name="appliedPaymentType">The applied payment type</param>
        /// <param name="description">The description of the payment application</param>
        /// <param name="amount">The amount of the payment to be applied</param>
        /// <returns>An <see cref="IAppliedPayment"/></returns>
        IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, decimal amount);

        /// <summary>
        /// Saves an <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayment">The <see cref="IAppliedPayment"/> to be saved</param>
        void Save(IAppliedPayment appliedPayment);

        /// <summary>
        /// Deletes a <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayment">The <see cref="IAppliedPayment"/> to be deleted</param>
        void Delete(IAppliedPayment appliedPayment);

        /// <summary>
        /// Deletes a collection of <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayments">The collection of <see cref="IAppliedPayment"/>s to be deleted</param>
        void Delete(IEnumerable<IAppliedPayment> appliedPayments);

        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the payment key
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey);

        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the invoice key
        /// </summary>
        /// <param name="invoiceKey">The invoice key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByInvoiceKey(Guid invoiceKey);
    }
}