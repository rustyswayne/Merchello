namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Services;

    /// <summary>
    /// The EntityCollectionProvider interface.
    /// </summary>
    /// <typeparam name="TSerivce">
    /// The type of the wrapped service
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of entity
    /// </typeparam>
    public interface IEntityCollectionProvider<out TSerivce, TEntity>
        where TSerivce : IEntityCollectionEntityService<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the wrapped query service.
        /// </summary>
        TSerivce Service { get; }

        /// <summary>
        /// Returns true if the entity exists in the collection.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(TEntity entity);

        /// <summary>
        /// The get entities.
        /// </summary>
        /// <returns>
        /// The collection of entities.
        /// </returns>
        IEnumerable<TEntity> GetEntitiesFromCollection();

        /// <summary>
        /// Returns a value indicating whether or not the entity exists in a collection.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// A value indicating if the entity exists in the collection.
        /// </returns>
        bool ExistsInCollection(Guid entityKey);


        /// <summary>
        /// Gets the entities associated with the entity collection.
        /// </summary>
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
        PagedCollection<TEntity> GetEntitiesFromCollection(long page, long itemsPerPage, string orderExpression = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Gets the entities associated with the entity collection.
        /// </summary>
        /// <param name="searchTerm">
        /// The search Term.
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
        PagedCollection<TEntity> GetEntitiesFromCollection(string searchTerm, long page, long itemsPerPage, string orderExpression = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Gets entities that does not exist in a collection.
        /// </summary>
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
        PagedCollection<TEntity> GetEntitiesNotInCollection(long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);
    }
}