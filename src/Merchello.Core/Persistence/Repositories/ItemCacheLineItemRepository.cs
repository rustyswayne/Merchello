namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using LightInject;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal class ItemCacheLineItemRepository : NPocoLineItemRespositoryBase<IItemCacheLineItem, ItemCacheItemDto, ItemCacheLineItemFactory>, IItemCacheLineItemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCacheLineItemRepository"/> class.
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        public ItemCacheLineItemRepository(IDatabaseUnitOfWork work, [Inject(Constants.Repository.DisabledCache)] ICacheHelper cache, ILogger logger, IMapperRegister mappers)
            : base(work, cache, logger, mappers)
        {
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ItemCacheItemDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchItemCacheItem.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchItemCacheItem WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        protected override ItemCacheLineItemFactory GetFactoryInstance()
        {
            return new ItemCacheLineItemFactory();
        }
    }
}