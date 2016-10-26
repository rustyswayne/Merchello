namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Services;

    /// <summary>
    /// Base class for GatewayContext objects
    /// </summary>
    /// <typeparam name="T">
    /// The type of the gateway provider
    /// </typeparam>
    public abstract class GatewayProviderTypedContextBase<T> : IGatewayProviderTypedContextBase<T>
        where T : GatewayProviderBase
    {
        /// <summary>
        /// The resolver.
        /// </summary>
        private readonly IGatewayProviderRegister register;

        /// <summary>
        /// The <see cref="IGatewayProviderService"/>.
        /// </summary>
        private readonly Lazy<IGatewayProviderService> _gatewayProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderTypedContextBase{T}"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="register">
        /// The resolver.
        /// </param>
        protected GatewayProviderTypedContextBase(Lazy<IGatewayProviderService> gatewayProviderService, IGatewayProviderRegister register)
        {
            Ensure.ParameterNotNull(gatewayProviderService, "gatewayProviderService");            
            Ensure.ParameterNotNull(register, "resolver");
            _gatewayProviderService = gatewayProviderService;
            this.register = register;
        }

        /// <summary>
        /// Gets the <see cref="IGatewayProviderRegister"/>
        /// </summary>
        protected IGatewayProviderRegister GatewayProviderRegister
        {
            get
            {
                if (this.register == null) throw new InvalidOperationException("GatewayProviderResolver has not been instantiated.");
                return this.register;
            }
        }

        /// <summary>
        /// Gets the GatewayProviderService
        /// </summary>
        protected IGatewayProviderService GatewayProviderService => _gatewayProviderService.Value;

        /// <summary>
        /// Lists all activated <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <returns>A collection of all "activated" GatewayProvider of the particular type T</returns>
        public IEnumerable<IGatewayProvider> GetAllActivatedProviders()
        {
            return this.GatewayProviderRegister.GetActivatedProviders<T>();
        }

        /// <summary>
        /// Lists all available providers.  This list includes providers that are just resolved and not configured
        /// </summary>
        /// <returns>A collection of all GatewayProviders</returns>
        public IEnumerable<IGatewayProvider> GetAllProviders()
        {
            return this.GatewayProviderRegister.GetAllProviders<T>();
        }

        /// <summary>
        /// Instantiates a GatewayProvider given its registered Key
        /// </summary>
        /// <param name="gatewayProviderKey">
        /// The gateway provider key
        /// </param>
        /// <param name="activatedOnly">
        /// Search only activated providers
        /// </param>
        /// <returns>
        /// An instantiated GatewayProvider
        /// </returns>
        public T GetProviderByKey(Guid gatewayProviderKey, bool activatedOnly = true)
        {
            return this.GatewayProviderRegister.GetProviderByKey<T>(gatewayProviderKey, activatedOnly);
        }

        /// <summary>
        /// Returns an instance of an 'active' GatewayProvider associated with a GatewayMethod based given the unique Key (GUID) of the GatewayMethod
        /// </summary>
        /// <param name="gatewayMethodKey">The unique key (GUID) of the <see cref="IGatewayMethod"/></param>
        /// <returns>An instantiated GatewayProvider</returns>
        public abstract T GetProviderByMethodKey(Guid gatewayMethodKey);

        /// <summary>
        /// Creates an instance GatewayProvider given its registered Key
        /// </summary>
        /// <param name="gatewayProviderKey">
        /// The gateway Provider Key.
        /// </param>
        /// <returns>
        /// An instance of the gateway provider.
        /// </returns>
        [Obsolete("Use GetProviderByKey instead")]
        public T CreateInstance(Guid gatewayProviderKey)
        {
            return GetProviderByKey(gatewayProviderKey);
        }

        /// <summary>
        /// Activates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="provider">The GatewayProvider to be activated</param>
        public void ActivateProvider(IGatewayProvider provider)
        {
            ActivateProvider(provider.GatewayProviderSettings);
        }

        /// <summary>
        /// Activates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="gatewayProviderSettings">The <see cref="IGatewayProviderSettings"/> to be activated</param>
        public void ActivateProvider(IGatewayProviderSettings gatewayProviderSettings)
        {
            if (gatewayProviderSettings.Activated) return;
            GatewayProviderService.Save(gatewayProviderSettings);
            this.GatewayProviderRegister.RefreshCache();
        }

        /// <summary>
        /// Deactivates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="provider">The GatewayProvider to be deactivated</param>
        public void DeactivateProvider(IGatewayProvider provider)
        {
            DeactivateProvider(provider.GatewayProviderSettings);
        }

        /// <summary>
        /// Deactivates a <see cref="IGatewayProviderSettings"/>
        /// </summary>
        /// <param name="gatewayProviderSettings">The <see cref="IGatewayProviderSettings"/> to be deactivated</param>
        public void DeactivateProvider(IGatewayProviderSettings gatewayProviderSettings)
        {
            if (!gatewayProviderSettings.Activated) return;
            GatewayProviderService.Delete(gatewayProviderSettings);
            this.GatewayProviderRegister.RefreshCache();
        }
    }
}