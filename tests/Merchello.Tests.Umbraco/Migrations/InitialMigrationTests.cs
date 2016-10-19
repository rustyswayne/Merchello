namespace Merchello.Tests.Umbraco.Migrations
{
    using System;

    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations;

    using NUnit.Framework;

    [TestFixture]
    public class InitialMigrationTests : MigrationTestBase
    {
        private static bool IsUpgradeTest = false;

        [SetUp]
        public void Setup()
        {
            IsUpgradeTest = false;
            // Ensure the database is deleted so we can test how this behaves on installs
            var manager = IoC.Container.GetInstance<IDatabaseSchemaManager>();

            //var creation = GetDbSchemaCreation(manager);
            //creation.UninstallDatabaseSchema(V2OrderedTables);
            //manager.UninstallDatabaseSchema();
        }

        //[TearDown]
        public void Teardown()
        {
            var manager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            if (IsUpgradeTest)
            {
                var creation = GetDbSchemaCreation(manager);
                creation.UninstallDatabaseSchema(V2OrderedTables);
            }
            else
            {
                manager.UninstallDatabaseSchema();
            }
        }
        

        [Test]
        public void Can_Detect_Install_Required()
        {
            var manager = IoC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetDbSchemaStatus();

            Assert.That(status, Is.EqualTo(DbSchemaStatus.RequiresInstall));
        }

        [Test]
        public void Can_Install_Database()
        {
            var manager = IoC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetDbSchemaStatus();

            if (status != DbSchemaStatus.RequiresInstall)
            {
                Assert.Fail("Database status returned to not install");
            }

            manager.InstallDatabaseSchema();

            manager.Refresh();

            status = manager.GetDbSchemaStatus();

            Assert.That(status, Is.EqualTo(DbSchemaStatus.Current));
        }

        [Test]
        public void Can_Uninstall_Database()
        {
            var schemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.InstallDatabaseSchema();
            var manager = IoC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetDbSchemaStatus();

            if (status != DbSchemaStatus.Current)
            {
                Assert.Fail("Database status did not return Current");
            }

            manager.UninstallDatabaseSchema();
            manager.Refresh();
            status = manager.GetDbSchemaStatus();

            Assert.That(status, Is.EqualTo(DbSchemaStatus.RequiresInstall));
        }

        [Test]
        public void Can_Detect_UpgradeRequired()
        {
            // Toggle to delete the old db version
            IsUpgradeTest = true;

            //// Arrange
          
            // install a V2.3.1 database
            var schemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            var dbCreation = GetDbSchemaCreation(schemaManager);
            dbCreation.InitializeDatabaseSchema(V2OrderedTables);

            //// Act
            var manager = IoC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetDbSchemaStatus();
            Console.Write(manager.GetSummary());
            Assert.That(status, Is.EqualTo(DbSchemaStatus.RequiresUpgrade));
            Assert.That(manager.DbVersion, Is.EqualTo(new Version("2.3.1")));
        }

    }
}