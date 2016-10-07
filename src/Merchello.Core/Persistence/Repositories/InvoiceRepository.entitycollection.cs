namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IEntityCollectionEntityRepository<IInvoice>
    {
        /// <inheritdoc/>
        public void AddToCollection(Guid entityKey, Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void RemoveFromCollection(Guid entityKey, Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool ExistsInCollection(Guid entityKey, Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool ExistsInAtLeastOneCollection(Guid entityKey, Guid[] collectionKeys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
