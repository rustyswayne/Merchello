﻿namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IAppliedPayment"/>.
    /// </summary>
    public interface IAppliedPaymentService : IService
    {
        /// <summary>
        /// Returns a <see cref="IAppliedPayment"/> by it's unique 'key'
        /// </summary>
        /// <param name="key">The unique 'key' of the <see cref="IAppliedPayment"/></param>
        /// <returns>An <see cref="IAppliedPayment"/></returns>
        IAppliedPayment GetAppliedPaymentByKey(Guid key);

        /// <summary>
        /// Creates and saves an <see cref="IAppliedPayment"/> associated with an invoice.
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <param name="invoiceKey">The invoice 'key'</param>
        /// <param name="appliedPaymentType">The applied payment type</param>
        /// <param name="description">The description of the payment application</param>
        /// <param name="amount">The amount of the payment to be applied</param>
        /// <returns>An <see cref="IAppliedPayment"/></returns>
        IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount);

        /// <summary>
        /// Saves an <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayment">The <see cref="IAppliedPayment"/> to be saved</param>
        void Save(IAppliedPayment appliedPayment);

        /// <summary>
        /// Saves a collection of <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayments">The collection of <see cref="IAppliedPayment"/>s to be saved</param>
        void Save(IEnumerable<IAppliedPayment> appliedPayments);

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
        /// <returns>A collection <see cref="IAppliedPayment"/></returns>
        IEnumerable<IAppliedPayment> GetAppliedPaymentsByInvoiceKey(Guid invoiceKey);
    }
}