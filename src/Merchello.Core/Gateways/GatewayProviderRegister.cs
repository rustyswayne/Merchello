namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;

    /// <inheritdoc/>
    internal class GatewayProviderRegister : Register<Type>, IGatewayProviderRegister
    {
        /// <summary>
        /// maintain our own index for faster lookup
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IGatewayProvider> _activatedCache = new ConcurrentDictionary<Guid, IGatewayProvider>();

        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderRegister"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        public GatewayProviderRegister(IServiceContainer container, IEnumerable<Type> items)
            : base(items)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetActivatedProviders<T>() where T : IGatewayProvider
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetActivatedProviders()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetAllProviders()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetAllProviders<T>() where T : IGatewayProvider
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public T GetProviderByKey<T>(Guid gatewayProviderKey, bool activatedOnly = true) where T : IGatewayProvider
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void RefreshCache()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps the type of T to a <see cref="GatewayProviderType"/>
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// Returns a <see cref="GatewayProviderType"/>
        /// </returns>
        internal static GatewayProviderType GetGatewayProviderType(Type type)
        {
            if (typeof(IPaymentGatewayProvider).IsAssignableFrom(type)) return GatewayProviderType.Payment;
            if (typeof(INotificationGatewayProvider).IsAssignableFrom(type)) return GatewayProviderType.Notification;
            if (typeof(IShippingGatewayProvider).IsAssignableFrom(type)) return GatewayProviderType.Shipping;
            if (typeof(ITaxationGatewayProvider).IsAssignableFrom(type)) return GatewayProviderType.Taxation;

            throw new InvalidOperationException("Could not map GatewayProviderType from " + type.Name);
        }
    }
}