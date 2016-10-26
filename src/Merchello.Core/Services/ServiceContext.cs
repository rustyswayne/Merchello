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
        /// The <see cref="IStoreSettingService"/>.
        /// </summary>
        private readonly Lazy<IStoreSettingService> _storeSettingService;

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
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        /// <param name="storeSettingService">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        public ServiceContext(
            Lazy<IAuditLogService> auditLogService, 
            Lazy<ICustomerService> customerService, 
            Lazy<IEntityCollectionService> entityCollectionService, 
            Lazy<IInvoiceService> invoiceService,
            Lazy<IMigrationStatusService> migrationStatusService,
            Lazy<IPaymentService> paymentService,
            Lazy<IStoreSettingService> storeSettingService)
        {
            _auditLogService = auditLogService;
            _customerService = customerService;
            _entityCollectionService = entityCollectionService;
            _invoiceService = invoiceService;
            _migrationStatusService = migrationStatusService;
            _paymentService = paymentService;
            _storeSettingService = storeSettingService;
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
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        /// <param name="storeSettingServcie">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        public ServiceContext(
            IAuditLogService auditLogService = null,
            ICustomerService customerService = null,
            IEntityCollectionService entityCollectionService = null,
            IInvoiceService invoiceService = null,
            IMigrationStatusService migrationStatusService = null,
            IPaymentService paymentService = null,
            IStoreSettingService storeSettingServcie = null)
        {
            if (auditLogService != null) _auditLogService = new Lazy<IAuditLogService>(() => auditLogService);
            if (customerService != null) _customerService = new Lazy<ICustomerService>(() => customerService);
            if (entityCollectionService != null) _entityCollectionService = new Lazy<IEntityCollectionService>(() => entityCollectionService);
            if (invoiceService != null) _invoiceService = new Lazy<IInvoiceService>(() => invoiceService);
            if (migrationStatusService != null) _migrationStatusService = new Lazy<IMigrationStatusService>(() => migrationStatusService);
            if (paymentService != null) _paymentService = new Lazy<IPaymentService>(() => paymentService);
            if (storeSettingServcie != null) _storeSettingService = new Lazy<IStoreSettingService>(() => storeSettingServcie);
        }


        /// <inheritdoc/>
        public IAuditLogService AuditLogService => _auditLogService.Value;

        /// <inheritdoc/>
        public ICustomerService CustomerService => _customerService.Value;

        /// <inheritdoc/>
        public IEntityCollectionService EntityCollectionService => _entityCollectionService.Value;

        /// <summary>
        /// The <see cref="IInvoiceService"/>.
        /// </summary>
        public IInvoiceService InvoiceService => _invoiceService.Value;

        /// <inheritdoc/>
        public IMigrationStatusService MigrationStatusService => _migrationStatusService.Value;

        /// <summary>
        /// The <see cref="IPaymentService"/>.
        /// </summary>
        public IPaymentService PaymentService => _paymentService.Value;

        /// <inheritdoc/>
        public IStoreSettingService StoreSettingService => _storeSettingService.Value;
    }
}