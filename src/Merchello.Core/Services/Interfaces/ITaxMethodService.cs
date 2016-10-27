namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="ITaxMethod"/>.
    /// </summary>
    public interface ITaxMethodService : IService
    {
        /// <summary>
        /// Gets a <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="key">The unique 'key' (Guid) of the <see cref="ITaxMethod"/></param>
        /// <returns><see cref="ITaxMethod"/></returns>
        ITaxMethod GetTaxMethodByKey(Guid key);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{ITaxMethod}"/>.
        /// </returns>
        IEnumerable<ITaxMethod> GetAll();

        /// <summary>
        /// Gets a <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the <see cref="IGatewayProviderSettings"/></param>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns><see cref="ITaxMethod"/></returns>
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
        /// Gets a <see cref="ITaxMethod"/> based on a provider and country code
        /// </summary>
        /// <param name="countryCode">The country code of the <see cref="ITaxMethod"/></param>
        /// <returns><see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByCountryCode(string countryCode);

        /// <summary>
        /// Gets a collection of <see cref="ITaxMethod"/> for a given TaxationGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the TaxationGatewayProvider</param>
        /// <returns>A collection of <see cref="ITaxMethod"/></returns>
        IEnumerable<ITaxMethod> GetTaxMethodsByProviderKey(Guid providerKey);

        /// <summary>
        /// Create a <see cref="ITaxMethod"/> for a given provider and country.  If the provider already 
        /// defines a tax rate for the country, the creation fails.
        /// </summary>
        /// <param name="providerKey">
        /// The unique 'key' (GUID) of the TaxationGatewayProvider
        /// </param>
        /// <param name="countryCode">
        /// The two character ISO country code
        /// </param>
        /// <param name="percentageTaxRate">
        /// The tax rate in percentage for the country
        /// </param>
        /// <returns>
        /// The <see cref="ITaxMethod"/>.
        /// </returns>
        ITaxMethod CreateTaxMethodWithKey(Guid providerKey, string countryCode, decimal percentageTaxRate);

        /// <summary>
        /// Saves a single <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be saved</param>
        void Save(ITaxMethod taxMethod);

        /// <summary>
        /// Saves a collection of <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="countryTaxRateList">A collection of <see cref="ITaxMethod"/> to be saved</param>
        void Save(IEnumerable<ITaxMethod> countryTaxRateList);

        /// <summary>
        /// Deletes a single <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethod">The <see cref="ITaxMethod"/> to be deleted</param>
        void Delete(ITaxMethod taxMethod);

        /// <summary>
        /// Deletes a collection <see cref="ITaxMethod"/>
        /// </summary>
        /// <param name="taxMethods">The collection of <see cref="ITaxMethod"/> to be deleted</param>
        void Delete(IEnumerable<ITaxMethod> taxMethods);
    }
}