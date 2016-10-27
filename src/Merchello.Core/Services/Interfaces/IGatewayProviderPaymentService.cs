﻿namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Gateways;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a <see cref="IPayment"/> service for use in <see cref="IGatewayProvider"/>s.
    /// </summary>
    public interface IGatewayProviderPaymentService : IService
    {
        /// <summary>
        /// Creates a payment without saving it to the database
        /// </summary>
        /// <param name="paymentMethodType">The type of the payment method</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional payment method key</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePayment(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey);


        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="payment">The <see cref="IPayment"/> to be saved</param>
        void Save(IPayment payment);

        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the payment key
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey);

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
    }
}