namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal class DetachedContentTypeRepository : NPocoEntityRepositoryBase<IDetachedContentType, DetachedContentTypeDto, DetachedContentTypeFactory>, IDetachedContentTypeRepository
    {
        /// <summary>
        /// The <see cref="IProductOptionRepository"/>.
        /// </summary>
        private readonly IProductOptionRepository _productOptionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetachedContentTypeRepository"/> class.
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
        /// <param name="productOptionRepository">
        /// The <see cref="IProductOptionRepository"/>
        /// </param>
        public DetachedContentTypeRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IProductOptionRepository productOptionRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(productOptionRepository, nameof(productOptionRepository));
            _productOptionRepository = productOptionRepository;
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<DetachedContentTypeDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchDetachedContentType.pk = @Key";
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(IDetachedContentType entity)
        {
            // Remove the detached content reference from product options that use this type
            // before deleting.
            _productOptionRepository.UpdateForDetachedContentDelete(entity.Key);

            base.PersistDeletedItem(entity);
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchProductVariantDetachedContent WHERE merchProductVariantDetachedContent.detachedContentTypeKey = @Key",
                    "DELETE FROM merchDetachedContentType WHERE merchDetachedContentType.pk = @Key"
                };

            return list;
        }

        /// <inheritdoc/>
        protected override DetachedContentTypeFactory GetFactoryInstance()
        {
            return new DetachedContentTypeFactory();
        }
    }
}