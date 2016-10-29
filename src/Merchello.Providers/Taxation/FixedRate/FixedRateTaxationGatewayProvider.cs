namespace Merchello.Providers.Taxation.FixedRate
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Cache;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents the CountryTaxRateTaxationGatewayProvider.  
    /// </summary>
    /// <remarks>
    /// 
    /// This is Merchello's default TaxationGatewayProvider
    /// 
    /// </remarks> 
    [GatewayProviderActivation("A4AD4331-C278-4231-8607-925E0839A6CD", "Fixed Rate Tax Provider", "Fixed Rate Tax Provider")]
    public class FixedRateTaxationGatewayProvider : TaxationGatewayProviderBase, IFixedRateTaxationGatewayProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedRateTaxationGatewayProvider"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The <see cref="IGatewayProviderService"/>.
        /// </param>
        /// <param name="gatewayProviderSettings">
        /// The <see cref="IGatewayProviderSettings"/>.
        /// </param>
        /// <param name="runtimeCacheProvider">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        public FixedRateTaxationGatewayProvider(
            IGatewayProviderService gatewayProviderService,
            IGatewayProviderSettings gatewayProviderSettings,
            IRuntimeCacheProviderAdapter runtimeCacheProvider)
            : base(gatewayProviderService, gatewayProviderSettings, runtimeCacheProvider)
        {
        }

        /// <summary>
        /// Creates a <see cref="ITaxationGatewayMethod"/>
        /// </summary>
        /// <param name="countryCode">The two letter ISO Country Code</param>
        /// <param name="taxPercentageRate">The decimal percentage tax rate</param>
        /// <returns>The <see cref="ITaxationGatewayMethod"/></returns>
        public override ITaxationGatewayMethod CreateTaxMethod(string countryCode, decimal taxPercentageRate)
        {
            var attempt = this.ListResourcesOffered().FirstOrDefault(x => x.ServiceCode.Equals(countryCode)) != null
                ? this.GatewayProviderService.CreateTaxMethodWithKey(this.GatewayProviderSettings.Key, countryCode, taxPercentageRate)
                : Attempt<ITaxMethod>.Fail(new ConstraintException("A fixed tax rate method has already been defined for " + countryCode));


            if (attempt.Success)
            {
                return new FixRateTaxationGatewayMethod(attempt.Result);                   
            }

            MultiLogHelper.Error<TaxationGatewayProviderBase>("CreateTaxMethod failed.", attempt.Exception);

            throw attempt.Exception;
        }

        /// <summary>
        /// Gets a <see cref="ITaxMethod"/> by it's unique 'key' (GUID)
        /// </summary>
        /// <param name="countryCode">The two char ISO country code</param>
        /// <returns><see cref="ITaxMethod"/></returns>
        public override ITaxationGatewayMethod GetGatewayTaxMethodByCountryCode(string countryCode)
        {
            var taxMethod = this.FindTaxMethodForCountryCode(countryCode);

            return taxMethod != null ? new FixRateTaxationGatewayMethod(taxMethod) : null;
        }

        /// <summary>
        /// Gets the FixRateTaxationGatewayMethod.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ITaxationByProductMethod"/>.
        /// </returns>
        public ITaxationByProductMethod GetTaxationByProductMethod(Guid key)
        {
            var taxMethod = this.TaxMethods.FirstOrDefault(x => x.Key == key);
            return taxMethod != null ? new FixRateTaxationGatewayMethod(taxMethod) : null;
        }

        /// <summary>
        /// Gets a collection of all <see cref="ITaxMethod"/> associated with this provider
        /// </summary>
        /// <returns>A collection of <see cref="ITaxMethod"/> </returns>
        public override IEnumerable<ITaxationGatewayMethod> GetAllGatewayTaxMethods()
        {
            return this.TaxMethods.Select(taxMethod => new FixRateTaxationGatewayMethod(taxMethod));
        }

        /// <summary>
        /// Returns a collection of all possible gateway methods associated with this provider
        /// </summary>
        /// <returns>A collection of <see cref="IGatewayResource"/></returns>
        public override IEnumerable<IGatewayResource> ListResourcesOffered()
        {
            var countryCodes = this.GatewayProviderService.GetAllShipCountries().Select(x => x.CountryCode).Distinct();

            var resources =
                countryCodes.Select(x => new GatewayResource(x, x + "-FixedRate"))
                    .Where(code => this.TaxMethods.FirstOrDefault(x => x.CountryCode.Equals(code.ServiceCode)) == null);

            return resources;
        }
    }
}