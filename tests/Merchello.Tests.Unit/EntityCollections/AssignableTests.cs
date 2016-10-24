namespace Merchello.Tests.Unit.EntityCollections
{
    using System;

    using LightInject;

    using Merchello.Core.EntityCollections;
    using Merchello.Core.EntityCollections.Providers;

    using NUnit.Framework;

    [TestFixture]
    public class AssignableTests
    {
        [Test]
        public void ProductFilterGroupProvider_IsAssignable_From_IProductEntityCollectionProvider()
        {
            var type = typeof(ProductFilterGroupProvider);

            Assert.That(typeof(IEntityCollectionProvider).IsAssignableFrom(type), Is.True);
            Assert.That(typeof(IProductFilterGroupProvider).IsAssignableFrom(type), Is.True);
        }
    }
}