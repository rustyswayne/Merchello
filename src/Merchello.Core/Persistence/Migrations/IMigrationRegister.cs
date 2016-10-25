namespace Merchello.Core.Persistence.Migrations
{
    using System.Collections.Generic;

    using Merchello.Core.DI;

    using Semver;

    /// <summary>
    /// Represents a migration register for Merchello specific migrations.
    /// </summary>
    internal interface IMigrationRegister : IRegister<IMerchelloMigration>
    {
    }
}