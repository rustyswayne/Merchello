namespace Merchello.Core.Services
{
    using System;

    /// <inheritdoc/>
    public class ServiceContext : IServiceContext
    {
        /// <summary>
        /// The <see cref="IMigrationStatusService"/>.
        /// </summary>
        private readonly Lazy<IMigrationStatusService> _migrationStatusService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(Lazy<IMigrationStatusService> migrationStatusService)
        {
            _migrationStatusService = migrationStatusService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceContext"/> class.
        /// </summary>
        /// <param name="migrationStatusService">
        /// The <see cref="IMigrationStatusService"/>.
        /// </param>
        public ServiceContext(
            IMigrationStatusService migrationStatusService = null)
        {
            if (migrationStatusService != null) _migrationStatusService = new Lazy<IMigrationStatusService>(() => migrationStatusService);
        }

        /// <inheritdoc/>
        public IMigrationStatusService MigrationStatusService => _migrationStatusService.Value;
    }
}