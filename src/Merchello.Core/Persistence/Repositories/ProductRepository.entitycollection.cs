namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class ProductRepository : IEntityCollectionEntityRepository<IProduct>
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
        public PagedCollection<IProduct> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetEntitiesThatExistInAllCollections(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesThatExistInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesNotInCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IProduct> GetEntitiesNotInAnyCollection(Guid[] collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public int CountKeysThatExistInAllCollections(Guid[] collectionKeys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tuple<IEnumerable<Guid>, int>> CountKeysThatExistInAllCollections(IEnumerable<Guid[]> collectionKeysGroups)
        {
            throw new NotImplementedException();
        }
    }
}
