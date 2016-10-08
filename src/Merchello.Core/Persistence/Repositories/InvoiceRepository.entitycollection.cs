namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IEntityCollectionEntityRepository<IInvoice>
    {
        /// <inheritdoc/>
        public void AddToCollection(Guid entityKey, Guid collectionKey)
        {
            if (this.ExistsInCollection(entityKey, collectionKey)) return;

            var dto = new Invoice2EntityCollectionDto()
            {
                InvoiceKey = entityKey,
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
                "DELETE [merchInvoice2EntityCollection] WHERE [merchInvoice2EntityCollection].[invoiceKey] = @ikey AND [merchInvoice2EntityCollection].[entityCollectionKey] = @eckey",
                new { @ikey = entityKey, @eckey = collectionKey });
        }

        /// <inheritdoc/>
        public bool ExistsInCollection(Guid entityKey, Guid collectionKey)
        {
            var sql =
                Sql()
                .SelectCount()
                .From<Invoice2EntityCollectionDto>()
                .Where<Invoice2EntityCollectionDto>(x => x.InvoiceKey == entityKey && x.EntityCollectionKey == collectionKey);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public bool ExistsInAtLeastOneCollection(Guid entityKey, Guid[] collectionKeys)
        {
            var sql = Sql()
                    .SelectCount()
                    .From<Invoice2EntityCollectionDto>()
                    .Where<Invoice2EntityCollectionDto>(x => x.InvoiceKey == entityKey)
                    .WhereIn<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql()
                .SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                .From<Invoice2EntityCollectionDto>()
                .Where<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql()
                .SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                .From<Invoice2EntityCollectionDto>()
                .Where<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .WhereIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, collectionKeys)
                            .GroupBy<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .HavingCount(collectionKeys);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .WhereIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, collectionKeys)
                            .GroupBy<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .HavingCount(collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                                .From<Invoice2EntityCollectionDto>()
                                .WhereIn<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                                .From<Invoice2EntityCollectionDto>()
                                .WhereIn<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .Where<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereNotIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .Where<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey == collectionKey);

            var sql = BuildSearchSql(searchTerm)
                        .From<InvoiceDto>()
                        .SingleWhereNotIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .WhereIn<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereNotIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<Invoice2EntityCollectionDto>(x => x.InvoiceKey)
                            .From<Invoice2EntityCollectionDto>()
                            .WhereIn<Invoice2EntityCollectionDto>(x => x.EntityCollectionKey, collectionKeys);

            var sql = BuildSearchSql(searchTerm)
                        .From<InvoiceDto>()
                        .SingleWhereNotIn<Invoice2EntityCollectionDto>(x => x.InvoiceKey, innerSql)
                        .AppendOrderExpression<InvoiceDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
