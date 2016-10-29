namespace Merchello.Tests.Unit.Persistence.Repositories
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.UnitOfWork;
    using Merchello.Tests.Base.Mocks;

    using Moq;

    using NUnit.Framework;
    

    [TestFixture]
    public class DeployableEntityRepositoryTest
    {
        internal MockSimpleRepository<MockDeployableEntity> Repo;

        [OneTimeSetUp]
        public void Initialize()
        {
            Repo = new MockSimpleRepository<MockDeployableEntity>(Mock.Of<IUnitOfWork>(), Mock.Of<ICacheHelper>(), Mock.Of<ILogger>());
        }

        [Test]
        public void AddItem_Does_Not_Reset_AssignedKey()
        {
            //// Arrange
            var key = Guid.NewGuid();

            //// Act
            var entity = new MockDeployableEntity();
            entity.SetKeyForDeploymentCreate(key);
            Repo.AddOrUpdate(entity);

            //// Assert
            Assert.That(entity.Key, Is.EqualTo(key));

        }
    }
}