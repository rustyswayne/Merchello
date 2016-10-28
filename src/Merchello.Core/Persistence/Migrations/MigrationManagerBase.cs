namespace Merchello.Core.Persistence.Migrations
{
    using System;

    using LightInject;

    using Merchello.Core.Boot;
    using Merchello.Core.Configuration;
    using Merchello.Core.Exceptions;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Migrations.Initial;
    using Merchello.Core.Persistence.SqlSyntax;

    using NPoco;

    using Ensure = Merchello.Core.Ensure;

    /// <summary>
    /// Represents the a base Migration Manager.
    /// </summary>
    internal abstract class MigrationManagerBase : IMigrationManager
    {
        /// <summary>
        /// The <see cref="IDatabaseFactory"/>.
        /// </summary>
        private readonly IDatabaseFactory _dbFactory;

        /// <summary>
        /// The Database Adapter.
        /// </summary>
        private readonly IDatabaseAdapter _dbAdapter;

        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationManagerBase"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>
        /// </param>
        /// <param name="dbFactory">
        /// The <see cref="IDatabaseFactory"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        protected MigrationManagerBase(IServiceContainer container, IDatabaseFactory dbFactory, ILogger logger)
        {
            Ensure.ParameterNotNull(container, nameof(container));
            Ensure.ParameterNotNull(dbFactory, nameof(dbFactory));
            Ensure.ParameterNotNull(logger, nameof(logger));
            _container = container;
            this.Logger = logger;
            _dbFactory = dbFactory;
            _dbAdapter = dbFactory.GetDatabase();

            EnsureInitialized();
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// The sql syntax.
        /// </summary>
        public ISqlSyntaxProviderAdapter SqlSyntax => _dbAdapter.SqlSyntax;

        /// <summary>
        /// The database.
        /// </summary>
        public Database Database => _dbAdapter.Database;

        /// <summary>
        /// Gets the database version.
        /// </summary>
        public Version DbVersion { get; private set; }

        /// <summary>
        /// Gets the <see cref="IDatabaseSchemaManager"/>.
        /// </summary>
        protected IDatabaseSchemaManager SchemaManager { get; private set; }


        /// <summary>
        /// Gets the <see cref="DatabaseSchemaResult"/>.
        /// </summary>
        protected DatabaseSchemaResult DbSchemaResult { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is manager is initialized.
        /// </summary>
        protected bool IsInitialized { get; private set; } = false;

        /// <inheritdoc/>
        public string GetSummary()
        {
            if (DbSchemaResult == null) return string.Empty;
            return DbSchemaResult.GetSummary();
        }

        /// <summary>
        /// The sql.
        /// </summary>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        public Sql<SqlContext> Sql()
        {
            return _dbAdapter.Sql();
        }


        /// <summary>
        /// Gets the <see cref="MigrationInstruction"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="MigrationInstruction"/>.
        /// </returns>
        public virtual MigrationInstruction GetMigrationInstruction()
        {
            EnsureInitialized();

            // version in the configuration
            var statusFromConfig = MerchelloConfig.For.MerchelloSettings().ConfigurationStatus;
            Logger.Info<IMigrationManager>($"Merchello configuration status version: {statusFromConfig.GetVersion(0)}");

            // get the configuration value which indicates whether or not we should automatically execute migrations.
            var autoUpdateDbSchema = MerchelloConfig.For.MerchelloSettings().Migrations.AutoUpdateDbSchema;

            var installStatus = GetPluginInstallStatus();

            return new MigrationInstruction
                {
                    ConfigurationStatus = statusFromConfig,
                    TargetConfigurationStatus = MerchelloVersion.GetSemanticVersion(),
                    PluginInstallStatus = installStatus,
                    AutoUpdateDbSchema = autoUpdateDbSchema
                };
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            EnsureInitialized();

            DbSchemaResult = SchemaManager.ValidateSchema();
            DbVersion = DbSchemaResult.DetermineInstalledVersion();
        }

        /// <inheritdoc/>
        public virtual void InstallDatabaseSchema()
        {
            EnsureInitialized();

            SchemaManager.InstallDatabaseSchema();
        }


        /// <inheritdoc/>
        public virtual void UninstallDatabaseSchema()
        {
            EnsureInitialized();

            SchemaManager.UninstallDatabaseSchema();
        }

        /// <summary>
        /// Compares database schema status with application status.
        /// </summary>
        /// <returns>
        /// The <see cref="PluginInstallStatus"/>.
        /// </returns>
        protected virtual PluginInstallStatus GetPluginInstallStatus()
        {
            EnsureInitialized();

            Logger.Info<IMigrationManager>("Checking Merchello database.");
            Logger.Info<IMigrationManager>($"Merchello database version: {DbVersion}");
            Logger.Info<IMigrationManager>($"Merchello application version: {MerchelloVersion.Current}");

            if (DbVersion != MerchelloVersion.Current)
            {
                if (DbVersion == new Version(0, 0, 0))
                {
                    Logger.Info<IMigrationManager>("Merchello database not installed.  Initial migration");
                    return PluginInstallStatus.RequiresInstall;
                }

                if (DbVersion < MerchelloVersion.Current)
                {
                    Logger.Info<IMigrationManager>("Merchello database needs to updated, will try to find migration(s).");
                    return PluginInstallStatus.RequiresUpgrade;
                }
            }

            Logger.Info<IMigrationManager>("Merchello is version is current.");
            return PluginInstallStatus.Current;
        }

        /// <summary>
        /// The process migrations for upgrading.
        /// </summary>
        protected abstract void ProcessMigrations();

        /// <summary>
        /// Ensures the manager is initialized.
        /// </summary>
        private void EnsureInitialized()
        {
            if (IsInitialized) return;
            if (_dbFactory.Configured && _dbFactory.CanConnect)
            {
                Initialize();
                return;
            }

            var bex = new BootException("The database is not configured or Merchello cannot connect to database.");
            Logger.Error<IMigrationManager>("Cannot connect to the database", bex);
            throw bex;
        }

        /// <summary>
        /// Initializes the Migration Manager.
        /// </summary>
        private void Initialize()
        {
            SchemaManager = _container.GetInstance<IDatabaseSchemaManager>();
            DbSchemaResult = SchemaManager.ValidateSchema();
            DbVersion = DbSchemaResult.DetermineInstalledVersion();
            IsInitialized = true;
        }
    }
}