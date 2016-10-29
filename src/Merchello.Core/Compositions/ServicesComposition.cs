namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.Events;
    using Merchello.Core.Services;

    /// <summary>
    /// Sets the IoC container for the Merchello services.
    /// </summary>
    public class ServicesComposition : ICompositionRoot
    {
        /// <summary>
        /// Composes configuration services by adding services to the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry container)
        {
            // Register a transient messages factory, which will be replaced by the web
            // boot manager when running in a web context
            container.Register<IEventMessagesFactory, TransientMessageFactory>();

            // Registers the ActivatorServiceProvider
            // TODO - we need to work toward getting rid of services that require this since
            // all services should be registered in the container directly.
            container.RegisterSingleton<IActivatorServiceProvider, ActivatorServiceProvider>();

            // The service context
            container.RegisterSingleton<IServiceContext, ServiceContext>();

            container.RegisterSingleton<IAuditLogService, AuditLogService>();
            container.RegisterSingleton<ICustomerService, CustomerService>();
            container.RegisterSingleton<IEntityCollectionService, EntityCollectionService>();
            container.RegisterSingleton<IGatewayProviderService, GatewayProviderService>();
            container.RegisterSingleton<IItemCacheService, ItemCacheService>();
            container.RegisterSingleton<IInvoiceService, InvoiceService>();
            container.RegisterSingleton<IMigrationStatusService, MigrationStatusService>();
            container.RegisterSingleton<IOfferSettingsService, OfferSettingsService>();
            container.RegisterSingleton<IOrderService, OrderService>();
            container.RegisterSingleton<IPaymentService, PaymentService>();
            container.RegisterSingleton<IProductService, ProductService>();
            container.RegisterSingleton<IProductOptionService, ProductOptionService>();
            container.RegisterSingleton<IShipmentService, ShipmentService>();
            container.RegisterSingleton<IStoreService, StoreService>();
            container.RegisterSingleton<IStoreSettingService, StoreSettingService>();
            container.RegisterSingleton<IWarehouseService, WarehouseService>();
        }
    }
}