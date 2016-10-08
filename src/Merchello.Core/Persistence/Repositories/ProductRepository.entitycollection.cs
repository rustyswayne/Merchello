namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductRepository : IEntityCollectionEntityRepository<IProduct>
    {
        /// <inheritdoc/>
        public void AddToCollection(Guid entityKey, Guid collectionKey)
        {
            if (this.ExistsInCollection(entityKey, collectionKey)) return;

            var dto = new Product2EntityCollectionDto()
            {
                ProductKey = entityKey,
                EntityCollectionKey = collectionKey,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            Database.Insert(dto);
        }

        /// <inheritdoc/>
        public void RemoveFromCollection(Guid entityKey, Guid collectionKey)
        {
            Database.Execute(
                "DELETE [merchProduct2EntityCollection] WHERE [merchProduct2EntityCollection].[productKey] = @pkey AND [merchProduct2EntityCollection].[entityCollectionKey] = @eckey",
                new { @pkey = entityKey, @eckey = collectionKey });
        }

        /// <inheritdoc/>
        public bool ExistsInCollection(Guid entityKey, Guid collectionKey)
        {
            var sql =
                Sql()
                .SelectCount()
                .From<Product2EntityCollectionDto>()
                .Where<Product2EntityCollectionDto>(x => x.ProductKey == entityKey && x.EntityCollectionKey == collectionKey);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public bool ExistsInAtLeastOneCollection(Guid entityKey, Guid[] collectionKeys)
        {
            var sql = Sql()
                    .SelectCount()
                    .From<Product2EntityCollectionDto>()
                    .Where<Product2EntityCollectionDto>(x => x.ProductKey == entityKey)
                    .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql()
                .SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                .From<Product2EntityCollectionDto>()
                .Where<Product2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql()
                .SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                .From<Product2EntityCollectionDto>()
                .Where<Product2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.ProductKey, collectionKeys)
                            .GroupBy<Product2EntityCollectionDto>(x => x.ProductKey)
                            .HavingCount(collectionKeys);

            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.ProductKey, collectionKeys)
                            .GroupBy<Product2EntityCollectionDto>(x => x.ProductKey)
                            .HavingCount(collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                                .From<Product2EntityCollectionDto>()
                                .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                                .From<Product2EntityCollectionDto>()
                                .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .Where<Product2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = GetBaseQuery(false)
                        .SingleWhereNotIn<Product2EntityCollectionDto>(x => x.ProductKey, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesNotInCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .Where<Product2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereNotIn<Product2EntityCollectionDto>(x => x.ProductKey, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = GetBaseQuery(false)
                        .SingleWhereNotIn<Product2EntityCollectionDto>(x => x.ProductKey, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.ProductKey)
                            .From<Product2EntityCollectionDto>()
                            .WhereIn<Product2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereNotIn<Product2EntityCollectionDto>(x => x.ProductKey, innerSql)
                        .AppendOrderExpression<ProductDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
