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
    internal partial class ProductOptionRepository : NPocoEntityRepositoryBase<IProductOption, ProductOptionDto, ProductOptionFactory>
    {
        /// <summary>
        /// The caching policy.
        /// </summary>
        private IRepositoryCachePolicy<IProductOption> _cachePolicy;

        /// <inheritdoc/>
        protected override IRepositoryCachePolicy<IProductOption> CachePolicy
        {
            get
            {
                var options = new RepositoryCachePolicyOptions(() =>
                {
                    // Get count of all entities of current type (TEntity) to ensure cached result is correct
                    var query = Query.Where(x => x.Key != null && x.Key != Guid.Empty);
                    return PerformCount(query);
                });

                _cachePolicy = new ProductOptionRepositoryCachePolicy(RuntimeCache, options);

                return _cachePolicy;
            }
        }


        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ProductOptionDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchProductOption.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchProductVariant2ProductAttribute WHERE productVariantKey IN (SELECT productVariantKey FROM merchProductVariant2ProductAttribute WHERE optionKey = @Key)",
                    "DELETE FROM merchProductOptionAttributeShare WHERE optionKey = @Key",
                    "DELETE FROM merchProduct2ProductOption WHERE optionKey = @Key",
                    "DELETE FROM merchProductAttribute WHERE optionKey = @Key",
                    "DELETE FROM merchProductOption WHERE pk = @Key"
                };

            return list;
        }
    }
}
