namespace Merchello.Core.Persistence.Migrations
{
    using System;

    using LightInject;

    using Merchello.Core.Boot;
    using Merchello.Core.Configuration;
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
        /// <param name="dbAdapter">
        /// The <see cref="IDatabaseAdapter"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        protected MigrationManagerBase(IServiceContainer container, IDatabaseAdapter dbAdapter, ILogger logger)
        {
            Ensure.ParameterNotNull(container, nameof(container));
            Ensure.ParameterNotNull(dbAdapter, nameof(dbAdapter));
            Ensure.ParameterNotNull(logger, nameof(logger));
            _container = container;
            this.Logger = logger;
            _dbAdapter = dbAdapter;

            Initialize();
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
        /// Gets the <see cref="IDatabaseSchemaCreation"/>.
        /// </summary>
        protected IDatabaseSchemaCreation SchemaCreation { get; private set; }

        /// <summary>
        /// Gets the <see cref="DatabaseSchemaResult"/>.
        /// </summary>
        protected DatabaseSchemaResult DbSchemaResult { get; private set; }

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
        /// Ensures all necessary migrations have been run.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual DbSchemaStatus GetDbSchemaStatus()
        {
            Logger.Info<MigrationManagerBase>("Verifying Merchello database is present.");
            
            if (DbVersion != MerchelloVersion.Current)
            {
                if (DbVersion == new Version(0, 0, 0))
                {
                    Logger.Info<CoreBoot>("Merchello database not installed.  Initial migration");
                    return DbSchemaStatus.RequiresInstall;
                }

                Logger.Info<CoreBoot>("Merchello version did not match, find migration(s).");
                return DbSchemaStatus.RequiresUpgrade;
            }
           
            Logger.Info<CoreBoot>("Merchello database is the current version");

            return DbSchemaStatus.Current;
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            DbSchemaResult = SchemaCreation.ValidateSchema();
            DbVersion = DbSchemaResult.DetermineInstalledVersion();
        }

        /// <inheritdoc/>
        public virtual void InstallDatabaseSchema()
        {
            SchemaCreation.InitializeDatabaseSchema();
        }


        /// <inheritdoc/>
        public virtual void UninstallDatabaseSchema()
        {
            SchemaCreation.UninstallDatabaseSchema();
        }

        /// <summary>
        /// The process migrations for upgrading.
        /// </summary>
        protected abstract void ProcessMigrations();

        /// <summary>
        /// Initializes the Migration Manager.
        /// </summary>
        private void Initialize()
        {
            SchemaCreation = _container.GetInstance<IDatabaseSchemaCreation>();
            DbSchemaResult = SchemaCreation.ValidateSchema();
            DbVersion = DbSchemaResult.DetermineInstalledVersion();
        }
    }
}