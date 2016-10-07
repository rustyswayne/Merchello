namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// Represents a service that uses repositories that can perform bulk operations
    /// </summary>
    public abstract class RepositoryBulkServiceBase : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBulkServiceBase"/> class.
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
        protected RepositoryBulkServiceBase(IDatabaseBulkUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(logger, eventMessagesFactory)
        {
            Ensure.ParameterNotNull(provider, nameof(provider));
            UowProvider = provider;
        }

        /// <summary>
        /// Gets the unit of work provider.
        /// </summary>
        protected IDatabaseBulkUnitOfWorkProvider UowProvider { get; private set; }
    }
}