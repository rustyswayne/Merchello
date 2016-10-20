namespace Merchello.Umbraco.Migrations.Upgrades.TargetVersionThreeZeroZero
{
    using Merchello.Core;
    using Merchello.Core.Persistence.Migrations;

    using global::Umbraco.Core.Persistence.Migrations;

    /// <summary>
    /// Drops old examine index id tables.
    /// </summary>
    [global::Umbraco.Core.Persistence.Migrations.MigrationAttribute("2.3.1", "3.0.0", 0, Constants.MerchelloMigrationName)]
    public class DropIndexTables : MigrationBase, IMerchelloMigration
    {
        public DropIndexTables(IMigrationContext context)
            : base(context)
        {
        }

        public override void Up()
        {
            throw new System.NotImplementedException();
        }

        public override void Down()
        {
            throw new System.NotImplementedException();
        }
    }
}