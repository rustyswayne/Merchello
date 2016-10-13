namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Cache;
    using Merchello.Core.Cache.Policies;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.Querying;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class EntityCollectionRepository : IEntityCollectionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionRepository"/> class.
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
        public EntityCollectionRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetEntityCollectionsByProductKey(Guid productKey, bool isFilter = false)
        {
            // Inner SQL expression
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.EntityCollectionKey)
                            .From<Product2EntityCollectionDto>()
                            .Where<Product2EntityCollectionDto>(x => x.ProductKey == productKey);

            var sql = GetBaseQuery(false)
                            .Where<EntityCollectionDto>(x => x.IsFilter == isFilter)
                            .AndIn<EntityCollectionDto>(x => x.Key, innerSql);

            var dtos = Database.Fetch<EntityCollectionDto>(sql);

            return dtos.DistinctBy(x => x.Key).Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetEntityCollectionsByInvoiceKey(Guid invoiceKey)
        {
            // Inner SQL expression
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey)
                                .From<Invoice2EntityCollectionDto>()
                                .Where<Invoice2EntityCollectionDto>(x => x.InvoiceKey == invoiceKey);

            var sql = this.GetBaseQuery(false)
                        .SingleWhereIn<EntityCollectionDto>(x => x.Key, innerSql);

            var dtos = Database.Fetch<EntityCollectionDto>(sql);
            return dtos.DistinctBy(x => x.Key).Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetEntityCollectionsByCustomerKey(Guid customerKey)
        {
            // Inner SQL expression
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.EntityCollectionKey)
                                .From<Customer2EntityCollectionDto>()
                                .Where<Customer2EntityCollectionDto>(x => x.CustomerKey == customerKey);

            var sql = this.GetBaseQuery(false)
                        .SingleWhereIn<EntityCollectionDto>(x => x.Key, innerSql);


            var dtos = Database.Fetch<EntityCollectionDto>(sql);
            return dtos.DistinctBy(x => x.Key).Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        public PagedCollection<IEntityCollection> GetPage(IQuery<IEntityCollection> query, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var translator = new SqlTranslator<IEntityCollection>(GetBaseQuery(false), query);
            var sql = translator.Translate().AppendOrderExpression<EntityCollectionDto>(orderExpression, direction);

            return Database.Page<EntityCollectionDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public IEntityFilterGroup GetEntityFilterGroup(Guid key)
        {
            return ((EntityCollectionRepositoryCachePolicy)CachePolicy).Get(key, PerformGetEntityFilterGroup);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsByProviderKeys(Guid[] keys)
        {
            var sql = Sql().Select("pk").From<EntityCollectionDto>()
                .Where<EntityCollectionDto>(x => x.ParentKey == null)
                .WhereIn<EntityCollectionDto>(x => x.ProviderKey, keys);

            var matches = Database.Fetch<KeyDto>(sql);

            return !matches.Any() ?
                Enumerable.Empty<IEntityFilterGroup>() :
                matches.Select(x => this.GetEntityFilterGroup(x.Key)).Where(x => x != null);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsContainingProduct(Guid[] keys, Guid productKey)
        {
            // distinct collection keys by product key
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.EntityCollectionKey)
                                .From<Product2EntityCollectionDto>()
                                .Where<Product2EntityCollectionDto>(x => x.ProductKey == productKey);

            // collections query
            var sql = Sql().Select("pk").From<EntityCollectionDto>()
                            .WhereIn<EntityCollectionDto>(x => x.ProviderKey, keys)
                            .AndIn<EntityCollectionDto>(x => x.Key, innerSql);

            var matches = Database.Fetch<KeyDto>(sql);

            return !matches.Any() ?
                Enumerable.Empty<IEntityFilterGroup>() :
                matches.Select(x => this.GetEntityFilterGroup(x.Key)).Where(x => x != null);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsNotContainingProduct(Guid[] keys, Guid productKey)
        {
            // distinct collections by product
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.EntityCollectionKey)
                                .From<Product2EntityCollectionDto>()
                                .Where<Product2EntityCollectionDto>(x => x.ProductKey == productKey);

            // collections query
            var sql = Sql().Select("pk")
                .From<EntityCollectionDto>()
                .WhereIn<EntityCollectionDto>(x => x.ProviderKey, keys)
                .AndNotIn<EntityCollectionDto>(x => x.Key, innerSql);

            var matches = Database.Fetch<KeyDto>(sql);

            return !matches.Any() ?
                Enumerable.Empty<IEntityFilterGroup>() :
                matches.Select(x => this.GetEntityFilterGroup(x.Key)).Where(x => x != null);
        }

        /// <summary>
        /// Performs the work of getting an <see cref="IEntityFilterGroup"/>.
        /// </summary>
        /// <param name="key">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityFilterGroup"/>.
        /// </returns>
        protected IEntityFilterGroup PerformGetEntityFilterGroup(Guid key)
        {
            var collection = Get(key);
            if (collection == null) return null;

            var query = Query.Where(x => x.ParentKey == key);
            var children = GetByQuery(query);

            var filterGroup = new EntityFilterGroup(collection);
            foreach (var child in children)
            {
                filterGroup.Filters.Add(child);
            }

            return filterGroup;
        }

        /// <inheritdoc/>
        protected override EntityCollectionFactory GetFactoryInstance()
        {
            return new EntityCollectionFactory();
        }
    }
}