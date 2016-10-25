namespace Merchello.Core.Persistence.Migrations
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.DI;

    using Semver;

    /// <inheritdoc/>
    internal class MigrationRegister : Register<IMerchelloMigration>, IMigrationRegister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationRegister"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public MigrationRegister(IEnumerable<IMerchelloMigration> items)
            : base(items)
        {
        }
    }
}