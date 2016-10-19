namespace Merchello.Tests.Umbraco.Migrations
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Migrations.Initial;
    using Merchello.Tests.Base;
    using Merchello.Tests.Umbraco.Migrations.V2Dtos;

    using InvoiceDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.InvoiceDto;
    using ItemCacheDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.ItemCacheDto;
    using OfferSettingsDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.OfferSettingsDto;
    using StoreSettingDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.StoreSettingDto;
    using WarehouseDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.WarehouseDto;

    public abstract class MigrationTestBase : UmbracoApplicationContextTestBase
    {
        /// <summary>
        /// The ordered tables.
        /// </summary>
        protected static readonly Dictionary<int, Type> V2OrderedTables = new Dictionary<int, Type>
        {
            { 0, typeof(TypeFieldDto) },
            { 1, typeof(DetachedContentTypeDto) },
            { 2, typeof(AnonymousCustomerDto) },
            { 3, typeof(CustomerDto) },
            { 4, typeof(CustomerIndexDto) },
            { 5, typeof(CustomerAddressDto) },
            { 6, typeof(ItemCacheDto) },
            { 7, typeof(ItemCacheItemDto) },
            { 8, typeof(GatewayProviderSettingsDto) },
            { 9, typeof(WarehouseDto) },
            { 10, typeof(WarehouseCatalogDto) },
            { 11, typeof(ShipCountryDto) },
            { 12, typeof(ShipMethodDto) },
            { 13, typeof(ShipRateTierDto) },
            { 14, typeof(InvoiceStatusDto) },
            { 15, typeof(InvoiceDto) },
            { 16, typeof(InvoiceItemDto) },
            { 17, typeof(InvoiceIndexDto) },
            { 18, typeof(OrderStatusDto) },
            { 19, typeof(OrderDto) },
            { 20, typeof(ShipmentStatusDto) },
            { 21, typeof(ShipmentDto) },
            { 22, typeof(OrderItemDto) },
            { 23, typeof(PaymentMethodDto) },
            { 24, typeof(PaymentDto) },
            { 25, typeof(ProductDto) },
            { 26, typeof(ProductVariantDto) },
            { 27, typeof(ProductOptionDto) },
            { 28, typeof(ProductAttributeDto) },
            { 29, typeof(Product2ProductOptionDto) },
            { 30, typeof(CatalogInventoryDto) },
            { 31, typeof(TaxMethodDto) },
            { 32, typeof(ProductVariant2ProductAttributeDto) },
            { 33, typeof(AppliedPaymentDto) },
            { 34, typeof(ProductVariantIndexDto) },
            { 35, typeof(StoreSettingDto) },
            { 36, typeof(OrderIndexDto) },
            { 37, typeof(NotificationMethodDto) },
            { 38, typeof(NotificationMessageDto) },
            { 39, typeof(AuditLogDto) },
            { 40, typeof(OfferSettingsDto) },
            { 41, typeof(OfferRedeemedDto) },
            { 42, typeof(EntityCollectionDto) },
            { 43, typeof(Product2EntityCollectionDto) },
            { 44, typeof(Invoice2EntityCollectionDto) },
            { 45, typeof(Customer2EntityCollectionDto) },
            { 46, typeof(ProductVariantDetachedContentDto) },
            { 47, typeof(NoteDto) },
            { 48, typeof(ProductOptionAttributeShareDto) }
        };

        

        internal static DatabaseSchemaCreation GetDbSchemaCreation(IDatabaseSchemaManager manager)
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            var logger = IoC.Container.GetInstance<ILogger>();

            return new DatabaseSchemaCreation(dbAdapter, logger, manager);
        }
    }
}