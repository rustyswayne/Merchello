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
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(Lazy<IAuditLogService> auditLogService, Lazy<IMigrationStatusService> migrationStatusService)
        {
            _auditLogService = auditLogService;
            _migrationStatusService = migrationStatusService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="auditLogService">
        /// The <see cref="IAuditLogService"/>
        /// </param>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(
            IAuditLogService auditLogService = null,
            IMigrationStatusService migrationStatusService = null)
        {
            if (auditLogService != null) _auditLogService = new Lazy<IAuditLogService>(() => auditLogService);
            if (migrationStatusService != null) _migrationStatusService = new Lazy<IMigrationStatusService>(() => migrationStatusService);
        }


        /// <inheritdoc/>
        public IAuditLogService AuditLogService => _auditLogService.Value;

        /// <inheritdoc/>
        public IMigrationStatusService MigrationStatusService => _migrationStatusService.Value;
    }
}