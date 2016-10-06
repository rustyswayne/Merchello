namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : NPocoRepositoryBase<IProductVariant>
    {
        /// <inheritdoc/>
        protected override IProductVariant PerformGet(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProductVariant> PerformGetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProductVariant> PerformGetByQuery(IQuery<IProductVariant> query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IProductVariant entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IProductVariant entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            var sql = Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ProductDto>()
                .InnerJoin<ProductVariantDto>()
                .On<ProductDto, ProductVariantDto>(left => left.Key, right => right.ProductKey)
                .InnerJoin<ProductVariantIndexDto>()
                .On<ProductVariantDto, ProductVariantIndexDto>(left => left.Key, right => right.ProductVariantKey);

            return sql;
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchProductVariant.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchCatalogInventory WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariantDetachedContent WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariantIndex WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariant WHERE pk = @Key"
            };

            return list;
        }
    }
}
