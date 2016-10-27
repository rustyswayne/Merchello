namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents a Gateway Provider 
    /// </summary>
    public interface IGatewayProvider : IHasExtendedData
    {
        /// <summary>
        /// Gets the unique key for the gateway.  
        /// Used by Merchello in the GatewayProvider's installation/configuration
        /// </summary>
        Guid Key { get; }

        /// <summary>
        /// Gets the <see cref="IGatewayProviderService"/>.
        /// </summary>
        IGatewayProviderService GatewayProviderService { get; }

        /// <summary>
        /// Gets the <see cref="IGatewayProviderSettings"/>.
        /// </summary>
        IGatewayProviderSettings GatewayProviderSettings { get; }

        /// <summary>
        /// Gets a value indicating whether provider is activated (in use).
        /// </summary>
        bool Activated { get; }

        /// <summary>
        /// Gets the currently configured currency code.
        /// </summary>
        string CurrencyCode { get; }

        /// <summary>
        /// Returns a collection of all possible gateway methods associated with this provider
        /// </summary>
        /// <returns>
        /// A collection of <see cref="IGatewayResource"/>
        /// </returns>
        IEnumerable<IGatewayResource> ListResourcesOffered();
    }
}