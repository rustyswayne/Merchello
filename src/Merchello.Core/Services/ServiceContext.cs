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
        /// The <see cref="IMigrationStatusService"/>.
        /// </summary>
        private readonly Lazy<IMigrationStatusService> _migrationStatusService;

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
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(
            Lazy<IAuditLogService> auditLogService, 
            Lazy<ICustomerService> customerService, 
            Lazy<IEntityCollectionService> entityCollectionService, 
            Lazy<IMigrationStatusService> migrationStatusService)
        {
            _auditLogService = auditLogService;
            _customerService = customerService;
            _entityCollectionService = entityCollectionService;
            _migrationStatusService = migrationStatusService;
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
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(
            IAuditLogService auditLogService = null,
            ICustomerService customerService = null,
            IEntityCollectionService entityCollectionService = null,
            IMigrationStatusService migrationStatusService = null)
        {
            if (auditLogService != null) _auditLogService = new Lazy<IAuditLogService>(() => auditLogService);
            if (customerService != null) _customerService = new Lazy<ICustomerService>(() => customerService);
            if (entityCollectionService != null) _entityCollectionService = new Lazy<IEntityCollectionService>(() => entityCollectionService);
            if (migrationStatusService != null) _migrationStatusService = new Lazy<IMigrationStatusService>(() => migrationStatusService);
        }


        /// <inheritdoc/>
        public IAuditLogService AuditLogService => _auditLogService.Value;

        /// <inheritdoc/>
        public ICustomerService CustomerService => _customerService.Value;

        /// <inheritdoc/>
        public IEntityCollectionService EntityCollectionService => _entityCollectionService.Value;

        /// <inheritdoc/>
        public IMigrationStatusService MigrationStatusService => _migrationStatusService.Value;
    }
}