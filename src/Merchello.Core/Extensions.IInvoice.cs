﻿namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Models.TypeFields;

    using Newtonsoft.Json;

    using NodaMoney;

    using Formatting = Newtonsoft.Json.Formatting;

    /// <summary>
    /// Extension methods for <see cref="IInvoice"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Returns a constructed invoice number (including it's invoice number prefix - if any)
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <returns>The prefixed invoice number</returns>
        public static string PrefixedInvoiceNumber(this IInvoice invoice)
        {
            return String.IsNullOrEmpty(invoice.InvoiceNumberPrefix)
                ? invoice.InvoiceNumber.ToString(CultureInfo.InvariantCulture)
                : $"{invoice.InvoiceNumberPrefix}-{invoice.InvoiceNumber}";
        }

        /// <summary>
        /// Returns the currency code associated with the invoice
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns>The currency code associated with the invoice</returns>
        public static string CurrencyCode(this IInvoice invoice)
        {
            var allCurrencyCodes =
                invoice.Items.Select(x => x.ExtendedData.GetValue(Constants.ExtendedDataKeys.CurrencyCode)).Distinct().ToArray();

            return allCurrencyCodes.Any() ? allCurrencyCodes.First() : String.Empty;
        }

        /// <summary>
        /// The currency.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="Currency"/>.
        /// </returns>
        public static Currency Currency(this IInvoice invoice)
        {
            var currencyCode = invoice.CurrencyCode();
            return !String.IsNullOrWhiteSpace(currencyCode) ?
                    NodaMoney.Currency.FromCode(currencyCode) :
                    default(Currency);
        }

        #region Address

        /// <summary>
        /// Utility extension method to add an <see cref="IAddress"/> to an <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/> to which to set the address information</param>
        /// <param name="address">The billing address <see cref="IAddress"/></param>
        public static void SetBillingAddress(this IInvoice invoice, IAddress address)
        {
            invoice.BillToName = address.Name;
            invoice.BillToCompany = address.Organization;
            invoice.BillToAddress1 = address.Address1;
            invoice.BillToAddress2 = address.Address2;
            invoice.BillToLocality = address.Locality;
            invoice.BillToRegion = address.Region;
            invoice.BillToPostalCode = address.PostalCode;
            invoice.BillToCountryCode = address.CountryCode;
            invoice.BillToPhone = address.Phone;
            invoice.BillToEmail = address.Email;
        }

        /// <summary>
        /// Utility extension to extract the billing <see cref="IAddress"/> from an <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The invoice</param>
        /// <returns>
        /// The billing address saved in the invoice
        /// </returns>
        public static IAddress GetBillingAddress(this IInvoice invoice)
        {
            return new Address()
            {
                Name = invoice.BillToName,
                Organization = invoice.BillToCompany,
                Address1 = invoice.BillToAddress1,
                Address2 = invoice.BillToAddress2,
                Locality = invoice.BillToLocality,
                Region = invoice.BillToRegion,
                PostalCode = invoice.BillToPostalCode,
                CountryCode = invoice.BillToCountryCode,
                Phone = invoice.BillToPhone,
                Email = invoice.BillToEmail,
                AddressType = AddressType.Billing
            };
        }

        /// <summary>
        /// Gets the collection of shipping addresses.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IAddress}"/>.
        /// </returns>
        public static IEnumerable<IAddress> GetShippingAddresses(this IInvoice invoice)
        {
            var shippingLineItems = invoice.ShippingLineItems().ToArray();
            if (!shippingLineItems.Any()) return Enumerable.Empty<IAddress>();

            var addresses = shippingLineItems.Select(item => item.ExtendedData.GetShipment<InvoiceLineItem>().GetDestinationAddress()).ToList();

            return addresses;
        }

        #endregion

        #region Static Collections

        ///// <summary>
        ///// The add to collection.
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice.
        ///// </param>
        ///// <param name="collection">
        ///// The collection.
        ///// </param>
        //public static void AddToCollection(this IInvoice invoice, IEntityCollection collection)
        //{
        //    invoice.AddToCollection(collection.Key);
        //}

        ///// <summary>
        ///// The add to collection.
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice.
        ///// </param>
        ///// <param name="collectionKey">
        ///// The collection key.
        ///// </param>
        //public static void AddToCollection(this IInvoice invoice, Guid collectionKey)
        //{
        //    if (!EntityCollectionProviderResolver.HasCurrent || !MerchelloContext.HasCurrent) return;
        //    var attempt = EntityCollectionProviderResolver.Current.GetProviderForCollection(collectionKey);
        //    if (!attempt.Success) return;

        //    var provider = attempt.Result;

        //    if (!provider.EnsureEntityType(EntityType.Invoice))
        //    {
        //        MultiLogHelper.Debug(typeof(ProductExtensions), "Attempted to add a invoice to a non invoice collection");
        //        return;
        //    }

        //    MerchelloContext.Current.Services.InvoiceService.AddToCollection(invoice.Key, collectionKey);
        //}

        ///// <summary>
        ///// The remove from collection.
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice.
        ///// </param>
        ///// <param name="collection">
        ///// The collection.
        ///// </param>        
        //public static void RemoveFromCollection(this IInvoice invoice, IEntityCollection collection)
        //{
        //    invoice.RemoveFromCollection(collection.Key);
        //}

        ///// <summary>
        ///// The remove from collection.
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice.
        ///// </param>
        ///// <param name="collectionKey">
        ///// The collection key.
        ///// </param>        
        //public static void RemoveFromCollection(this IInvoice invoice, Guid collectionKey)
        //{
        //    if (!MerchelloContext.HasCurrent) return;
        //    MerchelloContext.Current.Services.InvoiceService.RemoveFromCollection(invoice.Key, collectionKey);
        //}


        /// <summary>
        /// Returns static collections containing the invoice.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityCollection}"/>.
        /// </returns>
        public static IEnumerable<IEntityCollection> GetCollectionsContaining(this IInvoice invoice)
        {

            return MC.Services.EntityCollectionService.GetByInvoiceKey(invoice.Key);
        }

        #endregion

        #region Customer

        /// <summary>
        /// Gets the customer from an invoice (if applicable)
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="ICustomer"/>.
        /// </returns>
        public static ICustomer Customer(this IInvoice invoice)
        {
            if (invoice.CustomerKey == null) return null;
            return MC.Services.CustomerService.GetByKey(invoice.CustomerKey.Value);
        }

        #endregion

        #region Order

        ///// <summary>
        ///// Prepares an <see cref="IOrder"/> without saving it to the database.  
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/> to base the order on</param>
        ///// <returns>The <see cref="IOrder"/></returns>        
        //public static IOrder PrepareOrder(this IInvoice invoice)
        //{
        //    return invoice.PrepareOrder(MerchelloContext.Current);
        //}

        ///// <summary>
        ///// Prepare an <see cref="IOrder"/> with saving it to the database
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/> to base the order or</param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <returns>The <see cref="IOrder"/></returns>
        //public static IOrder PrepareOrder(this IInvoice invoice, IMerchelloContext merchelloContext)
        //{
        //    var orderStatusKey = invoice.HasShippableItems()
        //                             ? Constants.DefaultKeys.OrderStatus.NotFulfilled
        //                             : Constants.DefaultKeys.OrderStatus.Fulfilled;

        //    var orderStatus =
        //        merchelloContext.Services.OrderService.GetOrderStatusByKey(orderStatusKey);

        //    return invoice.PrepareOrder(merchelloContext, new OrderBuilderChain(orderStatus, invoice));
        //}

        ///// <summary>
        ///// Prepares an <see cref="IOrder"/> without saving it to the database.  
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/> to base the order on</param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="orderBuilder">The <see cref="IBuilderChain{IOrder}"/></param>
        ///// <returns>The <see cref="IOrder"/></returns>
        ///// <remarks>
        ///// 
        ///// This method will save the invoice in the event it has not previously been saved
        ///// 
        ///// </remarks>
        //public static IOrder PrepareOrder(
        //    this IInvoice invoice,
        //    IMerchelloContext merchelloContext,
        //    IBuilderChain<IOrder> orderBuilder)
        //{
        //    if (!invoice.HasIdentity) merchelloContext.Services.InvoiceService.Save(invoice);

        //    var attempt = orderBuilder.Build();
        //    if (attempt.Success) return attempt.Result;

        //    MultiLogHelper.Error<OrderBuilderChain>("Extension method PrepareOrder failed", attempt.Exception);
        //    throw attempt.Exception;
        //}

        #endregion

        #region AppliedPayments

        /// <summary>
        /// Returns a collection of <see cref="IAppliedPayment"/> for the invoice
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        public static IEnumerable<IAppliedPayment> AppliedPayments(this IInvoice invoice)
        {
            return MC.Services.PaymentService.GetAppliedPaymentsByInvoiceKey(invoice.Key);
        }


        #endregion

        #region Payments

        /// <summary>
        /// Gets a collection of <see cref="IPayment"/> applied to the invoice
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <returns>A collection of <see cref="IPayment"/></returns>
        public static IEnumerable<IPayment> Payments(this IInvoice invoice)
        {
            return MC.Services.PaymentService.GetByInvoiceKey(invoice.Key);
        }


        /// <summary>
        /// Attempts to process a payment
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <param name="paymentGatewayMethod">The <see cref="IPaymentGatewayMethod"/> to use in processing the payment</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>The <see cref="IPaymentResult"/></returns>
        public static IPaymentResult AuthorizePayment(this IInvoice invoice, IPaymentGatewayMethod paymentGatewayMethod, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(paymentGatewayMethod, "paymentGatewayMethod");

            return paymentGatewayMethod.AuthorizePayment(invoice, args);
        }

        /// <summary>
        /// Attempts to process a payment
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <param name="paymentGatewayMethod">The <see cref="IPaymentGatewayMethod"/> to use in processing the payment</param>
        /// <returns>The <see cref="IPaymentResult"/></returns>
        public static IPaymentResult AuthorizePayment(this IInvoice invoice, IPaymentGatewayMethod paymentGatewayMethod)
        {
            Ensure.ParameterCondition(invoice.HasIdentity, "The invoice must be saved before a payment can be authorized.");
            Ensure.ParameterNotNull(paymentGatewayMethod, "paymentGatewayMethod");

            return invoice.AuthorizePayment(paymentGatewayMethod, new ProcessorArgumentCollection());
        }


        ///// <summary>
        ///// Attempts to process a payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>The <see cref="IPaymentResult"/></returns>
        //internal static IPaymentResult AuthorizePayment(this IInvoice invoice, IMerchelloContext merchelloContext, Guid paymentMethodKey, ProcessorArgumentCollection args)
        //{
        //    var paymentMethod = merchelloContext.Gateways.Payment.GetPaymentGatewayMethodByKey(paymentMethodKey);
        //    return invoice.AuthorizePayment(paymentMethod, args);
        //}

        ///// <summary>
        ///// Attempts to process a payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <returns>The <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult AuthorizePayment(this IInvoice invoice, Guid paymentMethodKey)
        //{
        //    return invoice.AuthorizePayment(paymentMethodKey, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Authorizes and Captures a Payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="paymentGatewayMethod">The <see cref="IPaymentMethod"/></param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult AuthorizeCapturePayment(this IInvoice invoice, IPaymentGatewayMethod paymentGatewayMethod, ProcessorArgumentCollection args)
        //{
        //    Mandate.ParameterNotNull(paymentGatewayMethod, "paymentGatewayMethod");

        //    return paymentGatewayMethod.AuthorizeCapturePayment(invoice, invoice.Total, args);
        //}

        ///// <summary>
        ///// Authorizes and Captures a Payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="paymentGatewayMethod">The <see cref="IPaymentMethod"/></param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult AuthorizeCapturePayment(this IInvoice invoice, IPaymentGatewayMethod paymentGatewayMethod)
        //{
        //    return invoice.AuthorizeCapturePayment(paymentGatewayMethod, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Authorizes and Captures a Payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult AuthorizeCapturePayment(this IInvoice invoice, Guid paymentMethodKey, ProcessorArgumentCollection args)
        //{
        //    return invoice.AuthorizeCapturePayment(MerchelloContext.Current, paymentMethodKey, args);
        //}

        ///// <summary>
        ///// Authorizes and Captures a Payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //internal static IPaymentResult AuthorizeCapturePayment(this IInvoice invoice, IMerchelloContext merchelloContext, Guid paymentMethodKey, ProcessorArgumentCollection args)
        //{
        //    var paymentMethod = merchelloContext.Gateways.Payment.GetPaymentGatewayMethodByKey(paymentMethodKey);
        //    return invoice.AuthorizeCapturePayment(paymentMethod, args);
        //}

        ///// <summary>
        ///// Authorizes and Captures a Payment
        ///// </summary>
        ///// <param name="invoice">The <see cref="IInvoice"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult AuthorizeCapturePayment(this IInvoice invoice, Guid paymentMethodKey)
        //{
        //    return invoice.AuthorizeCapturePayment(paymentMethodKey, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Captures a payment for the <see cref="IInvoice"/>
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice to be paid
        ///// </param>
        ///// <param name="payment">
        ///// The <see cref="IPayment"/>
        ///// </param>
        ///// <param name="paymentGatewayMethod">
        ///// The <see cref="IPaymentGatewayMethod"/>
        ///// </param>
        ///// <param name="amount">
        ///// The amount to the payment to be captured
        ///// </param>
        ///// <param name="args">
        ///// Additional arguments required by the payment processor
        ///// </param>
        ///// <returns>
        ///// A <see cref="IPaymentResult"/>
        ///// </returns>
        //public static IPaymentResult CapturePayment(this IInvoice invoice, IPayment payment, IPaymentGatewayMethod paymentGatewayMethod, decimal amount, ProcessorArgumentCollection args)
        //{
        //    return paymentGatewayMethod.CapturePayment(invoice, payment, amount, args);
        //}

        ///// <summary>
        ///// Captures a payment for the <see cref="IInvoice"/>
        ///// </summary>
        ///// <param name="invoice">
        ///// The invoice to be paid
        ///// </param>
        ///// <param name="payment">
        ///// The <see cref="IPayment"/>
        ///// </param>
        ///// <param name="paymentGatewayMethod">
        ///// The <see cref="IPaymentGatewayMethod"/>
        ///// </param>
        ///// <param name="amount">
        ///// The amount to the payment to be captured
        ///// </param>
        ///// <returns>
        ///// A <see cref="IPaymentResult"/>
        ///// </returns>
        //public static IPaymentResult CapturePayment(this IInvoice invoice, IPayment payment, IPaymentGatewayMethod paymentGatewayMethod, decimal amount)
        //{
        //    return invoice.CapturePayment(payment, paymentGatewayMethod, amount, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Captures a payment for the <see cref="IInvoice"/>
        ///// </summary>
        ///// <param name="invoice">The invoice to be paid</param>
        ///// <param name="payment">The <see cref="IPayment"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <param name="amount">The amount to the payment to be captured</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult CapturePayment(this IInvoice invoice, IPayment payment, Guid paymentMethodKey, decimal amount, ProcessorArgumentCollection args)
        //{
        //    return invoice.CapturePayment(MerchelloContext.Current, payment, paymentMethodKey, amount, args);
        //}

        ///// <summary>
        ///// Captures a payment for the <see cref="IInvoice"/>
        ///// </summary>
        ///// <param name="invoice">The invoice to be paid</param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="payment">The <see cref="IPayment"/></param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentMethod"/> key</param>
        ///// <param name="amount">The amount to the payment to be captured</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //internal static IPaymentResult CapturePayment(this IInvoice invoice, IMerchelloContext merchelloContext, IPayment payment, Guid paymentMethodKey, decimal amount, ProcessorArgumentCollection args)
        //{
        //    var paymentGatewayMethod = merchelloContext.Gateways.Payment.GetPaymentGatewayMethodByKey(paymentMethodKey);
        //    return invoice.CapturePayment(payment, paymentGatewayMethod, amount, args);
        //}

        ///// <summary>
        ///// Refunds a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be refunded</param>
        ///// <param name="paymentGatewayMethod">The <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="amount">The amount to be refunded</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult RefundPayment(this IInvoice invoice, IPayment payment, IPaymentGatewayMethod paymentGatewayMethod, decimal amount, ProcessorArgumentCollection args)
        //{
        //    return paymentGatewayMethod.RefundPayment(invoice, payment, amount, args);
        //}

        ///// <summary>
        ///// Refunds a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be refunded</param>
        ///// <param name="paymentGatewayMethod">The <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="amount">The amount to be refunded</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult RefundPayment(this IInvoice invoice, IPayment payment, IPaymentGatewayMethod paymentGatewayMethod, decimal amount)
        //{
        //    return invoice.RefundPayment(payment, paymentGatewayMethod, amount, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Refunds a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be refunded</param>
        ///// <param name="paymentMethodKey">The key of the <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="amount">The amount to be refunded</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult RefundPayment(this IInvoice invoice, IPayment payment, Guid paymentMethodKey, decimal amount)
        //{
        //    return invoice.RefundPayment(payment, paymentMethodKey, amount, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Refunds a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be refunded</param>
        ///// <param name="paymentMethodKey">The key of the <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="amount">The amount to be refunded</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult RefundPayment(this IInvoice invoice, IPayment payment, Guid paymentMethodKey, decimal amount, ProcessorArgumentCollection args)
        //{
        //    return invoice.RefundPayment(MerchelloContext.Current, payment, paymentMethodKey, amount, args);
        //}

        ///// <summary>
        ///// Refunds a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="payment">The payment to be refunded</param>
        ///// <param name="paymentMethodKey">The key of the <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="amount">The amount to be refunded</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //internal static IPaymentResult RefundPayment(this IInvoice invoice, IMerchelloContext merchelloContext, IPayment payment, Guid paymentMethodKey, decimal amount, ProcessorArgumentCollection args)
        //{
        //    var paymentGatewayMethod = merchelloContext.Gateways.Payment.GetPaymentGatewayMethodByKey(paymentMethodKey);
        //    return invoice.RefundPayment(payment, paymentGatewayMethod, amount, args);
        //}

        ///// <summary>
        ///// Voids a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be voided</param>
        ///// <param name="paymentGatewayMethod">The <see cref="IPaymentGatewayMethod"/></param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult VoidPayment(this IInvoice invoice, IPayment payment, IPaymentGatewayMethod paymentGatewayMethod, ProcessorArgumentCollection args)
        //{
        //    return paymentGatewayMethod.VoidPayment(invoice, payment, args);
        //}

        ///// <summary>
        ///// Voids a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be voided</param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentGatewayMethod"/> key</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult VoidPayment(this IInvoice invoice, IPayment payment, Guid paymentMethodKey)
        //{
        //    return invoice.VoidPayment(payment, paymentMethodKey, new ProcessorArgumentCollection());
        //}

        ///// <summary>
        ///// Voids a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="payment">The payment to be voided</param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentGatewayMethod"/> key</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //public static IPaymentResult VoidPayment(this IInvoice invoice, IPayment payment, Guid paymentMethodKey, ProcessorArgumentCollection args)
        //{
        //    return invoice.VoidPayment(MerchelloContext.Current, payment, paymentMethodKey, args);
        //}

        ///// <summary>
        ///// Voids a payment
        ///// </summary>
        ///// <param name="invoice">The invoice to be the payment was applied</param>
        ///// <param name="merchelloContext">The <see cref="IMerchelloContext"/></param>
        ///// <param name="payment">The payment to be voided</param>
        ///// <param name="paymentMethodKey">The <see cref="IPaymentGatewayMethod"/> key</param>
        ///// <param name="args">Additional arguments required by the payment processor</param>
        ///// <returns>A <see cref="IPaymentResult"/></returns>
        //internal static IPaymentResult VoidPayment(this IInvoice invoice, IMerchelloContext merchelloContext, IPayment payment, Guid paymentMethodKey, ProcessorArgumentCollection args)
        //{
        //    var paymentGatewayMethod = merchelloContext.Gateways.Payment.GetPaymentGatewayMethodByKey(paymentMethodKey);
        //    return paymentGatewayMethod.VoidPayment(invoice, payment, args);
        //}

        #endregion


        #region Taxation

        /// <summary>
        /// Calculates taxes for the invoice
        /// </summary>
        /// <param name="invoice">
        /// The <see cref="IInvoice"/>
        /// </param>
        /// <param name="quoteOnly">
        /// A value indicating whether or not the taxes should be calculated as a quote
        /// </param>
        /// <returns>
        /// The <see cref="ITaxCalculationResult"/> from the calculation
        /// </returns>
        public static ITaxCalculationResult CalculateTaxes(this IInvoice invoice, bool quoteOnly = true)
        {
            return invoice.CalculateTaxes(invoice.GetBillingAddress(), quoteOnly);
        }

        /// <summary>
        /// Calculates taxes for the invoice
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <param name="taxAddress">The address (generally country code and region) to be used to determine the taxation rates</param>
        /// <param name="quoteOnly">A value indicating whether or not the taxes should be calculated as a quote</param>
        /// <returns>The <see cref="ITaxCalculationResult"/> from the calculation</returns>
        public static ITaxCalculationResult CalculateTaxes(this IInvoice invoice, IAddress taxAddress, bool quoteOnly = true)
        {
            return MC.Gateways.Taxation.CalculateTaxesForInvoice(invoice, taxAddress, quoteOnly);
        }

        #endregion


        #region Totals

        /// <summary>
        /// Sums the total price of invoice items
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        public static Money TotalItemPrice(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Product));
        }

        /// <summary>
        /// Sums the total prices of custom line items.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        public static Money TotalCustomItemPrice(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Custom));
        }

        /// <summary>
        /// Sums the total price of adjustment line items.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        public static Money TotalAdjustmentItemPrice(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Adjustment));
        }

        /// <summary>
        /// Sums the total shipping amount for the invoice items
        /// </summary>
        /// <param name="invoice">
        /// The <see cref="IInvoice"/>
        /// </param>
        /// <returns>
        /// The <see cref="Money"/> total.
        /// </returns>
        public static Money TotalShipping(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Shipping));
        }

        /// <summary>
        /// Sums the total tax amount for the invoice items
        /// </summary>
        /// <param name="invoice">
        /// The <see cref="IInvoice"/>
        /// </param>
        /// <returns>
        /// The <see cref="Money"/> total.
        /// </returns>
        public static Money TotalTax(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Tax));
        }

        /// <summary>
        /// The total discounts.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        public static Money TotalDiscounts(this IInvoice invoice)
        {
            return TotalItems(invoice.Items.Where(x => x.LineItemType == LineItemType.Discount));
        }

        /// <summary>
        /// Totals a collection of items.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        public static Money TotalItems(IEnumerable<ILineItem> items)
        {
            return items.Select(x => x.TotalPrice).Sum();

            //var lineItems = items as ILineItem[] ?? items.ToArray();
            //var total = new Money(0, currencyCode);
            //if (!lineItems.Any()) return new Money(0, currencyCode);
            //total = lineItems.Aggregate(total, (current, li) => current + li.TotalPrice);
            //return total;
        }

        #endregion


        /// <summary>
        /// Ensures the invoice status.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="IInvoiceStatus"/>.
        /// </returns>
        public static IInvoiceStatus EnsureInvoiceStatus(this IInvoice invoice)
        {
            var invoiceService = MC.Services.InvoiceService;

            var appliedTotal = invoice.AppliedPaymentsTotal();

            var statuses = invoiceService.GetAllInvoiceStatuses().ToArray();

            if (invoice.Total > appliedTotal && invoice.InvoiceStatusKey != Constants.InvoiceStatus.Partial)
                invoice.InvoiceStatus = statuses.First(x => x.Key == Constants.InvoiceStatus.Partial);
            if (appliedTotal == 0 && invoice.InvoiceStatusKey != Constants.InvoiceStatus.Unpaid)
                invoice.InvoiceStatus = statuses.First(x => x.Key == Constants.InvoiceStatus.Unpaid);
            if (invoice.Total <= appliedTotal && invoice.InvoiceStatusKey != Constants.InvoiceStatus.Paid)
                invoice.InvoiceStatus = statuses.First(x => x.Key == Constants.InvoiceStatus.Paid);

            if (invoice.IsDirty()) invoiceService.Save(invoice);

            return invoice.InvoiceStatus;
        }

        /// <summary>
        /// Totals the payments applied to an invoice.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="Money"/>.
        /// </returns>
        public static Money AppliedPaymentsTotal(this IInvoice invoice)
        {
            var appliedPayments = invoice.AppliedPayments().ToArray();

            var total = new Money(0, invoice.CurrencyCode);
            total = appliedPayments.Where(x => x.TransactionType == AppliedPaymentType.Debit).Aggregate(total, (current, debit) => current + debit.Amount);
            total = appliedPayments.Where(x => x.TransactionType == AppliedPaymentType.Credit).Aggregate(total, (current, credit) => current - credit.Amount);

            return total;
        }

        /// <summary>
        /// The get invoice status JSON.
        /// </summary>
        /// <param name="invoiceStatus">
        /// The invoice status.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetInvoiceStatusJson(IInvoiceStatus invoiceStatus)
        {
            return JsonConvert.SerializeObject(
                    new
                    {
                        key = invoiceStatus.Key,
                        name = invoiceStatus.Name,
                        alias = invoiceStatus.Alias,
                        reportable = invoiceStatus.Reportable,
                        active = invoiceStatus.Active,
                        sortOrder = invoiceStatus.SortOrder
                    },
                        Formatting.None);
        }

        /// <summary>
        /// The get currency JSON.
        /// </summary>
        /// <param name="currency">
        /// The currency.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetCurrencyJson(Currency currency)
        {

            return JsonConvert.SerializeObject(
                new { currency.EnglishName, currency.Code, currency.Symbol },
                Formatting.None);
        }


        /// <summary>
        /// The get generic items collection.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetGenericItemsCollection(IEnumerable<ILineItem> items)
        {
            return JsonConvert.SerializeObject(
                items.Select(x =>
                    new
                    {
                        key = x.Key,
                        containerKey = x.ContainerKey,
                        name = x.Name,
                        lineItemTfKey = x.LineItemTfKey,
                        lineItemType = x.LineItemType.ToString(),
                        lineItemTypeField = (TypeField)x.GetTypeField(),
                        sku = x.Sku,
                        price = x.Price,
                        quantity = x.Quantity,
                        exported = x.Exported,
                        extendedData = x.ExtendedData.AsEnumerable()
                    }),
                Formatting.None);
        }
    }
}
