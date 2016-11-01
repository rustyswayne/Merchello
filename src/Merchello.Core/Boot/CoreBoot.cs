namespace Merchello.Core.Boot
{
    using System;

    using AutoMapper;

    using LightInject;

    using Merchello.Core.Acquired;
    using Merchello.Core.Compositions;
    using Merchello.Core.Configuration;
    using Merchello.Core.DI;
    using Merchello.Core.Events;
    using Merchello.Core.Exceptions;
    using Merchello.Core.Gateways;
    using Merchello.Core.Logging;
    using Merchello.Core.Mapping;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations;

    using Ensure = Merchello.Core.Ensure;


    /// <summary>
    /// Application boot strap for the Merchello Plugin which initializes all objects to be used in the Merchello Core
    /// </summary>
    /// <remarks>
    /// We needed our own boot strap to setup Merchello specific singletons.
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
        /// <param name="bootSettings">
        /// The <see cref="IBootSettings"/>
        /// </param>
        internal CoreBoot(IServiceContainer container, IBootSettings bootSettings)
        {
            Ensure.ParameterNotNull(container, nameof(container));
            Ensure.ParameterNotNull(bootSettings, nameof(bootSettings));

            this.BootSettings = bootSettings;

            // Self register the container
            container.Register<IServiceContainer>(_ => container);

            MC.Container = container;
        }

        /// <summary>
        /// Occurs after the boot has completed.
        /// </summary>
        public static event EventHandler Complete;

        /// <summary>
        /// Gets or sets the merchello settings path.
        /// </summary>
        /// <remarks>
        /// Used in tests.
        /// </remarks>
        internal string MerchelloSettingsPath { get; set; }

        /// <summary>
        /// Gets the <see cref="IBootSettings"/>.
        /// </summary>
        protected IBootSettings BootSettings { get; }

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
            if (EnsureInstallVersion(MC.Container))
            {
                PostInstallInitialization();
            }

            // stop the timer and log the output
            _timer.Dispose();
        }

        /// <inheritdoc/>
        public virtual void Terminate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Occurs after a full boot and installation.
        /// </summary>
        public void PostInstallInitialization()
        {
            // GatewayProviderRegister
            MC.Container.GetInstance<IGatewayProviderRegister>().RefreshCache();

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

            // Strategies and task chains
            if (BootSettings.RequiresConfig)
            {
                container.RegisterFrom<StrategiesComposition>();
                container.RegisterFrom<TaskChainComposition>();
            }

            // Gateways
            container.RegisterFrom<GatewaysComposition>();
            
            // Builders
            container.RegisterFrom<BuilderComposition>();
        }

        /// <summary>
        /// Ensures Merchello is installed and the database has been migrated to the current version.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// A success value.
        /// </returns>
        internal virtual bool EnsureInstallVersion(IServiceContainer container)
        {
            if (!BootSettings.AutoInstall) return false;

            var manager = container.GetInstance<IMigrationManager>();

            var instruction = manager.GetMigrationInstruction();

            // Check for the current version and ensure Merchello is supposed to automatically perform the database updates.
            if (instruction.PluginInstallStatus != PluginInstallStatus.Current && instruction.AutoUpdateDbSchema)
            {
                if (instruction.PluginInstallStatus == PluginInstallStatus.RequiresInstall)
                {
                    // Merchello database tables need to be installed.
                    _logger.Info<CoreBoot>("Installing Merchello database.");
                    try
                    {
                        manager.InstallDatabaseSchema();
                        _logger.Info<CoreBoot>("Merchello database installation completed.");
                    }
                    catch (Exception ex)
                    {
                        _logger.Error<CoreBoot>("Merchello failed to install database", ex);
                        return false;
                    }   
                }

                if (instruction.PluginInstallStatus == PluginInstallStatus.RequiresUpgrade)
                {
                    _logger.Info<CoreBoot>("Finding migrations.");

                    _logger.Warn<CoreBoot>("No migrations found. Database version should have reported as current. Please log issue.");
                }
            }
            else if (instruction.PluginInstallStatus != PluginInstallStatus.Current)
            {
                var bex = new BootException("Merchello boot detected an upgrade is required but Merchello Settings for AutoUpdateDbSchema was set to 'false' - aborting boot.");
                _logger.Error<CoreBoot>("Merchello needs to be manually upgraded per configuration setting", bex);
                throw bex;
            }

            // update the configuration status in the merchelloSettings.config file
            if (instruction.UpdateConfigFile)
            {
                if (BootSettings.IsTest && !MerchelloSettingsPath.IsNullOrWhiteSpace())
                {
                    MerchelloConfig.SaveConfigurationStatus(instruction.TargetConfigurationStatus, MerchelloSettingsPath);
                }

                if (!BootSettings.IsTest) MerchelloConfig.SaveConfigurationStatus(instruction.TargetConfigurationStatus);
            }

            return true;
        }

        /// <summary>
        /// The initializes the AutoMapper mappings.
        /// </summary>
        protected virtual void InitializeAutoMapperMappers()
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
