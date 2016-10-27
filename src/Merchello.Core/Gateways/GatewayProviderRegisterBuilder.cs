namespace Merchello.Core.Gateways
{
    using System;
    using System.Collections.Generic;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents a register builder for <see cref="IGatewayProviderRegister"/>
    /// </summary>
    internal class GatewayProviderRegisterBuilder : IRegisterBuilder<IGatewayProviderRegister, Type>
    {
        /// <summary>
        /// The container.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// The instance types.
        /// </summary>
        private readonly List<Type> _types = new List<Type>();

        /// <summary>
        /// The lock.
        /// </summary>
        private readonly object _locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderRegisterBuilder"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>
        /// </param>
        /// <param name="instanceTypes">
        /// The instance types.
        /// </param>
        public GatewayProviderRegisterBuilder(IServiceContainer container, IEnumerable<Type> instanceTypes)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
            this.Initialize(instanceTypes);
        }

        /// <summary>
        /// Gets the collection lifetime.
        /// </summary>
        /// <remarks>Return null for transient collections.</remarks>
        protected virtual ILifetime CollectionLifetime => new PerContainerLifetime();

        /// <inheritdoc/>
        public IGatewayProviderRegister CreateRegister()
        {
            return new GatewayProviderRegister(_container, _types);
        }

        /// <inheritdoc/>
        protected void Initialize(IEnumerable<Type> types)
        {
            lock (_locker)
            {
                _types.AddRange(types);
            }

            // register the collection
            _container.Register(_ => this.CreateRegister(), this.CollectionLifetime);
            _container.Register<IGatewayProviderRegister>(factory => factory.GetInstance<GatewayProviderRegister>());

            // register IGatewayProvider instantiation construct
            foreach (var type in _types)
            {
                var serviceName = GatewayProviderRegister.GetServiceNameForType(type);

                _container.Register<IGatewayProviderSettings, IGatewayProvider>(
                    (factory, settings) =>
                    {
                        var activator = factory.GetInstance<IActivatorServiceProvider>();

                        var provider = activator.GetService<IGatewayProvider>(
                        type,
                        new object[]
                        {
                                factory.GetInstance<IGatewayProviderService>(),
                                settings,
                                factory.GetInstance<ICacheHelper>().RuntimeCache
                        });
                        return provider;
                    }, 
                    serviceName);
            }

        }
    }
}