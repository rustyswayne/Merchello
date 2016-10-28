namespace Merchello.Tests.Umbraco.Persistence.Repositories
{
    using System.Linq;

    using Merchello.Core.DI;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    using Semver;

    [TestFixture]
    public class MigrationStatusRepositoryTests : MerchelloRepositoryTestBase
    {
        protected override bool AutoInstall => true;

        [Test]
        public void Can_Add_MigrationStatus()
        {
            //// Arrange
            var status = new MigrationStatus { MigrationName = "MerchelloTest", Version = new SemVersion(3) };


            //// Act
            using (var uow = UowProvider.CreateUnitOfWork())
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