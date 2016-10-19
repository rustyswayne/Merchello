namespace Merchello.Umbraco.Migrations.Upgrades.TargetVersionThreeZeroZero
{
    using Merchello.Core;

    using global::Umbraco.Core.Persistence.Migrations;

    /// <summary>
    /// Drops old examine index id tables.
    /// </summary>
    [Migration("2.3.1", "3.0.0", 0, Constants.MerchelloMigrationName)]
    public class DropIndexTables : MigrationBase
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