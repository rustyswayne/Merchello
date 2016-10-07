namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public ProductRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IProduct> GetByDetachedContentType(Guid detachedContentTypeKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Guid GetKeyForSlug(string slug)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool SkuExists(string sku)
        {
            throw new NotImplementedException();
        }
    }
}