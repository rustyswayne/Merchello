namespace Merchello.Core.Persistence.Migrations
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a migration runner.
    /// </summary>
    internal interface IMigrationRunner
    {
        /// <summary>
        /// Gets a collection of ordered upgrade migrations for upgrading.
        /// </summary>
        /// <param name="foundMigrations">
        /// The collection of <see cref="IMerchelloMigration"/>.
        /// </param>
        /// <returns>
        /// The collection of migrations.
        /// </returns>
        IEnumerable<IMerchelloMigration> OrderedUpgradeMigrations(IEnumerable<IMerchelloMigration> foundMigrations);

        /// <summary>
        /// Gets a collection of ordered upgrade migrations for downgrading.
        /// </summary>
        /// <param name="foundMigrations">
        /// The collection of <see cref="IMerchelloMigration"/>.
        /// </param>
        /// <returns>
        /// The collection of migrations.
        /// </returns>
        IEnumerable<IMerchelloMigration> OrderedDowngradeMigrations(IEnumerable<IMerchelloMigration> foundMigrations);
    }
}