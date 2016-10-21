namespace Merchello.Umbraco.Boot
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Umbraco.DI;
    using Merchello.Umbraco.Migrations;
    using Merchello.Web.Boot;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Cache;
    using global::Umbraco.Core.DI;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Plugins;

    using Merchello.Core.Cache;
    using Merchello.Umbraco.Adapters.Persistence;
    using Merchello.Umbraco.Cache;

    using IDatabaseFactory = Merchello.Core.Persistence.IDatabaseFactory;

    /// <summary>
    /// Starts the Merchello Umbraco CMS Package.
    /// </summary>
    internal class UmbracoBoot : WebBoot
    {
        /// <summary>
        /// Umbraco's <see cref="PluginManager"/>.
        /// </summary>
        private readonly PluginManager _pluginManager;

        /// <summary>
        /// Umbraco's <see cref="DatabaseContext"/>.
        /// </summary>
        private readonly DatabaseContext _databaseContext;

        /// <summary>
        /// Umbraco's <see cref="_applicationCache"/>.
        /// </summary>
        private readonly CacheHelper _applicationCache;

        /// <summary>
        /// Umbraco's <see cref="ProfilingLogger"/>.
        /// </summary>
        private readonly ProfilingLogger _profilingLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Merchello.Umbraco.Boot.UmbracoBoot"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IBootSettings"/>.
        /// </param>
        /// <param name="databaseContext">
        /// Umbraco's <see cref="DatabaseContext"/>.
        /// </param>
        /// <param name="applicationCache">
        /// Umbraco's <see cref="CacheHelper"/>.
        /// </param>
        /// <param name="profilingLogger">
        /// Umbraco's <see cref="ProfilingLogger"/>.
        /// </param>
        /// <param name="pluginManager">
        /// Umbraco's <see cref="PluginManager"/>
        /// </param>
        public UmbracoBoot(IServiceContainer container, DatabaseContext databaseContext, CacheHelper applicationCache, ProfilingLogger profilingLogger, PluginManager pluginManager)
            : base(container)
        {
            Core.Ensure.ParameterNotNull(databaseContext, nameof(databaseContext));
            Core.Ensure.ParameterNotNull(applicationCache, nameof(applicationCache));
            Core.Ensure.ParameterNotNull(profilingLogger, nameof(profilingLogger));
            Core.Ensure.ParameterNotNull(pluginManager, nameof(pluginManager));
            _databaseContext = databaseContext;
            _applicationCache = applicationCache;
            _profilingLogger = profilingLogger;
            _pluginManager = pluginManager;
        }

        /// <inheritdoc/>
        public override void Boot()
        {
            var container = MC.Container;

            // ApplicationContext direct
            container.RegisterSingleton<global::Umbraco.Core.Persistence.SqlSyntax.ISqlSyntaxProvider>(factory => _databaseContext.SqlSyntax);
            container.RegisterSingleton<global::Umbraco.Core.Cache.CacheHelper>(factory => _applicationCache);
            container.RegisterSingleton<global::Umbraco.Core.Logging.ProfilingLogger>(factory => _profilingLogger);
            container.RegisterSingleton<global::Umbraco.Core.Logging.ILogger>(factory => _profilingLogger.Logger);
            container.RegisterSingleton<global::Umbraco.Core.DatabaseContext>(factory => _databaseContext);
            container.RegisterSingleton<global::Umbraco.Core.Plugins.PluginManager>(factory => _pluginManager);

            container.RegisterFrom<UmbracoCompositionRoot>();

            // Migrations
            container.Register<IMigrationManager, MigrationManager>();

            // we have to grab the previous services from Umbraco before allowing Merchello to 
            // boot as we need to adapt them for usage in Merchello.
            base.Boot();
        }


        /// <inheritdoc/>
        /// <param name="container"></param>
        internal override void Compose(IServiceContainer container)
        {
            base.Compose(container);

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