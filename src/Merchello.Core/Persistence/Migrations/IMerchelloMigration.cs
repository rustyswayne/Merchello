namespace Merchello.Core.Persistence.Migrations
{
    /// <summary>
    /// Marker interface for Merchello Migrations.
    /// </summary>
    internal interface IMerchelloMigration
    {
        void Up();
        void Down();
    }
}