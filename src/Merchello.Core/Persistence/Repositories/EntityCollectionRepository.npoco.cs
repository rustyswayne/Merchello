namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Cache.Policies;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class EntityCollectionRepository : NPocoEntityRepositoryBase<IEntityCollection, EntityCollectionDto, EntityCollectionFactory>
    {
        /// <summary>
        /// The caching policy.
        /// </summary>
        private IRepositoryCachePolicy<IEntityCollection> _cachePolicy;

        /// <inheritdoc/>
        protected override IRepositoryCachePolicy<IEntityCollection> CachePolicy
        {
            get
            {
                var options = new RepositoryCachePolicyOptions(() =>
                {
                    // Get count of all entities of current type (TEntity) to ensure cached result is correct
                    var query = Query.Where(x => x.Key != null && x.Key != Guid.Empty);
                    return PerformCount(query);
                });

                _cachePolicy = new EntityCollectionRepositoryCachePolicy(RuntimeCache, options);

                return _cachePolicy;
            }
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<EntityCollectionDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchEntityCollection.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchProduct2EntityCollection WHERE merchProduct2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchInvoice2EntityCollection WHERE merchInvoice2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchCustomer2EntityCollection WHERE merchCustomer2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchEntityCollection WHERE merchEntityCollection.pk = @Key"
                };

            return list;
        }
    }
}
