namespace Merchello.Core.Boot
{
    using System;

    using AutoMapper;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.ObjectResolution;
    using Merchello.Core.Cache;
    using Merchello.Core.Configuration;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Logging;
    using Merchello.Core.Mapping;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.Migrations.Initial;

    using Ensure = Merchello.Core.Ensure;


    /// <summary>
    /// Application boot strap for the Merchello Plugin which initializes all objects to be used in the Merchello Core
    /// </summary>
    /// <remarks>
    /// We needed our own boot strap to setup Merchello specific singletons
    /// </remarks>
    internal class CoreBootManager : BootManagerBase, IBootManager
    {
        #region Fields

        /// <summary>
        /// The multi log resolver.
        /// </summary>
        private MultiLogResolver _muliLogResolver;

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
        /// Initializes a new instance of the <see cref="CoreBootManager"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        internal CoreBootManager(ICoreBootSettings settings)
        {
            Ensure.ParameterNotNull(settings, nameof(settings));

            this.CoreBootSettings = settings;

            // "Service Registry" - singleton to for required application objects needed for the Merchello instance
            var container = new ServiceContainer();
            container.EnableAnnotatedConstructorInjection();
            IoC.Current = new IoC(container);

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
            ConfigureCmsServices(IoC.Container);

            _timer =
                IoC.Container.GetInstance<IProfilingLogger>()
                    .TraceDuration<CoreBootManager>(
                        $"Merchello {MerchelloVersion.GetSemanticVersion()} application starting on {NetworkHelper.MachineName}",
                        "Merchello application startup complete");

            _logger = IoC.Container.GetInstance<ILogger>();

            _muliLogResolver = new MultiLogResolver(GetMultiLogger(_logger));

            // Setup the container with all of the application services
            ConfigureCoreServices(IoC.Container);

            // AutoMapper mappings need to be setup for database definition adapters before checking the installation
            InitializeAutoMapperMappers();

            // Ensure Installation
            EnsureInstallVersion(IoC.Container);

            // Wait until we are certain the database is setup and upgraded before instantiating the MerchelloContext.
            // Certain resolvers may require data.
            MerchelloContext.Current = _merchelloContext = IoC.Container.GetInstance<IMerchelloContext>();

            InitializeResolvers();


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

            FreezeResolution();

            //if (afterComplete != null)
            //{
            //    afterComplete(MerchelloContext.Current);
            //}

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
            container.RegisterSingleton<MultiLogResolver>(factory => _muliLogResolver);


            // Configuration
            container.RegisterFrom<ConfigurationCompositionRoot>();

            // Cache
            container.RegisterSingleton<IRuntimeCacheProviderAdapter>(factory => factory.GetInstance<ICacheHelper>().RuntimeCache);

            // TODO see Umbraco's MixedScopeManagerProvider 
            // container.Register<ICloneableCacheEntityFactory, DefaultCloneableCacheEntityFactory>();

            // Repositories
            container.RegisterFrom<RepositoryCompositionRoot>();

            // Data Services/ServiceContext/etc...
            container.RegisterFrom<ServicesCompositionRoot>();

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
            var container = IoC.Container;

            Mapper.Initialize(configuration =>
                {
                    foreach (var mc in container.GetAllInstances<MerchelloMapperConfiguration>())
                    {
                        mc.ConfigureMappings(configuration);
                    }
                });
        }

        /// <summary>
        /// Initializes the logger resolver.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        protected virtual void InitializeLoggerResolver(IMultiLogger logger)
        {
            if (!MultiLogResolver.HasCurrent)
                MultiLogResolver.Current = new MultiLogResolver(logger);
        }



        /// <summary>
        /// Responsible for initializing resolvers.
        /// </summary>
        protected virtual void InitializeResolvers()
        {
            var container = IoC.Container;

            MappingResolver.Current = (MappingResolver)container.GetInstance<IMappingResolver>();

            //if (!TriggerResolver.HasCurrent)
            //TriggerResolver.Current = new TriggerResolver(PluginManager.Current.ResolveObservableTriggers());

            //if (!MonitorResolver.HasCurrent)
            //MonitorResolver.Current = new MonitorResolver(MerchelloContext.Current.Gateways.Notification, PluginManager.Current.ResolveObserverMonitors());            

            //if (!OfferProcessorFactory.HasCurrent)
            //OfferProcessorFactory.Current = new OfferProcessorFactory(PluginManager.Current.ResolveOfferConstraintChains());
        }



        /// <summary>
        /// Responsible initializing observer subscriptions.
        /// </summary>
        protected virtual void InitializeObserverSubscriptions()
        {
            //if (!TriggerResolver.HasCurrent || !MonitorResolver.HasCurrent) return;

            //var monitors = MonitorResolver.Current.GetAllMonitors();

            //LogHelper.Info<CoreBootManager>("Starting subscribing Monitors to Triggers");

            //foreach (var monitor in monitors)
            //{
            //    monitor.Subscribe(TriggerResolver.Current);
            //}

            //LogHelper.Info<Umbraco.Core.CoreBootManager>("Finished subscribing Monitors to Triggers");            
        }

        /// <summary>
        /// Gets the <see cref="MultiLogger"/>.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <returns>
        /// The <see cref="IMultiLogger"/>.
        /// </returns>
        protected virtual IMultiLogger GetMultiLogger(ILogger logger)
        {
            return new MultiLogger(logger);
        }

        /// <summary>
        /// Freeze resolution to not allow Resolvers to be modified
        /// </summary>
        protected virtual void FreezeResolution()
        {
            Resolution.Freeze();
        }
    }
}
