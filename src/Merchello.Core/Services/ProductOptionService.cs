namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class ProductOptionService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IProductOptionService
    {
        public ProductOptionService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IProductOption GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IProductOption> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductAttribute GetProductAttributeByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IProductAttribute> GetProductAttributes(IEnumerable<Guid> keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductOptionUseCount GetProductOptionUseCount(IProductOption option)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int GetProductOptionShareCount(IProductOption option)
        {
            throw new NotImplementedException();
        }
    }
}