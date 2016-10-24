namespace Merchello.Tests.Umbraco.Services
{
    using System;
    using System.Linq;

    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionServiceTests : EventsServiceTestBase<IEntityCollectionService, IEntityCollection>
    {
        protected IEntityCollectionService Service;

        private Guid _customerTfKey, _customerProviderKey;

        private Guid _productTfKey, _productProviderKey, _productFilterGroupKey;

        private Guid _invoiceTfKey, _invoiceProviderKey;

        private Guid _productColKey;

        private Guid _deleteColKey;

        private Guid _childCollectionKey;


        public override void Initialize()
        {
            base.Initialize();

            this.Service = MC.Services.EntityCollectionService;

            _productColKey = new Guid("B622458F-731D-4FD0-9E72-5135490D3FDE");
            _deleteColKey = new Guid("0E3DEB1C-0C47-4ACE-BA22-2FDBF2E4D981");

            var edXml = new ExtendedDataCollection().SerializeToXml();

            var typeField = new EntityTypeField();
            _customerTfKey = typeField.Customer.TypeKey;
            _productTfKey = typeField.Product.TypeKey;
            _invoiceTfKey = typeField.Invoice.TypeKey;

            _productProviderKey = new Guid("4700456D-A872-4721-8455-1DDAC19F8C16");
            _productFilterGroupKey = new Guid("5316C16C-E967-460B-916B-78985BB7CED2");
            _customerProviderKey = new Guid("A389D41B-C8F1-4289-BD2E-5FFF01DBBDB1");
            _invoiceProviderKey = new Guid("240023BB-80F0-445C-84D5-29F5892B2FB8");

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

            dto.Key = _deleteColKey;
            dto.Name = "Delete Collection Test";
            dto.SortOrder = 1;

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
        public void GetByKey()
        {
            var collection = this.Service.GetByKey(_productColKey);

            Assert.That(collection, Is.Not.Null);
            Assert.That("Product Collection Test", Is.EqualTo(collection.Name));
        }

        [Test]
        public void GetByProviderKey()
        {
            var collection = this.Service.GetByProviderKey(_productProviderKey);

            Assert.That(collection, Is.Not.Null);
        }

        [Test]
        public void GetByEntityType()
        {
            var collection = this.Service.GetByEntityType(EntityType.Product);

            Assert.That(collection, Is.Not.Null);
        }

        [TestCase(EntityType.Product)]
        [TestCase(EntityType.Customer)]
        [TestCase(EntityType.Invoice)]
        [Test]
        public void GetByEntityType_DoesNotThrowForTypes(EntityType entityType)
        {
            Assert.DoesNotThrow(() => this.Service.GetByEntityType(entityType));
        }

        [TestCase(EntityType.EntityCollection)]
        [TestCase(EntityType.GatewayProvider)]
        [TestCase(EntityType.ItemCache)]
        [TestCase(EntityType.Order)]
        [TestCase(EntityType.Payment)]
        [TestCase(EntityType.ProductOption)]
        [TestCase(EntityType.Shipment)]
        [TestCase(EntityType.Warehouse)]
        [TestCase(EntityType.WarehouseCatalog)]
        [Test]
        public void GetByEntityType_DoesThrowForTypes(EntityType entityType)
        {
            Assert.Throws<NotImplementedException>(() => this.Service.GetByEntityType(entityType));
        }

        [Test]
        public void GetChildren()
        {
            var parent = _productColKey;

            var children = this.Service.GetChildren(parent);

            Assert.AreEqual(2, children.Count());
        }

        [Test]
        public void ContainsChildCollection()
        {
            var exists = this.Service.ContainsChildCollection(_productColKey, _childCollectionKey);

            Assert.That(exists, Is.True);
        }

        [Test]
        public void ChildCollectionCount()
        {
            var count = this.Service.ChildEntityCollectionCount(_productColKey);

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public void HasChildCollections()
        {
            var test = this.Service.HasChildEntityCollections(_productColKey);

            Assert.That(test, Is.True);
        }

        [Test]
        public void HasChildCollections_IsFalse()
        {
            var test2 = this.Service.HasChildEntityCollections(_childCollectionKey);

            Assert.That(test2, Is.False);
        }

        [Test]
        public void CollectionCountManagedByProvider()
        {
            var count = this.Service.CollectionCountManagedByProvider(_productProviderKey);

            Assert.That(count, Is.GreaterThan(3));
        }



        [Test]
        public void Create()
        {
            EntityCollectionService.Creating += CreatingHandler;
            EntityCollectionService.Created += CreatedHandler;

            var providerKey = Guid.NewGuid();

            var collection = Service.Create(EntityType.Product, providerKey, "Create Test");

            Assert.That(CreatingEntity, Is.Not.Null, "Creating was null");
            Assert.That(CreatedEntity, Is.Not.Null, "Created was null");

            Assert.That(collection, Is.Not.Null);
            Assert.That(collection.ProviderKey, Is.EqualTo(providerKey));
            Assert.That(collection.EntityTfKey, Is.EqualTo(_productTfKey));
            Assert.That(collection.Name, Is.EqualTo("Create Test"));

            EntityCollectionService.Creating -= CreatingHandler;
            EntityCollectionService.Created -= CreatedHandler;
        }

        [Test]
        public void CreateWithKey()
        {
            EntityCollectionService.Creating += CreatingHandler;
            EntityCollectionService.Created += CreatedHandler;
            EntityCollectionService.Saving += SavingHandler;
            EntityCollectionService.Saved += SavedHandler;

            var collection = Service.CreateWithKey(EntityType.Customer, _customerProviderKey, "Test Customer Collection");
            Assert.That(CreatingEntity, Is.Not.Null, "Creating was null");
            Assert.That(CreatedEntity, Is.Not.Null, "Created was null");
            Assert.That(SavingEntities, Is.Not.Null, "Saving was null");
            Assert.That(SavedEntities, Is.Not.Null, "Saved was null");
            Assert.That(SavedEntities.First().HasIdentity, Is.True, "Saved did not have an identity");

            Assert.That(collection.HasIdentity, Is.True);
            Assert.AreEqual(collection.ProviderKey, _customerProviderKey);
            Assert.AreEqual("Test Customer Collection", collection.Name);

            EntityCollectionService.Creating -= CreatingHandler;
            EntityCollectionService.Created -= CreatedHandler;
            EntityCollectionService.Saving -= SavingHandler;
            EntityCollectionService.Saved -= SavedHandler;
        }

        [Test]
        public void Delete()
        {

            EntityCollectionService.Deleting += DeletingHandler;
            EntityCollectionService.Deleted += DeletedHandler;

            var deleter = Service.GetByKey(_deleteColKey);

            Assert.NotNull(deleter);

            Service.Delete(deleter);

            Assert.That(DeletingEntities, Is.Not.Null, "Deleting was null");
            Assert.That(DeletedEntities, Is.Not.Null, "Deleted was null");

            var retrieved = Service.GetByKey(_deleteColKey);

            Assert.IsNull(retrieved);

            EntityCollectionService.Deleting -= DeletingHandler;
            EntityCollectionService.Deleted -= DeletedHandler;
        }
    }
}