namespace Merchello.Core.DI
{
    using System;

    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.Gateways;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Services;

    /// <summary>
    /// Exposes Merchello Core container.
    /// </summary>
    public static class MC
    {
        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private static IServiceContainer _container;

        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        public static ILogger Logger => Container.GetInstance<ILogger>();

        /// <summary>
        /// The <see cref="ICacheHelper"/>.
        /// </summary>
        public static ICacheHelper Cache => Container.GetInstance<ICacheHelper>(); 

        /// <summary>
        /// The <see cref="DatabaseAdapter"/>.
        /// </summary>
        public static IDatabaseAdapter DatabaseAdapter => Container.GetInstance<IDatabaseAdapter>();

        /// <summary>
        /// Gets the <see cref="IServiceContext"/>.
        /// </summary>
        public static IServiceContext Services => Container.GetInstance<IServiceContext>();

        /// <summary>
        /// Gets the <see cref="IGatewayContext"/>
        /// </summary>
        public static IGatewayContext Gateways => Container.GetInstance<IGatewayContext>();


        /// <summary>
        /// Gets the <see cref="IEntityCollectionProviderRegister"/>.
        /// </summary>
        internal static IEntityCollectionProviderRegister EntityCollectionProviderRegister => Container.GetInstance<IEntityCollectionProviderRegister>();

        /// <summary>
        /// Gets the <see cref="IGatewayProviderRegister"/>
        /// </summary>
        internal static IGatewayProviderRegister GatewayProviderRegister => Container.GetInstance<IGatewayProviderRegister>();

        /// <summary>
        /// The <see cref="IActivatorServiceProvider"/>.
        /// </summary>
        internal static IActivatorServiceProvider ActivatorServiceProvider => Container.GetInstance<IActivatorServiceProvider>();

        /// <summary>
        /// Gets or sets the <see cref="IServiceContainer"/>.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Throws an exception if the singleton has not been instantiated.
        /// </exception>
        internal static IServiceContainer Container
        {
            get
            {
                if (_container == null) throw new NullReferenceException("Container has not been set");
                return _container;
            }

            set
            {
                _container = value;
            }
        }
    }
}