namespace Merchello.Tests.Umbraco.Persistence.Mappers
{
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class MapperRegisterTests : UmbracoRuntimeTestBase
    {
        protected IMapperRegister Register;

        public override void Initialize()
        {
            base.Initialize();
            Register = MC.Container.GetInstance<IMapperRegister>();
            
        }


        [Test]
        public void Register_Contains_AnonymousCustomerMapper()
        {
            var expected = typeof(AnonymousCustomerMapper);

            Assert.AreEqual(expected, Register[typeof(IAnonymousCustomer)].GetType());
            Assert.AreEqual(expected, Register[typeof(AnonymousCustomer)].GetType());
        }

        [Test]
        public void Register_Contains_AppliedPaymentMapper()
        {
            var expected = typeof(AppliedPaymentMapper);

            Assert.AreEqual(expected, Register[typeof(IAppliedPayment)].GetType());
            Assert.AreEqual(expected, Register[typeof(AppliedPayment)].GetType());
        }

        [Test]
        public void Register_Contains_AuditLogMapper()
        {
            var expected = typeof(AuditLogMapper);

            Assert.AreEqual(expected, Register[typeof(IAuditLog)].GetType());
            Assert.AreEqual(expected, Register[typeof(AuditLog)].GetType());
        }

        [Test]
        public void Register_Contains_CatalogInventoryMapper()
        {
            var expected = typeof(CatalogInventoryMapper);

            Assert.AreEqual(expected, Register[typeof(ICatalogInventory)].GetType());
            Assert.AreEqual(expected, Register[typeof(CatalogInventory)].GetType());
        }

        [Test]
        public void Register_Contains_CustomerMapper()
        {
            var expected = typeof(CustomerMapper);

            Assert.AreEqual(expected, Register[typeof(ICustomer)].GetType());
            Assert.AreEqual(expected, Register[typeof(Customer)].GetType());
        }

        [Test]
        public void Register_Contains_CustomerAddressMapper()
        {
            var expected = typeof(CustomerAddressMapper);

            Assert.AreEqual(expected, Register[typeof(ICustomerAddress)].GetType());
            Assert.AreEqual(expected, Register[typeof(CustomerAddress)].GetType());
        }

        [Test]
        public void Register_Contains_DetachedContentTypeMapper()
        {
            var expected = typeof(DetachedContentTypeMapper);

            Assert.AreEqual(expected, Register[typeof(IDetachedContentType)].GetType());
            Assert.AreEqual(expected, Register[typeof(DetachedContentType)].GetType());
        }

        [Test]
        public void Register_Contains_EntityCollectionMapper()
        {
            var expected = typeof(EntityCollectionMapper);

            Assert.AreEqual(expected, Register[typeof(IEntityCollection)].GetType());
            Assert.AreEqual(expected, Register[typeof(EntityCollection)].GetType());
        }

        [Test]
        public void Register_Contains_GatewayProviderSettingsMapper()
        {
            var expected = typeof(GatewayProviderSettingsMapper);

            Assert.AreEqual(expected, Register[typeof(IGatewayProviderSettings)].GetType());
            Assert.AreEqual(expected, Register[typeof(GatewayProviderSettings)].GetType());
        }

        [Test]
        public void Register_Contains_InvoiceMapper()
        {
            var expected = typeof(InvoiceMapper);

            Assert.AreEqual(expected, Register[typeof(IInvoice)].GetType());
            Assert.AreEqual(expected, Register[typeof(Invoice)].GetType());
        }

        [Test]
        public void Register_Contains_InvoiceLineItemMapper()
        {
            var expected = typeof(InvoiceLineItemMapper);

            Assert.AreEqual(expected, Register[typeof(IInvoiceLineItem)].GetType());
            Assert.AreEqual(expected, Register[typeof(InvoiceLineItem)].GetType());
        }

        [Test]
        public void Register_Contains_InvoiceStatusMapper()
        {
            var expected = typeof(InvoiceStatusMapper);

            Assert.AreEqual(expected, Register[typeof(IInvoiceStatus)].GetType());
            Assert.AreEqual(expected, Register[typeof(InvoiceStatus)].GetType());
        }

        [Test]
        public void Register_Contains_MigrationStatusMapper()
        {
            var expected = typeof(MigrationStatusMapper);

            Assert.AreEqual(expected, Register[typeof(IMigrationStatus)].GetType());
            Assert.AreEqual(expected, Register[typeof(MigrationStatus)].GetType());
        }

        [Test]
        public void Register_Contains_NoteMapper()
        {
            var expected = typeof(NoteMapper);

            Assert.AreEqual(expected, Register[typeof(INote)].GetType());
            Assert.AreEqual(expected, Register[typeof(Note)].GetType());
        }

        [Test]
        public void Register_Contains_NotificationMessageMapper()
        {
            var expected = typeof(NotificationMessageMapper);

            Assert.AreEqual(expected, Register[typeof(INotificationMessage)].GetType());
            Assert.AreEqual(expected, Register[typeof(NotificationMessage)].GetType());
        }

        [Test]
        public void Register_Contains_NotificationMethodMapper()
        {
            var expected = typeof(NotificationMethodMapper);

            Assert.AreEqual(expected, Register[typeof(INotificationMethod)].GetType());
            Assert.AreEqual(expected, Register[typeof(NotificationMethod)].GetType());
        }

        [Test]
        public void Register_Contains_OfferRedeemedMapper()
        {
            var expected = typeof(OfferRedeemedMapper);

            Assert.AreEqual(expected, Register[typeof(IOfferRedeemed)].GetType());
            Assert.AreEqual(expected, Register[typeof(OfferRedeemed)].GetType());
        }

        [Test]
        public void Register_Contains_OfferSettingsMapper()
        {
            var expected = typeof(OfferSettingsMapper);

            Assert.AreEqual(expected, Register[typeof(IOfferSettings)].GetType());
            Assert.AreEqual(expected, Register[typeof(OfferSettings)].GetType());
        }

        [Test]
        public void Register_Contains_OrderMapper()
        {
            var expected = typeof(OrderMapper);

            Assert.AreEqual(expected, Register[typeof(IOrder)].GetType());
            Assert.AreEqual(expected, Register[typeof(Order)].GetType());
        }

        [Test]
        public void Register_Contains_OrderLineItemMapper()
        {
            var expected = typeof(OrderLineItemMapper);

            Assert.AreEqual(expected, Register[typeof(IOrderLineItem)].GetType());
            Assert.AreEqual(expected, Register[typeof(OrderLineItem)].GetType());
        }

        [Test]
        public void Register_Contains_OrderStatusMapper()
        {
            var expected = typeof(OrderStatusMapper);

            Assert.AreEqual(expected, Register[typeof(IOrderStatus)].GetType());
            Assert.AreEqual(expected, Register[typeof(OrderStatus)].GetType());
        }

        [Test]
        public void Register_Contains_PaymentMapper()
        {
            var expected = typeof(PaymentMapper);

            Assert.AreEqual(expected, Register[typeof(IPayment)].GetType());
            Assert.AreEqual(expected, Register[typeof(Payment)].GetType());
        }

        [Test]
        public void Register_Contains_PaymentMethodMapper()
        {
            var expected = typeof(PaymentMethodMapper);

            Assert.AreEqual(expected, Register[typeof(IPaymentMethod)].GetType());
            Assert.AreEqual(expected, Register[typeof(PaymentMethod)].GetType());
        }

        [Test]
        public void Register_Contains_ProductMapper()
        {
            var expected = typeof(ProductMapper);

            Assert.AreEqual(expected, Register[typeof(IProduct)].GetType());
            Assert.AreEqual(expected, Register[typeof(Product)].GetType());
        }

        [Test]
        public void Register_Contains_ProductAttributeMapper()
        {
            var expected = typeof(ProductAttributeMapper);

            Assert.AreEqual(expected, Register[typeof(IProductAttribute)].GetType());
            Assert.AreEqual(expected, Register[typeof(ProductAttribute)].GetType());
        }

        [Test]
        public void Register_Contains_ProductOptionMapper()
        {
            var expected = typeof(ProductOptionMapper);

            Assert.AreEqual(expected, Register[typeof(IProductOption)].GetType());
            Assert.AreEqual(expected, Register[typeof(ProductOption)].GetType());
        }

        [Test]
        public void Register_Contains_ProductVariantMapper()
        {
            var expected = typeof(ProductVariantMapper);

            Assert.AreEqual(expected, Register[typeof(IProductVariant)].GetType());
            Assert.AreEqual(expected, Register[typeof(ProductVariant)].GetType());
        }

        [Test]
        public void Register_Contains_ProductVariantDetachedContentMapper()
        {
            var expected = typeof(ProductVariantDetachedContentMapper);

            Assert.AreEqual(expected, Register[typeof(IProductVariantDetachedContent)].GetType());
            Assert.AreEqual(expected, Register[typeof(ProductVariantDetachedContent)].GetType());
        }

        [Test]
        public void Register_Contains_ShipCountryMapper()
        {
            var expected = typeof(ShipCountryMapper);

            Assert.AreEqual(expected, Register[typeof(IShipCountry)].GetType());
            Assert.AreEqual(expected, Register[typeof(ShipCountry)].GetType());
        }

        [Test]
        public void Register_Contains_ShipmentStatusMapper()
        {
            var expected = typeof(ShipmentStatusMapper);

            Assert.AreEqual(expected, Register[typeof(IShipmentStatus)].GetType());
            Assert.AreEqual(expected, Register[typeof(ShipmentStatus)].GetType());
        }

        [Test]
        public void Register_Contains_ShipMethodMapper()
        {
            var expected = typeof(ShipMethodMapper);

            Assert.AreEqual(expected, Register[typeof(IShipMethod)].GetType());
            Assert.AreEqual(expected, Register[typeof(ShipMethod)].GetType());
        }

        [Test]
        public void Register_Contains_ShipRateTierMapper()
        {
            var expected = typeof(ShipRateTierMapper);

            Assert.AreEqual(expected, Register[typeof(IShipRateTier)].GetType());
            Assert.AreEqual(expected, Register[typeof(ShipRateTier)].GetType());
        }

        [Test]
        public void Register_Contains_StoreMapper()
        {
            var expected = typeof(StoreMapper);

            Assert.AreEqual(expected, Register[typeof(IStore)].GetType());
            Assert.AreEqual(expected, Register[typeof(Store)].GetType());
        }

        [Test]
        public void Register_Contains_StoreSettingMapper()
        {
            var expected = typeof(StoreSettingMapper);

            Assert.AreEqual(expected, Register[typeof(IStoreSetting)].GetType());
            Assert.AreEqual(expected, Register[typeof(StoreSetting)].GetType());
        }

        [Test]
        public void Register_Contains_TaxMethodMapper()
        {
            var expected = typeof(TaxMethodMapper);

            Assert.AreEqual(expected, Register[typeof(ITaxMethod)].GetType());
            Assert.AreEqual(expected, Register[typeof(TaxMethod)].GetType());
        }

        [Test]
        public void Register_Contains_TypeFieldMapper()
        {
            var expected = typeof(TypeFieldMapper);

            Assert.AreEqual(expected, Register[typeof(ITypeField)].GetType());
            Assert.AreEqual(expected, Register[typeof(TypeField)].GetType());
        }

        [Test]
        public void Register_Contains_WarehouseMapper()
        {
            var expected = typeof(WarehouseMapper);

            Assert.AreEqual(expected, Register[typeof(IWarehouse)].GetType());
            Assert.AreEqual(expected, Register[typeof(Warehouse)].GetType());
        }

        [Test]
        public void Register_Contains_WarehouseCatalogMapper()
        {
            var expected = typeof(WarehouseCatalogMapper);

            Assert.AreEqual(expected, Register[typeof(IWarehouseCatalog)].GetType());
            Assert.AreEqual(expected, Register[typeof(WarehouseCatalog)].GetType());
        }
    }
}