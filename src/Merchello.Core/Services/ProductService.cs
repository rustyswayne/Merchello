namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public class ProductService : RepositoryBulkServiceBase, IProductService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
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
        public ProductService(IDatabaseBulkUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }
    }
}
