namespace Merchello.Core.Services
{
    using System;

    /// <inheritdoc/>
    public class ServiceContext : IServiceContext
    {
        #region "Fields"

        /// <summary>
        /// The <see cref="IAuditLogService"/>.
        /// </summary>
        private readonly Lazy<IAuditLogService> _auditLogService;

        /// <summary>
        /// The <see cref="ICustomerService"/>.
        /// </summary>
        private readonly Lazy<ICustomerService> _customerService;

        /// <summary>
        /// The <see cref="IEntityCollectionService"/>.
        /// </summary>
        private readonly Lazy<IEntityCollectionService> _entityCollectionService;

        /// <summary>
        /// The <see cref="IGatewayProviderService"/>.
        /// </summary>
        private readonly Lazy<IGatewayProviderService> _gatewayProviderService;

        /// <summary>
        /// The <see cref="IItemCacheService"/>.
        /// </summary>
        private readonly Lazy<IItemCacheService> _itemCacheService;

        /// <summary>
        /// The <see cref="IInvoiceService"/>.
        /// </summary>
        private readonly Lazy<IInvoiceService> _invoiceService;

        /// <summary>
        /// The <see cref="IPaymentService"/>.
        /// </summary>
        private readonly Lazy<IPaymentService> _paymentService;

        /// <summary>
        /// The <see cref="IMigrationStatusService"/>.
        /// </summary>
        private readonly Lazy<IMigrationStatusService> _migrationStatusService;

        /// <summary>
        /// The <see cref="IOfferSettingsService"/>.
        /// </summary>
        private readonly Lazy<IOfferSettingsService> _offerSettingsService;

        /// <summary>
        /// The <see cref="IOrderService"/>.
        /// </summary>
        private readonly Lazy<IOrderService> _orderService;

        /// <summary>
        /// The <see cref="IProductOptionService"/>.
        /// </summary>
        private readonly Lazy<IProductOptionService> _productOptionService;

        /// <summary>
        /// The <see cref="IProductService"/>.
        /// </summary>
        private readonly Lazy<IProductService> _productService;

        /// <summary>
        /// The <see cref="IShipmentService"/>.
        /// </summary>
        private readonly Lazy<IShipmentService> _shipmentService;

        /// <summary>
        /// The <see cref="IStoreService"/>.
        /// </summary>
        private readonly Lazy<IStoreService> _storeService;

        /// <summary>
        /// The <see cref="IStoreSettingService"/>.
        /// </summary>
        private readonly Lazy<IStoreSettingService> _storeSettingService;

        /// <summary>
        /// The <see cref="IWarehouseService"/>.
        /// </summary>
        private readonly Lazy<IWarehouseService> _warehouseService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="auditLogService">
        /// The <see cref="IAuditLogService"/>.
        /// </param>
        /// <param name="customerService">
        /// The <see cref="ICustomerService"/>
        /// </param>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>.
        /// </param>
        /// <param name="gatewayProviderService">
        /// The <see cref="IGatewayProviderService"/>
        /// </param>
        /// <param name="itemCacheService">
        /// The <see cref="IItemCacheService"/>.
        /// </param>
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        /// <param name="offerSettingsService">
        /// The <see cref="IOfferSettingsService"/>
        /// </param>
        /// <param name="orderService">
        /// The <see cref="IOrderService"/>
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        /// <param name="productOptionService">
        /// The <see cref="IProductOptionService"/>
        /// </param>
        /// <param name="productService">
        /// The <see cref="IProductService"/>
        /// </param>
        /// <param name="shipmentService">
        /// The <see cref="IShipmentService"/>
        /// </param>
        /// <param name="storeService">
        /// The <see cref="IStoreService"/> 
        /// </param>
        /// <param name="storeSettingService">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        /// <param name="warehouseService">
        /// The <see cref="IWarehouseService"/>
        /// </param>
        public ServiceContext(
            Lazy<IAuditLogService> auditLogService, 
            Lazy<ICustomerService> customerService, 
            Lazy<IEntityCollectionService> entityCollectionService, 
            Lazy<IGatewayProviderService> gatewayProviderService,
            Lazy<IItemCacheService> itemCacheService,
            Lazy<IInvoiceService> invoiceService,
            Lazy<IMigrationStatusService> migrationStatusService,
            Lazy<IOfferSettingsService> offerSettingsService,
            Lazy<IOrderService> orderService,
            Lazy<IPaymentService> paymentService,
            Lazy<IProductOptionService> productOptionService,
            Lazy<IProductService> productService,
            Lazy<IShipmentService> shipmentService,
            Lazy<IStoreService> storeService,
            Lazy<IStoreSettingService> storeSettingService,
            Lazy<IWarehouseService> warehouseService)
        {
            _auditLogService = auditLogService;
            _customerService = customerService;
            _entityCollectionService = entityCollectionService;
            _gatewayProviderService = gatewayProviderService;
            _itemCacheService = itemCacheService;
            _invoiceService = invoiceService;
            _migrationStatusService = migrationStatusService;
            _offerSettingsService = offerSettingsService;
            _orderService = orderService;
            _paymentService = paymentService;
            _productOptionService = productOptionService;
            _productService = productService;
            _shipmentService = shipmentService;
            _storeService = storeService;
            _storeSettingService = storeSettingService;
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="auditLogService">
        /// The <see cref="IAuditLogService"/>
        /// </param>
        /// <param name="customerService">
        /// The <see cref="ICustomerService"/>
        /// </param>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>
        /// </param>
        /// <param name="gatewayProviderService">
        /// The <see cref="IGatewayProviderService"/>
        /// </param>
        /// <param name="itemCacheService">
        /// The <see cref="IItemCacheService"/>
        /// </param>
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        /// <param name="offerSettingsService">
        /// The <see cref="IOfferSettingsService"/>
        /// </param>
        /// <param name="orderService">
        /// The <see cref="IOrderService"/>
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        /// <param name="productOptionService">
        /// The <see cref="IProductOptionService"/>
        /// </param>
        /// <param name="productService">
        /// The <see cref="IProductService"/>
        /// </param>
        /// <param name="shipmentService">
        /// The <see cref="IShipmentService"/>
        /// </param>
        /// <param name="storeService">
        /// The <see cref="IStoreService"/>
        /// </param>
        /// <param name="storeSettingService">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        /// <param name="warehouseService">
        /// The <see cref="IWarehouseService"/>
        /// </param>
        public ServiceContext(
            IAuditLogService auditLogService = null,
            ICustomerService customerService = null,
            IEntityCollectionService entityCollectionService = null,
            IGatewayProviderService gatewayProviderService = null,
            IItemCacheService itemCacheService = null,
            IInvoiceService invoiceService = null,
            IMigrationStatusService migrationStatusService = null,
            IOfferSettingsService offerSettingsService = null,
            IOrderService orderService = null,
            IPaymentService paymentService = null,
            IProductOptionService productOptionService = null,
            IProductService productService = null,
            IShipmentService shipmentService = null,
            IStoreService storeService = null,
            IStoreSettingService storeSettingService = null,
            IWarehouseService warehouseService = null)
        {
            if (auditLogService != null) _auditLogService = new Lazy<IAuditLogService>(() => auditLogService);
            if (customerService != null) _customerService = new Lazy<ICustomerService>(() => customerService);
            if (entityCollectionService != null) _entityCollectionService = new Lazy<IEntityCollectionService>(() => entityCollectionService);
            if (gatewayProviderService != null) _gatewayProviderService = new Lazy<IGatewayProviderService>(() => gatewayProviderService);
            if (itemCacheService != null) _itemCacheService = new Lazy<IItemCacheService>(() => itemCacheService);
            if (invoiceService != null) _invoiceService = new Lazy<IInvoiceService>(() => invoiceService);
            if (migrationStatusService != null) _migrationStatusService = new Lazy<IMigrationStatusService>(() => migrationStatusService);
            if (offerSettingsService != null) _offerSettingsService = new Lazy<IOfferSettingsService>(() => offerSettingsService);
            if (orderService != null) _orderService = new Lazy<IOrderService>(() => orderService);
            if (paymentService != null) _paymentService = new Lazy<IPaymentService>(() => paymentService);
            if (productOptionService != null) _productOptionService = new Lazy<IProductOptionService>(() => productOptionService);
            if (productService != null) _productService = new Lazy<IProductService>(() => productService);
            if (shipmentService != null) _shipmentService = new Lazy<IShipmentService>(() => shipmentService);
            if (storeService != null) _storeService = new Lazy<IStoreService>(() => storeService);
            if (storeSettingService != null) _storeSettingService = new Lazy<IStoreSettingService>(() => storeSettingService);
            if (warehouseService != null) _warehouseService = new Lazy<IWarehouseService>(() => warehouseService);
        }


        /// <inheritdoc/>
        public IAuditLogService AuditLogService => _auditLogService.Value;

        /// <inheritdoc/>
        public ICustomerService CustomerService => _customerService.Value;

        /// <inheritdoc/>
        public IEntityCollectionService EntityCollectionService => _entityCollectionService.Value;

        /// <inheritdoc/>
        public IGatewayProviderService GatewayProviderService => _gatewayProviderService.Value;

        /// <inheritdoc/>
        public IItemCacheService ItemCacheService => _itemCacheService.Value;

        /// <inheritdoc/>
        public IInvoiceService InvoiceService => _invoiceService.Value;

        /// <inheritdoc/>
        public IMigrationStatusService MigrationStatusService => _migrationStatusService.Value;

        /// <inheritdoc/>
        public IOfferSettingsService OfferSettingsService => _offerSettingsService.Value;

        /// <inheritdoc/>
        public IOrderService OrderService => _orderService.Value;

        /// <inheritdoc/>
        public IPaymentService PaymentService => _paymentService.Value;

        /// <inheritdoc/>
        public IProductOptionService ProductOptionService => _productOptionService.Value;

        /// <inheritdoc/>
        public IProductService ProductService => _productService.Value;

        /// <inheritdoc/>
        public IShipmentService ShipmentService => _shipmentService.Value;

        /// <inheritdoc/>
        public IStoreService StoreService => _storeService.Value;

        /// <inheritdoc/>
        public IStoreSettingService StoreSettingService => _storeSettingService.Value;

        /// <inheritdoc/>
        public IWarehouseService WarehouseService => _warehouseService.Value;
    }
}