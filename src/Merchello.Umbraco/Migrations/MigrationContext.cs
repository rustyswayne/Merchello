namespace Merchello.Umbraco.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using NPoco;

    using global::Umbraco.Core.Persistence;

    using global::Umbraco.Core.Persistence.Migrations;

    using global::Umbraco.Core.Persistence.SqlSyntax;

    using ILogger = global::Umbraco.Core.Logging.ILogger;

    /// <inheritdoc/>
    internal class MigrationContext : IMigrationContext
    {
        public MigrationContext(UmbracoDatabase database, ILogger logger)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            this.Expressions = new Collection<IMigrationExpression>();
            this.Database = database;
            this.Logger = logger;
        }

        public ICollection<IMigrationExpression> Expressions { get; set; }

        public UmbracoDatabase Database { get; }

        public ISqlSyntaxProvider SqlSyntax => this.Database.SqlSyntax;

        public DatabaseType DatabaseType => this.Database.DatabaseType;

        public ILogger Logger { get; }
    }
}