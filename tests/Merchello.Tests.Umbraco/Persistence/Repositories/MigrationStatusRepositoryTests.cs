namespace Merchello.Tests.Umbraco.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;
    using Merchello.Tests.Umbraco.TestHelpers.Base;

    using NPoco;

    using NUnit.Framework;

    using Semver;

    [TestFixture]
    public class MigrationStatusRepositoryTests : MerchelloDatabaseTestBase
    {
        [Test]
        public void Can_Add_MigrationStatus()
        {
            //// Arrange
            var uowProvider = IoC.Container.GetInstance<IDatabaseUnitOfWorkProvider>();
            var status = new MigrationStatus { MigrationName = "MerchelloTest", Version = new SemVersion(3) };


            //// Act
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IMigrationStatusRepository>();
                repo.AddOrUpdate(status);
                uow.Complete();
            }

            //// Assert
            var dtos = Database.Fetch<MigrationStatusDto>();
            Assert.That(dtos.Any(), Is.True);
            var dto = dtos.First();

            Assert.AreEqual("MerchelloTest", dto.Name);
            Assert.AreEqual("3.0.0", dto.Version);
        }
    }
}