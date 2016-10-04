namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class CustomerRepository : IEntityCollectionEntityRepository<ICustomer>
    {
        /// <inheritdoc/>
        public void AddToCollection(Guid entityKey, Guid collectionKey)
        {
            if (this.ExistsInCollection(entityKey, collectionKey)) return;

            var dto = new Customer2EntityCollectionDto()
            {
                CustomerKey = entityKey,
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
                "DELETE [merchCustomer2EntityCollection] WHERE [merchCustomer2EntityCollection].[customerKey] = @ekey AND [merchCustomer2EntityCollection].[entityCollectionKey] = @eckey",
                new { @ekey = entityKey, @eckey = collectionKey });
        }

        /// <inheritdoc/>
        public bool ExistsInCollection(Guid entityKey, Guid collectionKey)
        {
            var sql = Sql().SelectCount()
                        .From<Customer2EntityCollectionDto>()
                        .Where<Customer2EntityCollectionDto>(
                            x => x.EntityCollectionKey == collectionKey && x.CustomerKey == entityKey);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public bool ExistsInAtLeastOneCollection(Guid entityKey, Guid[] collectionKeys)
        {
            var sql = Sql().SelectCount()
                        .From<Customer2EntityCollectionDto>()
                        .Where<Customer2EntityCollectionDto>(x => x.CustomerKey == entityKey)
                        .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .Where<Customer2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = Sql().SelectAll().From<CustomerDto>()
                .SingleWhereIn<CustomerDto>(x => x.Key, innerSql)
                .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .Where<Customer2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .AndIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys)
                            .GroupBy<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .HavingCount(collectionKeys);

            var sql = Sql().SelectAll()
                        .From<CustomerDto>()
                        .SingleWhereIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys)
                            .GroupBy<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .HavingCount(collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .AndIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = Sql().SelectAll()
                        .From<CustomerDto>()
                        .SingleWhereIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .AndIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .Where<Customer2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = Sql().SelectAll()
                        .From<CustomerDto>()
                        .SingleWhereNotIn<CustomerDto>( x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesNotInCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .Where<Customer2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .AndNotIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)   
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = Sql().SelectAll()
                        .From<CustomerDto>()
                        .SingleWhereNotIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<ICustomer> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Customer2EntityCollectionDto>(x => x.CustomerKey)
                            .From<Customer2EntityCollectionDto>()
                            .WhereIn<Customer2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .AndNotIn<CustomerDto>(x => x.Key, innerSql)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
