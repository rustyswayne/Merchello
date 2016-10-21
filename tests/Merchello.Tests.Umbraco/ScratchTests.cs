namespace Merchello.Tests.Umbraco
{
    using System;

    using global::Umbraco.Core.Logging;

    using Merchello.Core;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class ScratchTests : UmbracoApplicationContextTestBase
    {
        [Test]
        public void LogTest()
        { 
            Logger.Info<ScratchTests>("Logging test");

            Assert.NotNull(this.DatabaseContext, "DatabaseContext was null");
            Assert.NotNull(this.DatabaseContext.SqlSyntax, "SqlSyntax was null");

            var dbFactory = MC.Container.GetInstance<IDatabaseFactory>();
            Assert.NotNull(dbFactory);

            var uowProvider = MC.Container.GetInstance<IDatabaseUnitOfWorkProvider>();
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                Assert.NotNull(repo);
            }

            var merchelloContext = MerchelloContext.Current;

            Assert.NotNull(merchelloContext.Services);

            //var unitOfWork = IoC.Container.GetInstance<IUnitOfWork>();

            var manager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            manager.UninstallDatabaseSchema();
            //manager.InstallDatabaseSchema();
            Assert.NotNull(manager);

            //var manager = IoC.Container.GetInstance<IMigrationManager>();
            //Assert.NotNull(manager);
            //var version = new Version("0.0.0");
            //Assert.That(manager.DbVersion, Is.EqualTo(version));
            
            var cacheFactory = MC.Container.GetInstance<ICloneableCacheEntityFactory>();

            var ecRegister = MC.EntityCollectionProviderRegister;

            Console.WriteLine(ecRegister.Count);
        }
    }
}
