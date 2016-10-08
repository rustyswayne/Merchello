namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductRepository : IProductRepository
    {
        /// <inheritdoc/>
        public int CountKeysThatExistInAllCollections(Guid[] collectionKeys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Tuple<IEnumerable<Guid>, int>> CountKeysThatExistInAllCollections(IEnumerable<Guid[]> collectionKeysGroups)
        {
            throw new NotImplementedException();
        }

        private Sql<SqlContext> SqlForKeysThatExistInAllCollections(Guid[] collectionKeys, bool isCount = false)
        {
            var innerSql = Sql().SelectField<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys)
                            .GroupBy<Product2EntityCollectionDto>(x => x.ProductKey)
                            .HavingCount(collectionKeys);

            var sql = Sql().Select(isCount ? "COUNT(*) AS Count" : "*")
                .From<ProductVariantDto>()
                .Where<ProductVariantDto>(x => x.Master)
                .AndIn<ProductVariantDto>(x => x.ProductKey, innerSql);

            return sql;
        }
    }
}
