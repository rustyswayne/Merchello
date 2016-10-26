namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class ProductService : EntityCollectionEntityServiceBase<IProduct, IDatabaseBulkUnitOfWorkProvider, IProductRepository>, IProductService
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
            : base(provider, logger, eventMessagesFactory, Constants.Locks.ProductTree)
        {
        }

        /// <inheritdoc/>
        public IProduct GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IProduct> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }
    }
}
