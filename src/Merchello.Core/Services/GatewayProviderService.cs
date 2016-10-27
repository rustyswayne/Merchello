namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class GatewayProviderService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IGatewayProviderService
    {
        /// <summary>
        /// The <see cref="IInvoiceService"/>.
        /// </summary>
        private readonly Lazy<IInvoiceService> _invoiceService;

        /// <summary>
        /// The <see cref="IPaymentService"/>.
        /// </summary>
        private readonly Lazy<IPaymentService> _paymentService;

        /// <summary>
        /// The <see cref="IStoreSettingService"/>.
        /// </summary>
        private readonly Lazy<IStoreSettingService> _storeSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseBulkUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        /// <param name="storeSettingService">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        public GatewayProviderService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory, Lazy<IInvoiceService> invoiceService, Lazy<IPaymentService> paymentService, Lazy<IStoreSettingService> storeSettingService)
            : base(provider, logger, eventMessagesFactory)
        {
            Ensure.ParameterNotNull(invoiceService, nameof(invoiceService));
            Ensure.ParameterNotNull(paymentService, nameof(paymentService));
            Ensure.ParameterNotNull(storeSettingService, nameof(storeSettingService));

            _invoiceService = invoiceService;
            _paymentService = paymentService;
            _storeSettingService = storeSettingService;
        }


        /// <inheritdoc/>
        public IWarehouse GetDefaultWarehouse()
        {
            //// TODO this will need to change with multi-store.

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IWarehouseRepository>();
                var warehouse = repo.Get(Constants.Warehouse.DefaultWarehouseKey);
                uow.Complete();
                return warehouse;
            }
        }

        /// <inheritdoc/>
        public string GetDefaultCurrencyCode()
        {
            return _storeSettingService.Value.GetByKey(Constants.StoreSetting.CurrencyCodeKey).Value;
        }
    }
}