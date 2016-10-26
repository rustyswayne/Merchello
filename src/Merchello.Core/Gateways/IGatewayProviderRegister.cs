namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DI;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a register for resolved <see cref="IGatewayProvider"/>.
    /// </summary>
    public interface IGatewayProviderRegister : IRegister<Type>
    {
        /// <summary>
        /// Gets a collection of <see cref="IGatewayProviderSettings"/>s by type
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="GatewayProviderBase"/>
        /// </typeparam>
        /// <returns>
        /// The collection of activated <see cref="IGatewayProvider"/>s
        /// </returns>
        IEnumerable<IGatewayProvider> GetActivatedProviders<T>() where T : IGatewayProvider;

        /// <summary>
        /// Gets a collection of all "activated" providers regardless of type
        /// </summary>
        /// <returns>
        /// The collection of all "activated" providers.
        /// </returns>
        IEnumerable<IGatewayProvider> GetActivatedProviders();

        /// <summary>
        /// Gets a collection of all providers regardless of type
        /// </summary>
        /// <returns>
        /// The collection of all providers.
        /// </returns>
        IEnumerable<IGatewayProvider> GetAllProviders();

        /// <summary>
        /// Gets a collection of inactive (not saved) <see cref="IGatewayProviderSettings"/> by type
        /// </summary>
        /// <typeparam name="T">
        /// The type of <see cref="GatewayProviderBase"/>
        /// </typeparam>
        /// <returns>
        /// The collection of inactive (not saved) <see cref="IGatewayProviderSettings"/>.
        /// </returns>
        IEnumerable<IGatewayProvider> GetAllProviders<T>() where T : IGatewayProvider;

        /// <summary>
        /// Instantiates a GatewayProvider given its registered Key
        /// </summary>
        /// <typeparam name="T">The Type of the GatewayProvider.  Must inherit from GatewayProviderBase</typeparam>
        /// <param name="gatewayProviderKey">The gateway provider key</param>
        /// <param name="activatedOnly">Search only activated providers</param>
        /// <returns>An instantiated GatewayProvider</returns>
        T GetProviderByKey<T>(Guid gatewayProviderKey, bool activatedOnly = true) where T : IGatewayProvider;

        /// <summary>
        /// Refreshes the <see cref="IGatewayProvider"/> cache
        /// </summary>
        void RefreshCache();
    }
}