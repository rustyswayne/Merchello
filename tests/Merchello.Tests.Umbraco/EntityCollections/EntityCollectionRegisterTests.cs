namespace Merchello.Tests.Umbraco.EntityCollections
{
    using System;
    using System.Linq;

    using Merchello.Core;
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.EntityCollections;
    using Merchello.Core.EntityCollections.Providers;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionRegisterTests : MerchelloDatabaseTestBase
    {
        private Guid _productTfKey, _productProviderKey, _productFilterGroupKey;

        private Guid _productColKey;
        private Guid _childCollectionKey;

        public override void Initialize()
        {
            base.Initialize();

            var edXml = new ExtendedDataCollection().SerializeToXml();

            var typeField = new EntityTypeField();
            _productTfKey = typeField.Product.TypeKey;

            _productColKey = new Guid("B622458F-731D-4FD0-9E72-5135490D3FDE");
            _productProviderKey = new Guid("4700456D-A872-4721-8455-1DDAC19F8C16");
            _productFilterGroupKey = new Guid("5316C16C-E967-460B-916B-78985BB7CED2");

            var dto = new EntityCollectionDto
            {
                Key = _productColKey,
                Name = "Product Collection Test",
                EntityTfKey = _productTfKey,
                ProviderKey = _productProviderKey,
                IsFilter = false,
                SortOrder = 0,
                ExtendedData = edXml,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            Database.Insert(dto);

            // children
            _childCollectionKey = new Guid("38AB050A-BA5A-492C-83BD-5628C636F6FB");

            var dto1 = new EntityCollectionDto
            {
                Key = _childCollectionKey,
                ParentKey = _productColKey,
                Name = "Child Collection1",
                EntityTfKey = _productTfKey,
                ProviderKey = _productProviderKey,
                ExtendedData = edXml,
                IsFilter = false,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };


            var dto2 = new EntityCollectionDto
            {
                Key = Guid.NewGuid(),
                ParentKey = _productColKey,
                Name = "Child Collection2",
                EntityTfKey = _productTfKey,
                ProviderKey = _productProviderKey,
                IsFilter = false,
                ExtendedData = edXml,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            Database.Insert(dto1);
            Database.Insert(dto2);
        }

        [Test]
        public void Count()
        {
            var expected = 4;

            var register = MC.EntityCollectionProviderRegister;

            Assert.That(register.Count, Is.EqualTo(expected));
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
                        var activator = factory.GetInstance<IActivatorServiceProvider>();

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

        [TestCase(typeof(IProductCollectionProvider))]
        [TestCase(typeof(IProductFilterGroupProvider))]
        [TestCase(typeof(IInvoiceCollectionProvider))]
        [TestCase(typeof(ICustomerCollectionProvider))]
        [Test]
        public void GetProviderKey(Type providerType)
        {
            var register = MC.EntityCollectionProviderRegister;

            var key = register.GetProviderKey(providerType);
            Console.WriteLine(key.ToString());
            Assert.That(key, Is.Not.EqualTo(Guid.Empty));
        }

        [TestCase(EntityType.Product)]
        [TestCase(EntityType.Invoice)]
        [TestCase(EntityType.Customer)]
        [Test]
        public void GetProviderTypesForEntityType(EntityType entityType)
        {
            var register = MC.EntityCollectionProviderRegister;

            var providers = register.GetProviderTypesForEntityType(entityType);

            Assert.That(providers.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void GetProvidGetProviderForCollection()
        {
            //// Arrange
            var register = MC.EntityCollectionProviderRegister;
            var service = MC.Services.EntityCollectionService;
            var collection = service.GetByKey(_productColKey);
            Assert.NotNull(collection);

            //// Act
            var provider = register.GetProviderForCollection(collection);

            //// Assert
            Assert.NotNull(provider);
            Assert.NotNull(provider.EntityCollection);
            Assert.AreEqual(provider.EntityCollection.Key, collection.Key);
        }

        [Test]
        public void GetProvidGetProviderForCollection_Typed()
        {

            //// Arrange
            var register = MC.EntityCollectionProviderRegister;
            var service = MC.Services.EntityCollectionService;
            var collection = service.GetByKey(_productColKey);
            Assert.NotNull(collection);

            //// Act
            var provider = register.GetProviderForCollection<StaticProductCollectionProvider>(collection);

            //// Assert
            Assert.NotNull(provider);
            Assert.AreEqual(typeof(StaticProductCollectionProvider), provider.GetType());
        }
    }
}