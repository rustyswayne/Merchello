namespace Merchello.Umbraco.Boot
{
    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Umbraco.Adapters.Persistence;
    using Merchello.Umbraco.Cache;
    using Merchello.Umbraco.DI;
    using Merchello.Umbraco.Migrations;
    using Merchello.Web.Boot;

    using global::Umbraco.Core;

    using global::Umbraco.Core.Plugins;

    using IDatabaseFactory = Merchello.Core.Persistence.IDatabaseFactory;

    /// <summary>
    /// Starts the Merchello Umbraco CMS Package.
    /// </summary>
    internal class UmbracoBoot : WebBoot
    {
        /// <summary>
        /// Umbraco's <see cref="ApplicationContext"/>.
        /// </summary>
        private readonly ApplicationContext _appContext;

        /// <summary>
        /// Umbraco's <see cref="PluginManager"/>.
        /// </summary>
        private readonly PluginManager _pluginManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Merchello.Umbraco.Boot.UmbracoBoot"/> class.
        /// </summary>
        public UmbracoBoot()
            : this(new BootSettings(), ApplicationContext.Current, PluginManager.Current)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Merchello.Umbraco.Boot.UmbracoBoot"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="appContext">
        /// The app context.
        /// </param>
        public UmbracoBoot(IBootSettings settings, ApplicationContext appContext)
            : this(settings, appContext, PluginManager.Current)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Merchello.Umbraco.Boot.UmbracoBoot"/> class.
        /// </summary>
        /// <param name="settings">
        /// The <see cref="IBootSettings"/>.
        /// </param>
        /// <param name="appContext">
        /// Umbraco's ApplicationContext.
        /// </param>
        /// <param name="pluginManager">
        /// Umbraco's PluginManager
        /// </param>
        public UmbracoBoot(IBootSettings settings, ApplicationContext appContext, PluginManager pluginManager)
            : base(settings)
        {
            Core.Ensure.ParameterNotNull(appContext, nameof(appContext));
            Core.Ensure.ParameterNotNull(pluginManager, nameof(pluginManager));
            
            _appContext = appContext;
            _pluginManager = pluginManager;
        }

        /// <inheritdoc/>
        internal override void ConfigureCmsServices(IServiceContainer container)
        {
            base.ConfigureCmsServices(container);

            // ApplicationContext direct
            container.RegisterSingleton<global::Umbraco.Core.Persistence.SqlSyntax.ISqlSyntaxProvider>(factory => _appContext.DatabaseContext.SqlSyntax);
            container.RegisterSingleton<global::Umbraco.Core.Cache.CacheHelper>(factory => _appContext.ApplicationCache);
            container.RegisterSingleton<global::Umbraco.Core.Logging.ProfilingLogger>(factory => _appContext.ProfilingLogger);
            container.RegisterSingleton<global::Umbraco.Core.Logging.ILogger>(factory => _appContext.ProfilingLogger.Logger);
            container.RegisterSingleton<global::Umbraco.Core.DatabaseContext>(factory => _appContext.DatabaseContext);
            container.RegisterSingleton<global::Umbraco.Core.Plugins.PluginManager>(factory => _pluginManager);

            container.RegisterFrom<UmbracoCompositionRoot>();

            // Migrations
            container.Register<IMigrationManager, MigrationManager>();
        }

        /// <inheritdoc/>
        /// <param name="container"></param>
        internal override void ConfigureCoreServices(IServiceContainer container)
        {
            base.ConfigureCoreServices(container);

            // Need to wait for Merchello's IQueryFactory to be defined
            container.RegisterSingleton<IDatabaseFactory, DatabaseContextAdapter>();

            // Replace ICloneableCacheEntityFactory
            container.Register<ICloneableCacheEntityFactory, CacheSurrogateFactory>();
        }

        /// <inheritdoc/>
        protected override void EnsureInstallVersion(IServiceContainer container)
        {
            var manager = container.GetInstance<IMigrationManager>();

            var status = manager.GetDbSchemaStatus();
            switch (status)
            {
                case DbSchemaStatus.RequiresInstall:
                    manager.InstallDatabaseSchema();
                    break;
                case DbSchemaStatus.RequiresUpgrade:
                    break;
                case DbSchemaStatus.Current:
                default:
                    break;
            }
        }
    }
}