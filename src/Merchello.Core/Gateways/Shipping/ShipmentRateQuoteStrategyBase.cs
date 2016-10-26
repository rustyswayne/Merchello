namespace Merchello.Core.Gateways.Shipping
{
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a Shipment Rate Quote Strategy
    /// </summary>
    public abstract class ShipmentRateQuoteStrategyBase : IShipmentRateQuoteStrategy
    {
        /// <summary>
        /// The runtime cache.
        /// </summary>
        private readonly IRuntimeCacheProviderAdapter _runtimeCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentRateQuoteStrategyBase"/> class.
        /// </summary>
        /// <param name="runtimeCache">
        /// The runtime cache.
        /// </param>
        /// <param name="shipment">
        /// The shipment.
        /// </param>
        /// <param name="shippingGatewayMethods">
        /// The shipping gateway methods.
        /// </param>
        protected ShipmentRateQuoteStrategyBase(IRuntimeCacheProviderAdapter runtimeCache, IShipment shipment, IShippingGatewayMethod[] shippingGatewayMethods)
        {
            Ensure.ParameterNotNull(shipment, "shipment");
            Ensure.ParameterNotNull(shippingGatewayMethods, "gatewayShipMethods");
            Ensure.ParameterNotNull(runtimeCache, "runtimeCache");

            this.Shipment = shipment;
            this.ShippingGatewayMethods = shippingGatewayMethods;
            _runtimeCache = runtimeCache;
        }

        /// <summary>
        /// Gets the collection of <see cref="ShippingGatewayMethodBase"/>
        /// </summary>
        protected IEnumerable<IShippingGatewayMethod> ShippingGatewayMethods { get; }

        /// <summary>
        /// Gets the <see cref="IShipment"/>
        /// </summary>
        protected IShipment Shipment { get; }

        /// <summary>
        /// Gets the <see cref="IRuntimeCacheProviderAdapter"/>
        /// </summary>
        protected IRuntimeCacheProviderAdapter RuntimeCache => this._runtimeCache;

        /// <summary>
        /// Quotes all available ship methods
        /// </summary>
        /// <param name="tryGetCached">
        /// If set true the strategy will try to get a quote from cache
        /// </param>
        /// <returns>
        /// A collection of <see cref="IShipmentRateQuote"/>
        /// </returns>
        public abstract IEnumerable<IShipmentRateQuote> GetShipmentRateQuotes(bool tryGetCached = true);

        /// <summary>
        /// Creates a cache key for caching <see cref="IShipmentRateQuote"/>s
        /// </summary>
        /// <param name="shipment">
        /// The shipment.
        /// </param>
        /// <param name="shippingGatewayMethod">
        /// The shipping Gateway Method.
        /// </param>
        /// <returns>
        /// The cache key.
        /// </returns>
        protected static string GetShipmentRateQuoteCacheKey(IShipment shipment, IShippingGatewayMethod shippingGatewayMethod)
        {
            var address = shipment.GetDestinationAddress();
            var args = address.Region.IsNullOrWhiteSpace() ? 
                address.CountryCode : $"{address.Region.Replace(" ", string.Empty)}.{address.CountryCode}";
            return Cache.CacheKeys.ShippingGatewayProviderShippingRateQuoteCacheKey(shipment.Key, shippingGatewayMethod.ShipMethod.Key, shipment.VersionKey, args);
        }

        /// <summary>
        /// Returns the cached <see cref="IShipmentRateQuote"/> if it exists
        /// </summary>
        /// <param name="shipment">
        /// The shipment.
        /// </param>
        /// <param name="shippingGatewayMethod">
        /// The shipping Gateway Method.
        /// </param>
        /// <returns>
        /// The <see cref="IShipmentRateQuote"/>.
        /// </returns>
        protected IShipmentRateQuote TryGetCachedShipmentRateQuote(IShipment shipment, IShippingGatewayMethod shippingGatewayMethod)
        {
            return _runtimeCache.GetCacheItem(GetShipmentRateQuoteCacheKey(shipment, shippingGatewayMethod)) as ShipmentRateQuote;
        }
    }
}