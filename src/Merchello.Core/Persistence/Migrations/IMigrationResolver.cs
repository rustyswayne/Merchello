namespace Merchello.Core.Persistence.Migrations
{
    using System;
    using System.Collections.Generic;

    using Semver;

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
        IEnumerable<IMerchelloMigration> OrderedUpgradeMigrations(SemVersion currentVersion, SemVersion targetVersion);

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
        IEnumerable<IMerchelloMigration> OrderedDowngradeMigrations(SemVersion currentVersion, SemVersion targetVersion);
    }
}