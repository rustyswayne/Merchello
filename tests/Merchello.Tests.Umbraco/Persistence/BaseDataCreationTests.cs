namespace Merchello.Tests.Umbraco.Persistence
{
    using Merchello.Core.DI;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NPoco;

    using NUnit.Framework;

    using StoreSettingDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.StoreSettingDto;
    using WarehouseDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.WarehouseDto;

    [TestFixture]
    public class BaseDataCreationTests : UmbracoRuntimeTestBase
    {
        private IDatabaseSchemaManager SchemaManager;

        private Database Database;

        public override void Initialize()
        {
            base.Initialize();
            Assert.NotNull(MC.Container, "IoC container not setup");

            SchemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            SchemaManager.UninstallDatabaseSchema();
            SchemaManager.InstallDatabaseSchema();

            Database = MC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
            Assert.NotNull(Database);
        }

        public override void TearDown()
        {
            SchemaManager.UninstallDatabaseSchema();
            base.TearDown();
        }

        [Test]
        public void Lock_Table_Is_Populated()
        {   
            //// Arrange
            const int expected = 7;

            //// Act
            var dtos = Database.Fetch<LockDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void TypeField_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 35;

            //// Act
            var dtos = Database.Fetch<TypeFieldDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void InvoiceStatus_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 5;

            //// Act
            var dtos = Database.Fetch<InvoiceStatusDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void OrderStatus_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 5;

            //// Act
            var dtos = Database.Fetch<InvoiceStatusDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void ShipmentStatus_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 5;

            //// Act
            var dtos = Database.Fetch<ShipmentStatusDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void Warehouse_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 1;

            //// Act
            var dtos = Database.Fetch<WarehouseDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void WarehouseCatalog_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 1;

            //// Act
            var dtos = Database.Fetch<WarehouseCatalogDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void GatewayProviderSettings_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 3;

            //// Act
            var dtos = Database.Fetch<GatewayProviderSettingsDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }

        [Test]
        public void StoreSettings_Table_Is_Populated()
        {
            //// Arrange
            const int expected = 15;

            //// Act
            var dtos = Database.Fetch<StoreSettingDto>();

            //// Assert
            Assert.AreEqual(expected, dtos.Count);
        }
    }
}