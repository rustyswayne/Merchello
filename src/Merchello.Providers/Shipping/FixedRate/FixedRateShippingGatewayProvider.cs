namespace Merchello.Providers.Shipping.FixedRate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core;
    using Merchello.Core.Cache;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// Defines the RateTableLookupGateway
    /// </summary>
    /// <remarks>
    /// 
    /// This is Merchello's default ShippingGatewayProvider
    /// 
    /// </remarks>
    [GatewayProviderActivation("AEC7A923-9F64-41D0-B17B-0EF64725F576", "Fixed Rate Shipping Provider", "Fixed Rate Shipping Provider")]
    public class FixedRateShippingGatewayProvider : ShippingGatewayProviderBase, IFixedRateShippingGatewayProvider
    {
        /// <summary>
        /// In this case, the GatewayResource can be used to create multiple ship methods of the same resource type.
        /// </summary>
        private static readonly IEnumerable<IGatewayResource> AvailableResources  = new List<IGatewayResource>()
            {
                new GatewayResource(VaryByWeightPrefix, "Vary by Weight"),
                new GatewayResource(PercentOfTotalPrefix, "Vary by Price")
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedRateShippingGatewayProvider"/> class.
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
        public FixedRateShippingGatewayProvider(IGatewayProviderService gatewayProviderService, IGatewayProviderSettings gatewayProviderSettings, IRuntimeCacheProviderAdapter runtimeCacheProvider)
            : base(gatewayProviderService, gatewayProviderSettings, runtimeCacheProvider)
        {            
        }

        /// <summary>
        /// Gets the vary by weight prefix.
        /// </summary>
        public static string VaryByWeightPrefix => "VBW";

        /// <summary>
        /// Gets the percent of total prefix.
        /// </summary>
        public static string PercentOfTotalPrefix => "VBP";

        /// <summary>
        /// Creates an instance of a <see cref="FixedRateShippingGatewayMethod"/>
        /// </summary>
        /// <param name="quoteType">
        /// The quote Type.
        /// </param>
        /// <param name="shipCountry">
        /// The ship Country.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <remarks>
        /// 
        /// This method is really specific to the RateTableShippingGateway due to the odd fact that additional ship methods can be created 
        /// rather than defined up front.  
        /// 
        /// </remarks>
        /// <returns>
        /// The <see cref="IShippingGatewayMethod"/> created
        /// </returns>
        public IShippingGatewayMethod CreateShipMethod(FixedRateShippingGatewayMethod.QuoteType quoteType, IShipCountry shipCountry, string name)
        {
            var resource = quoteType == FixedRateShippingGatewayMethod.QuoteType.VaryByWeight
                ? AvailableResources.First(x => x.ServiceCode == "VBW")
                : AvailableResources.First(x => x.ServiceCode == "VBP");

            return this.CreateShippingGatewayMethod(resource, shipCountry, name);
        }

        /// <summary>
        /// Creates an instance of a <see cref="FixedRateShippingGatewayMethod"/>
        /// </summary>
        /// <param name="gatewayResource">
        /// The gateway Resource.
        /// </param>
        /// <param name="shipCountry">
        /// The ship Country.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IShippingGatewayMethod"/> created
        /// </returns>
        /// <remarks>
        /// 
        /// GatewayShipMethods (in general) should be unique with respect to <see cref="IShipCountry"/> and <see cref="IGatewayResource"/>.  However, this is a
        /// a provider is sort of a unique case, sense we want to be able to add as many ship methods with rate tables as needed in order to facilitate 
        /// tiered rate tables for various ship methods without requiring a carrier based shipping provider.
        /// 
        /// </remarks>
        public override IShippingGatewayMethod CreateShippingGatewayMethod(IGatewayResource gatewayResource, IShipCountry shipCountry, string name)
        {
            Ensure.ParameterNotNull(gatewayResource, "gatewayResource");
            Ensure.ParameterNotNull(shipCountry, "shipCountry");
            Ensure.ParameterNotNullOrEmpty(name, "name");

            var attempt = this.GatewayProviderService.CreateShipMethodWithKey(this.GatewayProviderSettings.Key, shipCountry, name, gatewayResource.ServiceCode + string.Format("-{0}", Guid.NewGuid()));
            
            if (!attempt.Success) throw attempt.Exception;

            return new FixedRateShippingGatewayMethod(gatewayResource, attempt.Result, shipCountry, CurrencyCode);
        }

        /// <summary>
        /// Saves a <see cref="FixedRateShippingGatewayMethod"/> 
        /// </summary>
        /// <param name="shippingGatewayMethod">The <see cref="IShippingGatewayMethod"/> to be saved</param>
        public override void SaveShippingGatewayMethod(IShippingGatewayMethod shippingGatewayMethod)
        {
            this.GatewayProviderService.Save(shippingGatewayMethod.ShipMethod);
            this.ResetShipMethods();
            ShippingFixedRateTable.Save(this.GatewayProviderService, this.RuntimeCache, ((FixedRateShippingGatewayMethod) shippingGatewayMethod).RateTable);
        }

        /// <summary>
        /// Returns a collection of all possible gateway methods associated with this provider
        /// </summary>
        /// <returns>
        /// Returns the collection of <see cref="IGatewayResource"/> defined by this provider
        /// </returns>
        public override IEnumerable<IGatewayResource> ListResourcesOffered()
        {
            return AvailableResources;
        }

        /// <summary>
        /// Returns a collection of ship methods assigned for this specific provider configuration (associated with the ShipCountry)
        /// </summary>
        /// <param name="shipCountry">
        /// The ship Country.
        /// </param>
        /// <returns>
        /// Returns a collection of all <see cref="IShippingGatewayMethod"/>s associated with the ship country
        /// </returns>
        public override IEnumerable<IShippingGatewayMethod> GetAllShippingGatewayMethods(IShipCountry shipCountry)
        {
            var methods = this.GatewayProviderService.GetShipMethods(this.GatewayProviderSettings.Key, shipCountry.Key);
            return methods
                .Select(
                shipMethod => new FixedRateShippingGatewayMethod(
                    AvailableResources.FirstOrDefault(x => shipMethod.ServiceCode.StartsWith(x.ServiceCode)), 
                    shipMethod, 
                    shipCountry, 
                    ShippingFixedRateTable.GetShipRateTable(this.GatewayProviderService, this.RuntimeCache, shipMethod.Key, CurrencyCode),
                    CurrencyCode))
                .OrderBy(x => x.ShipMethod.Name);
        }
    }
}