namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a GatewayContext for a specific provider type
    /// </summary>
    /// <typeparam name="T">The type of providers in the context</typeparam>
    public interface IGatewayProviderTypedContextBase<out T> where T : GatewayProviderBase
    {
        /// <summary>
        /// Lists all available <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <returns>A collection of all GatewayProvider of the particular type T</returns>
        IEnumerable<GatewayProviderBase> GetAllActivatedProviders();

        /// <summary>
        /// Lists all available providers.  This list includes providers that are just resolved and not configured
        /// </summary>
        /// <returns>A collection of all gateway providers</returns>
        IEnumerable<GatewayProviderBase> GetAllProviders();

        /// <summary>
        /// Instantiates a GatewayProvider given its registered Key
        /// </summary>
        /// <param name="gatewayProviderKey">
        /// The key identifier (GUID) for the provider.
        /// </param>
        /// <param name="activatedOnly">
        /// Search only activated providers
        /// </param>
        /// <returns>
        /// An instantiated gateway provider
        /// </returns>
        T GetProviderByKey(Guid gatewayProviderKey, bool activatedOnly = true);

        /// <summary>
        /// Returns an instance of an 'active' GatewayProvider associated with a GatewayMethod based given the unique Key (Guid) of the GatewayMethod
        /// </summary>
        /// <param name="gatewayMethodKey">The unique key (Guid) of the <see cref="IGatewayMethod"/></param>
        /// <returns>An instantiated GatewayProvider</returns>
        T GetProviderByMethodKey(Guid gatewayMethodKey);

        /// <summary>
        /// Activates a GatewayProvider
        /// </summary>
        /// <param name="provider">The GatewayProvider</param>
        void ActivateProvider(GatewayProviderBase provider);

        /// <summary>
        /// Activates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="gatewayProviderSettings">The <see cref="IGatewayProviderSettings"/> to be activated</param>
        void ActivateProvider(IGatewayProviderSettings gatewayProviderSettings);

        /// <summary>
        /// Activates a GatewayProvider
        /// </summary>
        /// <param name="provider">The GatewayProvider</param>
        void DeactivateProvider(GatewayProviderBase provider);

        /// <summary>
        /// Deactivates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="gatewayProviderSettings">The <see cref="IGatewayProviderSettings"/> to be deactivated</param>
        void DeactivateProvider(IGatewayProviderSettings gatewayProviderSettings);
    }
}