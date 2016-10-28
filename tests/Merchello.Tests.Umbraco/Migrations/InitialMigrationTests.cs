namespace Merchello.Tests.Umbraco.Migrations
{
    using System;

    using Merchello.Core.DI;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations;

    using NUnit.Framework;

    [TestFixture]
    public class InitialMigrationTests : MigrationTestBase
    {
        private static bool IsUpgradeTest = false;

        protected override bool RequiresMerchelloConfig => true;

        protected override bool UninistallDatabaseOnTearDown => true;

        //[SetUp]
        //public void Setup()
        //{
        //    IsUpgradeTest = false;
        //    // Ensure the database is deleted so we can test how this behaves on installs
        //    var manager = MC.Container.GetInstance<IDatabaseSchemaManager>();

        //    //var creation = GetDbSchemaCreation(manager);
        //    //creation.UninstallDatabaseSchema(V2OrderedTables);
        //    //manager.UninstallDatabaseSchema();
        //}

        [TearDown]
        public override void TearDown()
        {
            var manager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            manager.UninstallDatabaseSchema();
            //if (IsUpgradeTest)
            //{
            //    var creation = GetDbSchemaCreation(manager);
            //    creation.UninstallDatabaseSchema(V2OrderedTables);
            //}
            //else
            //{
            //    manager.UninstallDatabaseSchema();
            //}
        }


        [Test]
        public void Can_Detect_Install_Required()
        {
            var manager = MC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetMigrationInstruction();

            Assert.That(status.PluginInstallStatus, Is.EqualTo(PluginInstallStatus.RequiresInstall));
        }

        [Test]
        public void Can_Install_Database()
        {
            var manager = MC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetMigrationInstruction();

            if (status.PluginInstallStatus != PluginInstallStatus.RequiresInstall)
            {
                Assert.Fail("Database status returned to not install");
            }

            manager.InstallDatabaseSchema();

            manager.Refresh();

            status = manager.GetMigrationInstruction();

            Assert.That(status.PluginInstallStatus, Is.EqualTo(PluginInstallStatus.Current));
        }

        [Test]
        public void Can_Uninstall_Database()
        {
            var schemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.InstallDatabaseSchema();
            var manager = MC.Container.GetInstance<IMigrationManager>();

            var status = manager.GetMigrationInstruction();

            if (status.PluginInstallStatus != PluginInstallStatus.Current)
            {
                Assert.Fail("Database status did not return Current");
            }

            manager.UninstallDatabaseSchema();
            manager.Refresh();
            status = manager.GetMigrationInstruction();

            Assert.That(status.PluginInstallStatus, Is.EqualTo(PluginInstallStatus.RequiresInstall));
        }

        //[Test]
        //[Ignore("Not ready for this")]
        //public void Can_Detect_UpgradeRequired()
        //{
        //    // Toggle to delete the old db version
        //    IsUpgradeTest = true;

        //    //// Arrange
          
        //    // install a V2.3.1 database
        //    var schemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
        //    var dbCreation = GetDbSchemaCreation(schemaManager);
        //    dbCreation.InitializeDatabaseSchema(V2OrderedTables);

        //    //// Act
        //    var manager = MC.Container.GetInstance<IMigrationManager>();

        //    var status = manager.GetMigrationInstruction();
        //    Console.Write(manager.GetSummary());
        //    Assert.That(status, Is.EqualTo(PluginInstallStatus.RequiresUpgrade));
        //    Assert.That(manager.DbVersion, Is.EqualTo(new Version("2.3.1")));
        //}

    }
}