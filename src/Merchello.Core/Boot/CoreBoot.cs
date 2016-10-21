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
    internal class CoreBoot : BootBase, IBoot, IBootManager
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

        /// <summary>
        /// The <see cref="IMerchelloContext"/>.
        /// </summary>
        private IMerchelloContext _merchelloContext;

        /// <summary>
        /// The is complete.
        /// </summary>
        private bool _isComplete;


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreBoot"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        internal CoreBoot(ICoreBootSettings settings)
        {
            Ensure.ParameterNotNull(settings, nameof(settings));

            this.CoreBootSettings = settings;

            // "Service Registry" - singleton to for required application objects needed for the Merchello instance
            var container = new ServiceContainer();
            container.EnableAnnotatedConstructorInjection();
            container.EnableAnnotatedPropertyInjection();

            MC.Container = container;

            this.IsForTesting = settings.IsForTesting;
        }

        /// <summary>
        /// Gets a value indicating whether Merchello is started.
        /// </summary>
        internal bool IsStarted { get; private set; }

        /// <summary>
        /// Gets a value indicating whether Merchello is initialized.
        /// </summary>
        internal bool IsInitialized { get; private set; }

        /// <summary>
        /// Gets a value indicating whether or not the boot manager is being used for testing.
        /// </summary>
        internal bool IsForTesting { get; }

        /// <summary>
        /// Gets the core boot settings.
        /// </summary>
        protected ICoreBootSettings CoreBootSettings { get; }

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <returns>
        /// The <see cref="IBootManager"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if Merchello is already initialized
        /// </exception>
        public override IBootManager Initialize()
        {
            if (IsInitialized)
                throw new InvalidOperationException("The Merchello core boot manager has already been initialized");

            OnMerchelloInit();

            // Grab everythin we need from Umbraco
            ConfigureCmsServices(MC.Container);

            _timer =
                MC.Container.GetInstance<IProfilingLogger>()
                    .TraceDuration<CoreBoot>(
                        $"Merchello {MerchelloVersion.GetSemanticVersion()} application starting on {NetworkHelper.MachineName}",
                        "Merchello application startup complete");

            _logger = MC.Container.GetInstance<ILogger>();

            // Setup the container with all of the application services
            ConfigureCoreServices(MC.Container);

            // AutoMapper mappings need to be setup for database definition adapters before checking the installation
            InitializeAutoMapperMappers();

            // Ensure Installation
            EnsureInstallVersion(MC.Container);



            this.IsInitialized = true;   

            return this;
        }

        /// <summary>
        /// Fires after initialization and calls the callback to allow for customizations to occur
        /// </summary>
        /// <param name="afterStartup">
        /// The action to call after startup
        /// </param>
        /// <returns>
        /// The <see cref="IBootManager"/>.
        /// </returns>
        public override IBootManager Startup(Action<IMerchelloContext> afterStartup)
        {
            if (this.IsStarted)
                throw new InvalidOperationException("The boot manager has already been initialized");

            //// if (afterStartup != null)
            ////    afterStartup(MerchelloContext.Current);

            this.IsStarted = true;

            return this;
        }

        /// <summary>
        /// Fires after startup and calls the callback once customizations are locked
        /// </summary>
        /// <param name="afterComplete">
        /// The after Complete.
        /// </param>
        /// <returns>
        /// The <see cref="IBootManager"/>.
        /// </returns>
        public override IBootManager Complete(Action<IMerchelloContext> afterComplete)
        {
            if (this._isComplete)
                throw new InvalidOperationException("The boot manager has already been completed");


            // if (afterComplete != null)
            // {
            // afterComplete(MerchelloContext.Current);
            // }

            this._isComplete = true;

            // stop the timer and log the output
            _timer.Dispose();
            return this;
        }

        /// <summary>
        /// Allows for injection of CMS Foundation services that Merchello relies on.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        internal virtual void ConfigureCmsServices(IServiceContainer container)
        {
            // Container
            container.Register<IServiceContainer>(factory => container);
        }

        /// <summary>
        /// Build the core container which contains all core things required to build the MerchelloContext
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        internal virtual void ConfigureCoreServices(IServiceContainer container)
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

            container.RegisterSingleton<IMerchelloContext, MerchelloContext>();
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


        public void Boot()
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
