namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IGatewayProviderSettings"/>.
    /// </summary>
    public interface IGatewayProviderService : IGatewayProviderSettingsService, INotificationMethodService, INotificationMessageService, IPaymentMethodService, IShipCountryService, IShipMethodService, IShipRateTierService, ITaxMethodService
    {

        /// <summary>
        /// Saves a single <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/> to save</param>
        void Save(IInvoice invoice);

        /// <summary>
        /// Creates a payment without saving it to the database
        /// </summary>
        /// <param name="paymentMethodType">The type of the payment method</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional payment method key</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePayment(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);


        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="payment">The <see cref="IPayment"/> to be saved</param>
        void Save(IPayment payment);

        /// <summary>
        /// Creates and saves an AppliedPayment
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <param name="invoiceKey">The invoice 'key'</param>
        /// <param name="appliedPaymentType">The applied payment type</param>
        /// <param name="description">The description of the payment application</param>
        /// <param name="amount">The amount of the payment to be applied</param>
        /// <returns>An <see cref="IAppliedPayment"/></returns>
        IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount);

        /// <summary>
        /// Saves a single <see cref="IAppliedPayment"/>.
        /// </summary>
        /// <param name="appliedPayment">
        /// The applied payment to be saved.
        /// </param>
        void Save(IAppliedPayment appliedPayment);

        /// <summary>
        /// Returns a collection of all <see cref="IInvoiceStatus"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IInvoiceStatus}"/>.
        /// </returns>
        IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses();

        /// <summary>
        /// Returns a collection of all <see cref="IOrderStatus"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IOrderStatus}"/>.
        /// </returns>
        IEnumerable<IOrderStatus> GetAllOrderStatuses();

        /// <summary>
        /// Gets the default <see cref="IWarehouse"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IWarehouse"/>.
        /// </returns>
        IWarehouse GetDefaultWarehouse();
    }
}