namespace Merchello.Tests.Unit.Models.EntityBase
{
    using System;

    using Merchello.Tests.Base.Mocks;

    using NUnit.Framework;

    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void HasIdentity()
        {
            //// Arrange
            var entity = new MockEntity();
            Assert.IsFalse(entity.HasIdentity);

            //// Act
            entity.Key = Guid.NewGuid();

            //// Assert
            Assert.IsTrue(entity.HasIdentity);
        }

        [Test]
        public void Deployable_HasIdentity()
        {
            //// Arrange
            var entity = new MockDeployableEntity();
            Assert.IsFalse(entity.HasIdentity);


            //// Act
            entity.SetKeyForDeploymentCreate(Guid.NewGuid());

            //// Assert
            Assert.IsFalse(entity.HasIdentity);
            Assert.AreNotEqual(Guid.Empty, entity.Key);
        }

        [Test]
        public void AddingEntity()
        {
            //// Arrange
            var entity = new MockEntity();
            Assert.AreEqual(entity.CreateDate, default(DateTime));
            Assert.AreEqual(entity.UpdateDate, default(DateTime));

            //// Act
            entity.AddingEntity();

            //// Assert
            Assert.IsTrue(entity.IsDirty());
            Assert.IsTrue(entity.IsPropertyDirty("CreateDate"));
            Assert.IsTrue(entity.IsPropertyDirty("UpdateDate"));
            Assert.AreNotEqual(default(DateTime), entity.CreateDate);
            Assert.AreNotEqual(default(DateTime), entity.UpdateDate);
        }

        [Test]
        public void UpdatingEntity()
        {
            //// Arrange
            var dt = DateTime.Now.AddDays(-1);
            var key = Guid.NewGuid();
            var entity = new MockEntity() { Key = key, CreateDate = dt, UpdateDate = dt };
            entity.ResetDirtyProperties();

            //// Act
            entity.UpdatingEntity();

            //// Assert
            Assert.IsTrue(entity.IsDirty());
            Assert.IsFalse(entity.IsPropertyDirty("CreateDate"));
            Assert.IsTrue(entity.IsPropertyDirty("UpdateDate"));
            Assert.AreEqual(key, entity.Key);
            Assert.AreEqual(dt, entity.CreateDate);
            Assert.AreNotEqual(dt, entity.UpdateDate);
        }
    }
}