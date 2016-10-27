namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Threading;
    using Merchello.Core.Boot;
    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Services;

    /// <inheritdoc/>
    internal class GatewayProviderRegister : Register<Type>, IGatewayProviderRegister
    {
        /// <summary>
        /// maintain our own index for faster lookup
        /// </summary>
        private readonly ConcurrentDictionary<Guid, IGatewayProviderSettings> _activatedCache = new ConcurrentDictionary<Guid, IGatewayProviderSettings>();

        /// <summary>
        /// The lock.
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

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

            CoreBoot.Complete += Initialize;
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetActivatedProviders<T>() where T : IGatewayProvider
        {
            return GetActivatedProviders().Where(x => x is T);
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetActivatedProviders()
        {
            return _activatedCache.Select(x => CreateInstance(x.Value));
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetAllProviders()
        {
            var activated = _activatedCache.Select(x => CreateInstance(x.Value));

            if (_activatedCache.Count == this.Count)
                return activated;

            var allResolved = new List<IGatewayProvider>();

            var factory = new GatewayProviderSettingsFactory();

            using (new WriteLock(_lock))
            {
                allResolved.AddRange(activated);

                var inactive = (from it in this
                                let key = it.GetCustomAttribute<GatewayProviderActivationAttribute>(false).Key
                                where !_activatedCache.ContainsKey(key)
                                select it).ToList();

                allResolved.AddRange(
                    inactive.Select(type => 
                         factory.BuildEntity(type, GetGatewayProviderType(type)))
                        .Select(CreateInstance));
            }

            return allResolved;
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProvider> GetAllProviders<T>() where T : IGatewayProvider
        {
            return GetAllProviders().Where(x => x is T);
        }

        /// <inheritdoc/>
        public T GetProviderByKey<T>(Guid gatewayProviderKey, bool activatedOnly = true) where T : IGatewayProvider
        {
            if (activatedOnly)
                return (T)GetActivatedProviders<T>().FirstOrDefault(x => x.Key == gatewayProviderKey);

            return (T)GetAllProviders().FirstOrDefault(x => x.Key == gatewayProviderKey);
        }

        /// <inheritdoc/>
        public void RefreshCache()
        {
            _activatedCache.Clear();
            BuildActivatedCache();
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

        /// <summary>
        /// Gets the name used to register the type in the service container.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The service name.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if the type is not decorated with the <see cref="GatewayProviderActivationAttribute"/>.
        /// </exception>
        internal static string GetServiceNameForType(Type type)
        {
            var att = type.GetCustomAttribute<GatewayProviderActivationAttribute>(false);
            if (att == null) throw new InvalidOperationException("Type was not decorated with GatewayProviderActivationAttribute");
            return type.GetCustomAttribute<GatewayProviderActivationAttribute>(false).Key.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="providerSettings">
        /// The provider settings.
        /// </param>
        /// <returns>
        /// The <see cref="IGatewayProvider"/>.
        /// </returns>
        private IGatewayProvider CreateInstance(IGatewayProviderSettings providerSettings)
        {
            var providerType = this.FirstOrDefault(x => x.GetCustomAttribute<GatewayProviderActivationAttribute>(false).Key == providerSettings.Key);

            if (providerType == null) throw new NullReferenceException($"Failed to find type for provider {providerSettings.Name}");

            var serviceName = GetServiceNameForType(providerType);

            return _container.GetInstance<IGatewayProviderSettings, IGatewayProvider>(providerSettings, serviceName);
        }

        /// <summary>
        /// The build activated gateway provider cache.
        /// </summary>
        private void BuildActivatedCache()
        {
            var service = _container.GetInstance<IGatewayProviderService>();

            // Get the list of all provider settings saved in the database.
            // If a provider is "Activated" there will always be a reference
            var settings = service.GetAllGatewayProviders().ToArray();
            foreach (var setting in settings) AddOrUpdateCache(setting);
        }

        /// <summary>
        /// Adds or updates the activated cache.
        /// </summary>
        /// <param name="settings">
        /// The provider.
        /// </param>
        private void AddOrUpdateCache(IGatewayProviderSettings settings)
        {
            _activatedCache.AddOrUpdate(settings.Key, settings, (x, y) => settings);
        }

        /// <summary>
        /// Initializes after boot has completed.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="CoreBoot"/>.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void Initialize(object sender, EventArgs e)
        {
            BuildActivatedCache();
        }
    }
}