namespace Merchello.Core.Services
{
    /// <summary>
    /// Represents Merchello service context.
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IAuditLogService"/>.
        /// </summary>
        IAuditLogService AuditLogService { get; }

        /// <summary>
        /// Gets the <see cref="ICustomerService"/>.
        /// </summary>
        ICustomerService CustomerService { get; }

        /// <summary>
        /// Gets the <see cref="IEntityCollectionService"/>.
        /// </summary>
        IEntityCollectionService EntityCollectionService { get; }

        /// <summary>
        /// Gets the <see cref="IGatewayProviderService"/>.
        /// </summary>
        IGatewayProviderService GatewayProviderService { get; }

        /// <summary>
        /// Gets the <see cref="IInvoiceService"/>.
        /// </summary>
        IInvoiceService InvoiceService { get; }

        /// <summary>
        /// Gets the <see cref="IItemCacheService"/>.
        /// </summary>
        IItemCacheService ItemCacheService { get; }

        /// <summary>
        /// Gets the <see cref="IMigrationStatusService"/>.
        /// </summary>
        IMigrationStatusService MigrationStatusService { get; }

        /// <summary>
        /// Gets the <see cref="IOfferSettingsService"/>.
        /// </summary>
        IOfferSettingsService OfferSettingsService { get; }

        /// <summary>
        /// Gets the <see cref="IOrderService"/>.
        /// </summary>
        IOrderService OrderService { get; }

        /// <summary>
        /// Gets the <see cref="IPaymentService"/>.
        /// </summary>
        IPaymentService PaymentService { get; }

        /// <summary>
        /// Gets the <see cref="IProductOptionService"/>.
        /// </summary>
        IProductOptionService ProductOptionService { get; }

        /// <summary>
        /// Gets the <see cref="IProductService"/>.
        /// </summary>
        IProductService ProductService { get; }

        /// <summary>
        /// Gets the <see cref="IShipmentService"/>.
        /// </summary>
        IShipmentService ShipmentService { get; }

        /// <summary>
        /// Gets the <see cref="IStoreService"/>.
        /// </summary>
        IStoreService StoreService { get; }

        /// <summary>
        /// Gets the <see cref="IStoreSettingService"/>
        /// </summary>
        IStoreSettingService StoreSettingService { get; }

        /// <summary>
        /// Gets the <see cref="IWarehouseService"/>.
        /// </summary>
        IWarehouseService WarehouseService { get; }
    }
}
