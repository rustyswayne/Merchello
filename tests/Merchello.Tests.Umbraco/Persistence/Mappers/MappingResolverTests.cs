namespace Merchello.Tests.Umbraco.Persistence.Mappers
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class MappingResolverTests : UmbracoApplicationContextTestBase
    {
        protected IMappingResolver Resolver => MappingResolver.Current;

        [Test]
        public void Can_Resolver_AnonymousCustomerMapper()
        {
            var expected = typeof(AnonymousCustomerMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IAnonymousCustomer)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(AnonymousCustomer)).GetType());
        }

        [Test]
        public void Can_Resolver_AppliedPaymentMapper()
        {
            var expected = typeof(AppliedPaymentMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IAppliedPayment)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(AppliedPayment)).GetType());
        }

        [Test]
        public void Can_Resolver_AuditLogMapper()
        {
            var expected = typeof(AuditLogMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IAuditLog)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(AuditLog)).GetType());
        }

        [Test]
        public void Can_Resolver_CatalogInventoryMapper()
        {
            var expected = typeof(CatalogInventoryMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ICatalogInventory)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(CatalogInventory)).GetType());
        }

        [Test]
        public void Can_Resolver_CustomerMapper()
        {
            var expected = typeof(CustomerMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ICustomer)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Customer)).GetType());
        }

        [Test]
        public void Can_Resolver_CustomerAddressMapper()
        {
            var expected = typeof(CustomerAddressMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ICustomerAddress)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(CustomerAddress)).GetType());
        }

        [Test]
        public void Can_Resolver_DetachedContentTypeMapper()
        {
            var expected = typeof(DetachedContentTypeMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IDetachedContentType)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(DetachedContentType)).GetType());
        }

        [Test]
        public void Can_Resolver_EntityCollectionMapper()
        {
            var expected = typeof(EntityCollectionMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IEntityCollection)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(EntityCollection)).GetType());
        }

        [Test]
        public void Can_Resolver_GatewayProviderSettingsMapper()
        {
            var expected = typeof(GatewayProviderSettingsMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IGatewayProviderSettings)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(GatewayProviderSettings)).GetType());
        }

        [Test]
        public void Can_Resolver_InvoiceMapper()
        {
            var expected = typeof(InvoiceMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IInvoice)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Invoice)).GetType());
        }

        [Test]
        public void Can_Resolver_InvoiceLineItemMapper()
        {
            var expected = typeof(InvoiceLineItemMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IInvoiceLineItem)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(InvoiceLineItem)).GetType());
        }

        [Test]
        public void Can_Resolver_InvoiceStatusMapper()
        {
            var expected = typeof(InvoiceStatusMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IInvoiceStatus)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(InvoiceStatus)).GetType());
        }

        [Test]
        public void Can_Resolver_MigrationStatusMapper()
        {
            var expected = typeof(MigrationStatusMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IMigrationStatus)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(MigrationStatus)).GetType());
        }

        [Test]
        public void Can_Resolver_NoteMapper()
        {
            var expected = typeof(NoteMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(INote)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Note)).GetType());
        }

        [Test]
        public void Can_Resolver_NotificationMessageMapper()
        {
            var expected = typeof(NotificationMessageMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(INotificationMessage)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(NotificationMessage)).GetType());
        }

        [Test]
        public void Can_Resolver_NotificationMethodMapper()
        {
            var expected = typeof(NotificationMethodMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(INotificationMethod)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(NotificationMethod)).GetType());
        }

        [Test]
        public void Can_Resolver_OfferRedeemedMapper()
        {
            var expected = typeof(OfferRedeemedMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IOfferRedeemed)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(OfferRedeemed)).GetType());
        }

        [Test]
        public void Can_Resolver_OfferSettingsMapper()
        {
            var expected = typeof(OfferSettingsMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IOfferSettings)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(OfferSettings)).GetType());
        }

        [Test]
        public void Can_Resolver_OrderMapper()
        {
            var expected = typeof(OrderMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IOrder)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Order)).GetType());
        }

        [Test]
        public void Can_Resolver_OrderLineItemMapper()
        {
            var expected = typeof(OrderLineItemMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IOrderLineItem)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(OrderLineItem)).GetType());
        }

        [Test]
        public void Can_Resolver_OrderStatusMapper()
        {
            var expected = typeof(OrderStatusMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IOrderStatus)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(OrderStatus)).GetType());
        }

        [Test]
        public void Can_Resolver_PaymentMapper()
        {
            var expected = typeof(PaymentMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IPayment)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Payment)).GetType());
        }

        [Test]
        public void Can_Resolver_PaymentMethodMapper()
        {
            var expected = typeof(PaymentMethodMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IPaymentMethod)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(PaymentMethod)).GetType());
        }

        [Test]
        public void Can_Resolver_ProductMapper()
        {
            var expected = typeof(ProductMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IProduct)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Product)).GetType());
        }

        [Test]
        public void Can_Resolver_ProductAttributeMapper()
        {
            var expected = typeof(ProductAttributeMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IProductAttribute)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ProductAttribute)).GetType());
        }

        [Test]
        public void Can_Resolver_ProductOptionMapper()
        {
            var expected = typeof(ProductOptionMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IProductOption)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ProductOption)).GetType());
        }

        [Test]
        public void Can_Resolver_ProductVariantMapper()
        {
            var expected = typeof(ProductVariantMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IProductVariant)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ProductVariant)).GetType());
        }

        [Test]
        public void Can_Resolver_ProductVariantDetachedContentMapper()
        {
            var expected = typeof(ProductVariantDetachedContentMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IProductVariantDetachedContent)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ProductVariantDetachedContent)).GetType());
        }

        [Test]
        public void Can_Resolver_ShipCountryMapper()
        {
            var expected = typeof(ShipCountryMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IShipCountry)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ShipCountry)).GetType());
        }

        [Test]
        public void Can_Resolver_ShipmentStatusMapper()
        {
            var expected = typeof(ShipmentStatusMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IShipmentStatus)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ShipmentStatus)).GetType());
        }

        [Test]
        public void Can_Resolver_ShipMethodMapper()
        {
            var expected = typeof(ShipMethodMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IShipMethod)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ShipMethod)).GetType());
        }

        [Test]
        public void Can_Resolver_ShipRateTierMapper()
        {
            var expected = typeof(ShipRateTierMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IShipRateTier)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ShipRateTier)).GetType());
        }

        [Test]
        public void Can_Resolver_StoreMapper()
        {
            var expected = typeof(StoreMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IStore)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Store)).GetType());
        }

        [Test]
        public void Can_Resolver_StoreSettingMapper()
        {
            var expected = typeof(StoreSettingMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IStoreSetting)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(StoreSetting)).GetType());
        }

        [Test]
        public void Can_Resolver_TaxMethodMapper()
        {
            var expected = typeof(TaxMethodMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ITaxMethod)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(TaxMethod)).GetType());
        }

        [Test]
        public void Can_Resolver_TypeFieldMapper()
        {
            var expected = typeof(TypeFieldMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(ITypeField)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(TypeField)).GetType());
        }

        [Test]
        public void Can_Resolver_WarehouseMapper()
        {
            var expected = typeof(WarehouseMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IWarehouse)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(Warehouse)).GetType());
        }

        [Test]
        public void Can_Resolver_WarehouseCatalogMapper()
        {
            var expected = typeof(WarehouseCatalogMapper);

            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(IWarehouseCatalog)).GetType());
            Assert.AreEqual(expected, Resolver.ResolveMapperByType(typeof(WarehouseCatalog)).GetType());
        }
    }
}