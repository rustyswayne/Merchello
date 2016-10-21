namespace Merchello.Core.Boot
{
    using System;

    using AutoMapper;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Compositions;
    using Merchello.Core.Configuration;
    using Merchello.Core.DI;
    using Merchello.Core.Logging;
    using Merchello.Core.Mapping;

    using Ensure = Merchello.Core.Ensure;


    /// <summary>
    /// Application boot strap for the Merchello Plugin which initializes all objects to be used in the Merchello Core
    /// </summary>
    /// <remarks>
    /// We needed our own boot strap to setup Merchello specific singletons
    /// </remarks>
    internal class CoreBoot : IBoot
    {
        #region Fields

        /// <summary>
        /// The Logger.
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// The timer.
        /// </summary>
        private IDisposableTimer _timer;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreBoot"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        internal CoreBoot(IServiceContainer container)
        {
            Ensure.ParameterNotNull(container, nameof(container));

            // Self register the container
            container.Register<IServiceContainer>(_ => container);

            MC.Container = container;
        }


        /// <inheritdoc/>
        public virtual void Boot()
        {
            _timer =
                MC.Container.GetInstance<IProfilingLogger>()
                    .TraceDuration<CoreBoot>(
                        $"Merchello {MerchelloVersion.GetSemanticVersion()} application starting on {NetworkHelper.MachineName}",
                        "Merchello application startup complete");

            _logger = MC.Container.GetInstance<ILogger>();

            // Setup the container with all of the application services
            this.Compose(MC.Container);

            // AutoMapper mappings need to be setup for database definition adapters before checking the installation
            InitializeAutoMapperMappers();

            // Ensure Installation
            EnsureInstallVersion(MC.Container);

            // stop the timer and log the output
            _timer.Dispose();
        }

        /// <inheritdoc/>
        public virtual void Terminate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Build the core container which contains all core things required to build the MerchelloContext
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        internal virtual void Compose(IServiceContainer container)
        {
            // Logger
            container.RegisterFrom<LoggerComposition>();

            // Configuration
            container.RegisterFrom<ConfigurationComposition>();

            // Cache
            container.RegisterFrom<CacheComposition>();

            // Repositories
            container.RegisterFrom<RepositoryComposition>();

            // Data Services/ServiceContext/etc...
            container.RegisterFrom<ServicesComposition>();

            // Registers
            container.RegisterFrom<RegistersComposition>();
        }

        /// <summary>
        /// Ensures Merchello is installed and the database has been migrated to the current version.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        protected virtual void EnsureInstallVersion(IServiceContainer container)
        {
        }

        /// <summary>
        /// The initializes the AutoMapper mappings.
        /// </summary>
        protected void InitializeAutoMapperMappers()
        {
            var container = MC.Container;

            Mapper.Initialize(configuration =>
                {
                    foreach (var mc in container.GetAllInstances<MerchelloMapperConfiguration>())
                    {
                        mc.ConfigureMappings(configuration);
                    }
                });
        }
    }
}
