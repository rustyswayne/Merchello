namespace Merchello.Tests.Umbraco.Persistence
{
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Umbraco.TestHelpers.Base;

    using NPoco;

    using NUnit.Framework;

    [TestFixture]
    public class BaseDataCreationTests : UmbracoApplicationContextTestBase
    {
        private IDatabaseSchemaManager SchemaManager;

        private Database Database;

        public override void Initialize()
        {
            base.Initialize();
            Assert.NotNull(IoC.Container, "IoC container not setup");

            SchemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            SchemaManager.InstallDatabaseSchema();

            Database = IoC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
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