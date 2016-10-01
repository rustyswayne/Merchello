﻿namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Models.Migrations;

    using Semver;

    /// <summary>
    /// Represents a service that provides information about database migrations.
    /// </summary>
    internal interface IMigrationStatusService : IService
    {
        /// <summary>
        /// Creates a migration entry, will throw an exception if it already exists
        /// </summary>
        /// <param name="migrationName">
        /// The name of the migration.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="IMigrationStatus"/>.
        /// </returns>
        IMigrationStatus CreateEntry(string migrationName, SemVersion version);

        /// <summary>
        /// Finds a migration by name and version, returns null if not found
        /// </summary>
        /// <param name="migrationName">
        /// The name of the migration.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="IMigrationStatus"/>.
        /// </returns>
        IMigrationStatus FindStatus(string migrationName, SemVersion version);

        /// <summary>
        /// Gets all entries for a given migration name
        /// </summary>
        /// <param name="migrationName">
        /// The name of the migration.
        /// </param>
        /// <returns>
        /// The collection of migrations that match the name.
        /// </returns>
        IEnumerable<IMigrationStatus> GetAll(string migrationName);
    }
}