namespace Merchello.Core.Persistence.Migrations
{
    using System;

    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.SqlSyntax;

    using NPoco;

    /// <summary>
    /// Represents a Migration Manager responsible for installing, uninstalling and upgrading Merchello's database.
    /// </summary>
    internal interface IMigrationManager
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Gets the database version.
        /// </summary>
        Version DbVersion { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// Gets the sql syntax.
        /// </summary>
        ISqlSyntaxProviderAdapter SqlSyntax { get; }

        /// <summary>
        /// Gets the <see cref="MigrationInstruction"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="MigrationInstruction"/>.
        /// </returns>
        MigrationInstruction GetMigrationInstruction();

        /// <summary>
        /// Refreshes the status and schema result.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Installs Merchello database schema.
        /// </summary>
        void InstallDatabaseSchema();

        /// <summary>
        /// Uninstalls Merchello's database schema.
        /// </summary>
        void UninstallDatabaseSchema();

        /// <summary>
        /// Gets the result summary.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetSummary();

        /// <summary>
        /// The sql builder.
        /// </summary>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        Sql<SqlContext> Sql();
    }
}