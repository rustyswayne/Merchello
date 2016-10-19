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
        /// Gets the <see cref="IMigrationStatusService"/>.
        /// </summary>
        IMigrationStatusService MigrationStatusService { get; }

        IStoreSettingService StoreSettingService { get; }
    }
}
