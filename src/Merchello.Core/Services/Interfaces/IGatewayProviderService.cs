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
    public interface IGatewayProviderService : IService<IGatewayProviderSettings>
    {
        #region GatewayProvider

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/> by its type (Shipping, Taxation, Payment)
        /// </summary>
        /// <param name="gatewayProviderType">The <see cref="GatewayProviderType"/></param>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByType(GatewayProviderType gatewayProviderType);

        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/> by ship country
        /// </summary>
        /// <param name="shipCountry">The <see cref="IShipCountry"/></param>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByShipCountry(IShipCountry shipCountry);

        /// <summary>
        /// Gets a collection containing all <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <returns>A collection of <see cref="IGatewayProviderSettings"/></returns>
        IEnumerable<IGatewayProviderSettings> GetAllGatewayProviders();

        #endregion

        #region AppliedPayments

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

        /// <summary>
        /// Saves a single <see cref="IAppliedPayment"/>
        /// </summary>
        /// <param name="appliedPayment">The <see cref="IAppliedPayment"/> to be saved</param>
        void Save(IAppliedPayment appliedPayment);

        #endregion

        #region Invoice

        /// <summary>
        /// Saves a single <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/> to save</param>
        void Save(IInvoice invoice);

        #endregion

        #region PaymentMethod

        /// <summary>
        /// Creates a <see cref="IPaymentMethod"/> for a given provider.  If the provider already 
        /// defines a paymentCode, the creation fails.
        /// </summary>
        /// <param name="providerKey">The unique 'key' (Guid) of the TaxationGatewayProvider</param>
        /// <param name="name">The name of the payment method</param>
        /// <param name="description">The description of the payment method</param>
        /// <param name="paymentCode">The unique 'payment code' associated with the payment method.  (e.g. visa, mc)</param>
        /// <returns><see cref="Attempt"/> indicating whether or not the creation of the <see cref="IPaymentMethod"/> with respective success or fail</returns>
        IPaymentMethod CreatePaymentMethodWithKey(Guid providerKey, string name, string description, string paymentCode);

        /// <summary>
        /// Saves a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be saved</param>        
        void Save(IPaymentMethod paymentMethod);

        /// <summary>
        /// Deletes a single <see cref="IPaymentMethod"/>
        /// </summary>
        /// <param name="paymentMethod">The <see cref="IPaymentMethod"/> to be deleted</param>        
        void Delete(IPaymentMethod paymentMethod);

        /// <summary>
        /// Gets a collection of <see cref="IPaymentMethod"/> for a given PaymentGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the PaymentGatewayProvider</param>
        /// <returns>A collection of <see cref="IPaymentMethod"/></returns>
        IEnumerable<IPaymentMethod> GetPaymentMethodsByProviderKey(Guid providerKey);

        #endregion

        #region Payment

        /// <summary>
        /// Creates a payment without saving it to the database
        /// </summary>
        /// <param name="paymentMethodType">The type of the payment method</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional payment method key</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePayment(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

        /// <summary>
        /// Creates and saves a payment
        /// </summary>
        /// <param name="paymentMethodType">The type of the payment method</param>
        /// <param name="amount">The amount of the payment</param>
        /// <param name="paymentMethodKey">The optional payment method key</param>
        /// <returns>Returns <see cref="IPayment"/></returns>
        IPayment CreatePaymentWithKey(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey);

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
        /// Gets a collection of <see cref="IPayment"/> for a given invoice
        /// </summary>
        /// <param name="invoiceKey">The unique 'key' of the invoice</param>
        /// <returns>A collection of <see cref="IPayment"/></returns>
        IEnumerable<IPayment> GetPaymentsForInvoice(Guid invoiceKey);

        #endregion

        #region Notification

        /// <summary>
        /// Creates a <see cref="INotificationMethod"/> and saves it to the database
        /// </summary>
        /// <param name="providerKey">The <see cref="IGatewayProviderSettings"/> key</param>
        /// <param name="name">The name of the notification (used in back office)</param>
        /// <param name="serviceCode">The notification service code</param>        
        /// <returns>An Attempt{<see cref="INotificationMethod"/>}</returns>
        Attempt<INotificationMethod> CreateNotificationMethodWithKey(Guid providerKey, string name, string serviceCode);

        /// <summary>
        /// Saves a <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="method">The <see cref="INotificationMethod"/> to be saved</param>
        void Save(INotificationMethod method);

        /// <summary>
        /// Deletes a <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="method">The <see cref="INotificationMethod"/> to be deleted</param>
        void Delete(INotificationMethod method);

        /// <summary>
        /// Creates a <see cref="INotificationMessage"/> and saves it to the database
        /// </summary>
        /// <param name="methodKey">The <see cref="INotificationMethod"/> key</param>
        /// <param name="name">The name of the message (primarily used in the back office UI)</param>
        /// <param name="description">The description of the message (primarily used in the back office UI)</param>
        /// <param name="fromAddress">The senders or "from" address</param>
        /// <param name="recipients">A collection of recipient address</param>
        /// <param name="bodyText">The body text of the message</param>
        /// <returns>The <see cref="INotificationMessage"/></returns>
        INotificationMessage CreateNotificationMessageWithKey(Guid methodKey, string name, string description, string fromAddress, IEnumerable<string> recipients, string bodyText);

        /// <summary>
        /// Saves a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to save</param>
        void Save(INotificationMessage message);

        /// <summary>
        /// Deletes a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to be deleted</param>
        void Delete(INotificationMessage message);

        /// <summary>
        /// Gets a collection of <see cref="INotificationMethod"/> for a give NotificationGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the NotificationGatewayProvider</param>
        /// <returns>A collection of <see cref="INotificationMethod"/></returns>
        IEnumerable<INotificationMethod> GetNotificationMethodsByProviderKey(Guid providerKey);

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/> associated with a <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="notificationMethodKey">The key (Guid) of the <see cref="INotificationMethod"/></param>
        /// <returns>A collection of <see cref="INotificationMessage"/></returns>
        IEnumerable<INotificationMessage> GetNotificationMessagesByMethodKey(Guid notificationMethodKey);

        /// <summary>
        /// Gets a <see cref="INotificationMethod"/> by it's unique key(Guid)
        /// </summary>
        /// <param name="notificationMessageKey">The unique key (Guid) of the <see cref="INotificationMessage"/></param>
        /// <returns>A <see cref="INotificationMessage"/></returns>
        INotificationMessage GetNotificationMessageByKey(Guid notificationMessageKey);

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/>s based on a monitor key
        /// </summary>
        /// <param name="monitorKey">The Notification Monitor Key (Guid)</param>
        /// <returns>A collection of <see cref="INotificationMessage"/></returns>
        IEnumerable<INotificationMessage> GetNotificationMessagesByMonitorKey(Guid monitorKey);


        #endregion

        #region ShipMethod

        /// <summary>
        /// Creates a <see cref="IShipMethod"/>.  This is useful due to the data constraint
        /// preventing two ShipMethods being created with the same ShipCountry and ServiceCode for any provider.
        /// </summary>
        /// <param name="providerKey">
        /// The unique gateway provider key (Guid)
        /// </param>
        /// <param name="shipCountry">
        /// The <see cref="IShipCountry"/> this ship method is to be associated with
        /// </param>
        /// <param name="name">
        /// The required name of the <see cref="IShipMethod"/>
        /// </param>
        /// <param name="serviceCode">
        /// The ShipMethods service code
        /// </param>
        /// <returns>
        /// The <see cref="IShipMethod"/>.
        /// </returns>
        IShipMethod CreateShipMethodWithKey(Guid providerKey, IShipCountry shipCountry, string name, string serviceCode);

        /// <summary>
        /// Saves a single <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod">The <see cref="IShipMethod"/></param>
        void Save(IShipMethod shipMethod);

        /// <summary>
        /// Saves a collection of <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethodList">Collection of <see cref="IShipMethod"/></param>
        void Save(IEnumerable<IShipMethod> shipMethodList);

        /// <summary>
        /// Deletes a <see cref="IShipMethod"/>
        /// </summary>
        /// <param name="shipMethod">The <see cref="IShipMethod"/></param>
        void Delete(IShipMethod shipMethod);

        /// <summary>
        /// Gets a list of <see cref="IShipMethod"/> objects given a <see cref="IGatewayProviderSettings"/> key and a <see cref="IShipCountry"/> key
        /// </summary>
        /// <param name="providerKey">
        /// The provider Key.
        /// </param>
        /// <param name="shipCountryKey">
        /// The ship Country Key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipMethod"/>
        /// </returns>
        IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey, Guid shipCountryKey);

        /// <summary>
        /// Gets a list of all <see cref="IShipMethod"/> objects given a <see cref="IGatewayProviderSettings"/> key
        /// </summary>
        /// <param name="providerKey">
        /// The provider Key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipMethod"/>
        /// </returns>
        IEnumerable<IShipMethod> GetShipMethodsByShipCountryKey(Guid providerKey);

        /// <summary>
        /// Gets a <see cref="IShipMethod"/> by it's unique key
        /// </summary>
        /// <param name="shipMethodKey">The <see cref="IShipMethod"/> key</param>
        /// <returns>A <see cref="IShipMethod"/></returns>
        IShipMethod GetShipMethodByKey(Guid shipMethodKey);

        /// <summary>
        /// Gets all <see cref="IShipMethod"/>s.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IShipMethod}"/>s.
        /// </returns>
        IEnumerable<IShipMethod> GetAllShipMethods();

        #endregion

        #region ShipRateTier

        /// <summary>
        /// Saves a single <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTier">The <see cref="IShipRateTier"/></param>
        void Save(IShipRateTier shipRateTier);

        /// <summary>
        /// Saves a collection of <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTierList">The collection of <see cref="IShipRateTier"/></param>
        void Save(IEnumerable<IShipRateTier> shipRateTierList);

        /// <summary>
        /// Deletes a <see cref="IShipRateTier"/>
        /// </summary>
        /// <param name="shipRateTier">The <see cref="IShipRateTier"/></param>
        void Delete(IShipRateTier shipRateTier);

        /// <summary>
        /// Gets a list of <see cref="IShipRateTier"/> objects given a <see cref="IShipMethod"/> key
        /// </summary>
        /// <param name="shipMethodKey">The ship method key</param>
        /// <returns>A collection of <see cref="IShipRateTier"/></returns>
        IEnumerable<IShipRateTier> GetShipRateTiersByShipMethodKey(Guid shipMethodKey);

        #endregion

        #region ShipCountry

        /// <summary>
        /// Gets a <see cref="IShipCountry"/> by it's unique key (Guid)
        /// </summary>
        /// <param name="shipCountryKey">The unique key of the <see cref="IShipCountry"/></param>
        /// <returns>The <see cref="IShipCountry"/></returns>
        IShipCountry GetShipCountryByKey(Guid shipCountryKey);

        /// <summary>
        /// Gets a <see cref="IShipCountry"/> by CatalogKey and CountryCode
        /// </summary>
        /// <param name="catalogKey">The unique key of the <see cref="IWarehouseCatalog"/></param>
        /// <param name="countryCode">The two character ISO country code</param>
        /// <returns>An <see cref="IShipCountry"/></returns>
        IShipCountry GetShipCountry(Guid catalogKey, string countryCode);

        /// <summary>
        /// Returns a collection of all <see cref="IShipCountry"/>
        /// </summary>
        /// <returns>A collection of all <see cref="IShipCountry"/></returns>
        IEnumerable<IShipCountry> GetAllShipCountries();

        #endregion

        #region Statuses


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

        #endregion

        #region Taxmethod

        /// <summary>
        /// Attempts to create a <see cref="ITaxMethod"/> for a given provider and country.  If the provider already 
        /// defines a tax rate for the country, the creation fails.
        /// </summary>
        /// <param name="providerKey">The unique 'key' (GUID) of the TaxationGatewayProvider</param>
        /// <param name="countryCode">The two character ISO country code</param>
        /// <param name="percentageTaxRate">The tax rate in percentage for the country</param>
        /// <returns><see cref="Attempt"/> indicating whether or not the creation of the <see cref="ITaxMethod"/> with respective success or fail</returns>
        Attempt<ITaxMethod> CreateTaxMethodWithKey(Guid providerKey, string countryCode, decimal percentageTaxRate);

        /// <summary>
        /// Gets a <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the <see cref="IGatewayProviderSettings"/></param>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns>A collection <see cref="ITaxMethod"/></returns>
        ITaxMethod GetTaxMethodByCountryCode(Guid providerKey, string countryCode);

        /// <summary>
        /// Get tax method for product pricing.
        /// </summary>
        /// <returns>
        /// The <see cref="ITaxMethod"/> or null if no tax method is found
        /// </returns>
        /// <remarks>
        /// There can be only one =)
        /// </remarks>
        ITaxMethod GetTaxMethodForProductPricing();

        /// <summary>
        /// Gets a collection of <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns><see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByCountryCode(string countryCode);

        /// <summary>
        /// Saves a single <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be saved</param>        
        void Save(ITaxMethod taxMethod);

        /// <summary>
        /// Deletes a <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be deleted</param>
        void Delete(ITaxMethod taxMethod);

        /// <summary>
        /// Gets a collection of <see cref="ITaxMethod"/> for a given TaxationGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the TaxationGatewayProvider</param>
        /// <returns>A collection of <see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByProviderKey(Guid providerKey);

        #endregion

        #region Warehouse

        /// <summary>
        /// Gets the default <see cref="IWarehouse"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IWarehouse"/>.
        /// </returns>
        IWarehouse GetDefaultWarehouse();


        #endregion
    }
}