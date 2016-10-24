namespace Merchello.Tests.Umbraco.EntityCollections
{
    using System;

    using LightInject;

    using Merchello.Core;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.EntityCollections.Providers;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionRegisterTests : UmbracoRuntimeTestBase
    {
        public void Count()
        {
        }

        [Test]
        public void Can_Create_Instance_FromContainer()
        {
            var container = MC.Container;

            var t = typeof(ProductFilterGroupProvider);

            var key = Guid.NewGuid();

            container.Register<Guid, IEntityCollectionProvider>(
                (factory, value) =>
                    {
                        var activator = factory.GetInstance<ActivatorServiceProvider>();

                        var provider = activator.GetService<IEntityCollectionProvider>(
                            t, 
                            new object[]
                            {
                              factory.GetInstance(typeof(IProductService)) as IProductService,
                                factory.GetInstance<IEntityCollectionService>(),
                                factory.GetInstance<ICacheHelper>(),
                                value
                            });

                        return provider;
                    },
                t.Name);

            Console.WriteLine(t.Name);

            var instance = container.GetInstance<Guid, IEntityCollectionProvider>(key, t.Name);

            Assert.NotNull(instance);

            Assert.NotNull(container.GetAvailableService<IEntityCollectionProvider>(t.Name), "Service not available by name");
        }
    }
}