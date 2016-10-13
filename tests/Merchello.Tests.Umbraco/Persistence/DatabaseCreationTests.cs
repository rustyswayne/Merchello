namespace Merchello.Tests.Umbraco.Persistence
{
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class DatabaseCreationTests : UmbracoApplicationContextTestBase
    {
        private IDatabaseSchemaManager SchemaManager;

        public override void Initialize()
        {
            base.Initialize();
            Assert.NotNull(IoC.Container, "IoC container not setup");

            SchemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            Assert.NotNull(SchemaManager);
        }

        public override void TearDown()
        {
            SchemaManager.UninstallDatabaseSchema();
            base.TearDown();
        }

        [Test]
        public void Can_Install_Merch_Database_Tables()
        {
            //// Arrange
            const int expected = 52; // tables

            //// Act
            SchemaManager.InstallDatabaseSchema();
            var result = SchemaManager.ValidateSchema();
            
            //// Assert
            Assert.NotNull(result);
            Assert.AreEqual(expected, result.TableDefinitions.Count, "table count did not match expected.");
        }

    }
}