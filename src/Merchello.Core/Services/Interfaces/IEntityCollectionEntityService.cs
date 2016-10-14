namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a service that manages entity collection operations for <see cref="IEntityCollection"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of entity
    /// </typeparam>
    public interface IEntityCollectionEntityService<T>
        where T : IEntity
    {
        /// <summary>
        /// Adds a entity to a collection.
        /// </summary>
        /// <param name="entity">
        /// The entity to be added.
        /// </param>
        /// <param name="collection">
        /// The collection to which the entity is to be added.
        /// </param>
        void AddToCollection(T entity, IEntityCollection collection);

        /// <summary>
        /// Adds a entity to a collection.
        /// </summary>
        /// <param name="entity">
        /// The entity of the entity to be added.
        /// </param>
        /// <param name="collectionKey">
        /// The collection to which the entity is to be added.
        /// </param>
        void AddToCollection(T entity, Guid collectionKey);

        /// <summary>
        /// Adds a entity to a collection.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key of the entity to be added.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key of the collection to be added.
        /// </param>
        void AddToCollection(Guid entityKey, Guid collectionKey);

        /// <summary>
        /// Removes an entity from a collection.
        /// </summary>
        /// <param name="entity">
        /// The entity to be removed.
        /// </param>
        /// <param name="collection">
        /// The collection which the entity should be removed.
        /// </param>
        void RemoveFromCollection(T entity, IEntityCollection collection);

        /// <summary>
        /// Removes an entity from a collection.
        /// </summary>
        /// <param name="entity">
        /// The entity entity to be removed.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key of the collection entity should be removed.
        /// </param>
        void RemoveFromCollection(T entity, Guid collectionKey);

        /// <summary>
        /// Removes an entity from a collection.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key entity to be removed.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key of the collection entity should be removed.
        /// </param>
        void RemoveFromCollection(Guid entityKey, Guid collectionKey);

        /// <summary>
        /// Returns a value indicating whether or not the entity exists in a collection.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key to be checked.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ExistsInCollection(Guid entityKey, Guid collectionKey);

        /// <summary>
        /// Returns a value indicating whether or not the entity exists in at least one of the collections.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ExistsInAtLeastOneCollection(Guid entityKey, IEnumerable<Guid> collectionKeys);

        /// <summary>
        /// Gets the entities associated with the entity collection.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key to be checked.
        /// </param>
        /// <param name="page">
        /// The current page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The number of items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets the entities associated with the entity collection.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key to be checked.
        /// </param>
        /// <param name="searchTerm">
        /// A search term
        /// </param>
        /// <param name="page">
        /// The current page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The number of items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that exist in ALL collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesThatExistInAllCollections(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that exist in ALL collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesThatExistInAllCollections(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that exist in ANY of the collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesThatExistInAnyCollection(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that exist in ANY collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesThatExistInAnyCollection(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that does not exist in a collection.
        /// </summary>
        /// <param name="collectionKey">
        /// The collection key to be checked.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that does not exist in ANY of the collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesNotInAnyCollection(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that does not exist in ANY of the collections.
        /// </summary>
        /// <param name="collectionKeys">
        /// The collection keys to be searched.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<T> GetEntitiesNotInAnyCollection(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);
    }
}