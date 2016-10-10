namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductRepository : IProductRepository
    {
        /// <inheritdoc/>
        public int CountKeysThatExistInAllCollections(Guid[] collectionKeys)
        {
            var innerSql = Sql().SelectField<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys)
                            .GroupBy<Product2EntityCollectionDto>(x => x.ProductKey)
                            .HavingCount(collectionKeys);

            var sql = Sql().SelectCount()
                .From<ProductVariantDto>()
                .Where<ProductVariantDto>(x => x.Master)
                .AndIn<ProductVariantDto>(x => x.ProductKey, innerSql);

            return Database.ExecuteScalar<int>(sql);
        }

        /// <inheritdoc/>
        public IEnumerable<Tuple<IEnumerable<Guid>, int>> CountKeysThatExistInAllCollections(IEnumerable<Guid[]> collectionKeysGroups)
        {
            var keysGroups = collectionKeysGroups as Guid[][] ?? collectionKeysGroups.ToArray();

            var quotedQ = SqlSyntax.GetQuotedName("CountQ");
            var quotedC = SqlSyntax.GetQuotedName("Count");
            var quoted = $"{quotedQ}.{quotedC}";

            var sql = Sql();
            foreach (var group in keysGroups)
            {
                var innerSql = Sql().SelectField<Product2EntityCollectionDto>(x => x.ProductKey)
                                .From<Product2EntityCollectionDto>()
                                .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, group)
                                .GroupBy<Product2EntityCollectionDto>(x => x.ProductKey)
                                .HavingCount(keysGroups);

                var countQ = Sql().SelectCount().Append($"AS {quotedC}")
                                .From<ProductVariantDto>()
                                .Where<ProductVariantDto>(x => x.Master)
                                .SingleWhereIn<ProductVariantDto>(x => x.ProductKey, innerSql);

                // Append UNION to group multiple queries
                if (sql.SQL.Length > 0) sql.Append("UNION");

                sql.Select($"{group.GetHashCode()} AS Hash, {quoted}").FromQuery(countQ, "CountQ");
            }

            var dtos = Database.Fetch<FilterCountingDto>(sql);

            var results = new List<Tuple<IEnumerable<Guid>, int>>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var group in keysGroups)
            {
                var hash = group.GetHashCode();
                var dto = dtos.FirstOrDefault(x => x.Hash == hash);
                if (dto != null) results.Add(new Tuple<IEnumerable<Guid>, int>(group, dto.Count));
            }

            return results;
        }
    }
}
