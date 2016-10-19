namespace Merchello.Core.Persistence.Migrations
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a migration resolver for finding Merchello specific migrations.
    /// </summary>
    internal interface IMigrationResolver
    {
        /// <summary>
        /// Gets the instance types.
        /// </summary>
        IEnumerable<Type> InstanceTypes { get; }

        /// <summary>
        /// Gets a collection of ordered upgrade migrations for upgrading.
        /// </summary>
        /// <param name="currentVersion">
        /// The current version.
        /// </param>
        /// <param name="targetVersion">
        /// The target version.
        /// </param>
        /// <returns>
        /// The collection of migrations.
        /// </returns>
        IEnumerable<IMigration> OrderedUpgradeMigrations(Version currentVersion, Version targetVersion);

        /// <summary>
        /// Gets a collection of ordered upgrade migrations for downgrading.
        /// </summary>
        /// <param name="currentVersion">
        /// The current version.
        /// </param>
        /// <param name="targetVersion">
        /// The target version.
        /// </param>
        /// <returns>
        /// The collection of migrations.
        /// </returns>
        IEnumerable<IMigration> OrderedDowngradeMigrations(Version currentVersion, Version targetVersion);
    }
}