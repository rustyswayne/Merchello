namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Shipping;

    using Models;
    using Services;

    /// <summary>
    /// Defines the GatewayBase
    /// </summary>
    public abstract class GatewayProviderBase : IGatewayProvider
    {
        #region Fields

        /// <summary>
        /// The gateway provider settings.
        /// </summary>
        private readonly IGatewayProviderSettings _gatewayProviderSettings;

        /// <summary>
        /// The currency code currently configured.
        /// </summary>
        private Lazy<string> _currencyCode;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderBase"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The <see cref="IGatewayProviderService"/>
        /// </param>
        /// <param name="gatewayProviderSettings">
        /// The <see cref="IGatewayProviderSettings"/>
        /// </param>
        /// <param name="runtimeCacheProvider">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>
        /// </param>
        protected GatewayProviderBase(IGatewayProviderService gatewayProviderService, IGatewayProviderSettings gatewayProviderSettings, IRuntimeCacheProviderAdapter runtimeCacheProvider)
        {
            Ensure.ParameterNotNull(gatewayProviderService, "gatewayProviderService");
            Ensure.ParameterNotNull(gatewayProviderSettings, "gatewayProvider");
            Ensure.ParameterNotNull(runtimeCacheProvider, "runtimeCacheProvider");

            this.GatewayProviderService = gatewayProviderService;
            _gatewayProviderSettings = gatewayProviderSettings;
            this.RuntimeCache = runtimeCacheProvider;

            this.Initialize();
        }


        /// <summary>
        /// Gets the <see cref="IGatewayProviderService"/>
        /// </summary>
        public IGatewayProviderService GatewayProviderService { get; }

        /// <summary>
        /// Gets the unique Key that will be used
        /// </summary>
        public Guid Key => this._gatewayProviderSettings.Key;

        /// <inheritdoc/>
        public IGatewayProviderSettings GatewayProviderSettings => this._gatewayProviderSettings;

        /// <summary>
        /// Gets the ExtendedData collection from the <see cref="IGatewayProviderSettings"/>
        /// </summary>
        public ExtendedDataCollection ExtendedData => this._gatewayProviderSettings.ExtendedData;


        /// <inheritdoc/>
        public bool Activated => this._gatewayProviderSettings.Activated;

        /// <inheritdoc/>
        public string CurrencyCode => _currencyCode.Value;

        /// <summary>
        /// Gets the RuntimeCache
        /// </summary>
        /// <returns></returns>
        protected IRuntimeCacheProviderAdapter RuntimeCache { get; }

        /// <summary>
        /// Returns a collection of all possible gateway methods associated with this provider
        /// </summary>
        /// <returns>A collection of <see cref="IGatewayResource"/></returns>
        public abstract IEnumerable<IGatewayResource> ListResourcesOffered();

        /// <summary>
        /// Gets an instance of the <see cref="IShipmentRateQuoteStrategy"/>.
        /// </summary>
        /// <param name="shipment">
        /// The shipment.
        /// </param>
        /// <param name="shipMethods">
        /// The ship methods.
        /// </param>
        /// <returns>
        /// The <see cref="IShipmentRateQuoteStrategy"/>.
        /// </returns>
        protected IShipmentRateQuoteStrategy GetRateQuoteStrategy(IShipment shipment, IShippingGatewayMethod[] shipMethods)
        {
            return MC.Container.GetInstance<IShipment, IShippingGatewayMethod[], IShipmentRateQuoteStrategy>(shipment, shipMethods);
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        private void Initialize()
        {
            _currencyCode = new Lazy<string>(() => GatewayProviderService.GetDefaultCurrencyCode());
        }
    }
}