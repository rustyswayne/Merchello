namespace Merchello.Tests.Umbraco
{
    using System;

    using global::Umbraco.Core.Logging;

    using Merchello.Core;
    using Merchello.Core.Cache;
    using Merchello.Core.DependencyInjection;
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

            Assert.NotNull(IoC.Current);


            Assert.NotNull(ApplicationContext.DatabaseContext, "DatabaseContext was null");
            Assert.NotNull(ApplicationContext.DatabaseContext.SqlSyntax, "SqlSyntax was null");

            var mappingResolver = IoC.Container.GetInstance<IMappingResolver>();

            Assert.NotNull(mappingResolver);

            var dbFactory = IoC.Container.GetInstance<IDatabaseFactory>();
            Assert.NotNull(dbFactory);

            var uowProvider = IoC.Container.GetInstance<IDatabaseUnitOfWorkProvider>();
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                Assert.NotNull(repo);
            }

            Assert.That(MerchelloContext.HasCurrent, Is.True);

            //var unitOfWork = IoC.Container.GetInstance<IUnitOfWork>();

            var manager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            manager.UninstallDatabaseSchema();
            //manager.InstallDatabaseSchema();
            Assert.NotNull(manager);

            //var manager = IoC.Container.GetInstance<IMigrationManager>();
            //Assert.NotNull(manager);
            //var version = new Version("0.0.0");
            //Assert.That(manager.DbVersion, Is.EqualTo(version));
            
            var cacheFactory = IoC.Container.GetInstance<ICloneableCacheEntityFactory>();

            Console.WriteLine(cacheFactory.GetType());
        }
    }
}
