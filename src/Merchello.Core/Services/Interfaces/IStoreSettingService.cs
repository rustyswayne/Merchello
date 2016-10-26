namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Models.TypeFields;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IStoreSetting"/>.
    /// </summary>
    public interface IStoreSettingService : IService<IStoreSetting>
    {
        /// <summary>
        /// Gets a collection of all store settings.
        /// </summary>
        /// <param name="keys">
        /// Optional keys to limit the query.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IStoreSetting"/>.
        /// </returns>
        IEnumerable<IStoreSetting> GetAll(params Guid[] keys);

        /// <summary>
        /// Gets a collection of all store settings for a store.
        /// </summary>
        /// <param name="storeKey">
        /// Limit to a particular store.
        /// </param>
        /// <param name="excludeGlobal">
        /// A value indicating that global settings should be excluded.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IStoreSetting"/>.
        /// </returns>
        IEnumerable<IStoreSetting> GetByStoreKey(Guid storeKey, bool excludeGlobal = false);

        /// <summary>
        /// Returns the <see cref="ICountry"/> for the country code passed.
        /// </summary>
        /// <param name="countryCode">The two letter ISO Region code (country code)</param>
        /// <returns><see cref="ICountry"/> for the country corresponding the the country code passed</returns>
        ICountry GetCountryByCode(string countryCode);

        /// <summary>
        /// Gets a collection of all <see cref="ICountry"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ICountry}"/>.
        /// </returns>
        IEnumerable<ICountry> GetAllCountries();

        /// <summary>
        /// Gets a collection of all <see cref="Currency"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ICurrency}"/>.
        /// </returns>
        IEnumerable<Currency> GetAllCurrencies();

        /// <summary>
        /// Gets a <see cref="Currency"/> for the currency code passed
        /// </summary>
        /// <param name="currencyCode">The ISO Currency Code (ex. USD)</param>
        /// <returns>The <see cref="Currency"/></returns>
        Currency GetCurrencyByCode(string currencyCode);

        /// <summary>
        /// Gets currency by country code.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// The <see cref="Currency"/>.
        /// </returns>
        Currency GetCurrencyByCountryCode(string countryCode);

        /// <summary>
        /// Returns a <see cref="ICountry"/> collection for all countries excluding codes passed
        /// </summary>
        /// <param name="excludeCountryCodes">A collection of country codes to exclude from the result set</param>
        /// <returns>A collection of <see cref="ICountry"/></returns>
        IEnumerable<ICountry> GetAllCountries(string[] excludeCountryCodes);

        /// <summary>
        /// Gets the next usable InvoiceNumber
        /// </summary>
        /// <param name="invoicesCount">
        /// The number of invoices.
        /// </param>
        /// <returns>
        /// The next invoice number
        /// </returns>
        int GetNextInvoiceNumber(int invoicesCount = 1);

        /// <summary>
        /// Gets the next usable OrderNumber
        /// </summary>
        /// <param name="ordersCount">The number of orders</param>
        /// <returns>The next order number</returns>
        int GetNextOrderNumber(int ordersCount = 1);

        /// <summary>
        /// Gets the next usable ShipmentNumber.
        /// </summary>
        /// <param name="shipmentsCount">
        /// The shipments count.
        /// </param>
        /// <returns>
        /// The next shipment number.
        /// </returns>
        int GetNextShipmentNumber(int shipmentsCount = 1);

        /// <summary>
        /// Gets the complete collection of registered type fields
        /// </summary>
        /// <returns>The collection of <see cref="ITypeField"/></returns>
        IEnumerable<ITypeField> GetTypeFields();
    }
}