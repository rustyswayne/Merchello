namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a service.
    /// </summary>
    public interface IService
    { 
    }

    /// <summary>
    /// Represents an entity service.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity
    /// </typeparam>
    public interface IService<TEntity> : IService
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the entity by it's unique key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity GetByKey(Guid key);

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Save(TEntity entity);

        /// <summary>
        /// Saves a collection of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities to be saved.
        /// </param>
        void Save(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">
        /// The entity to be deleted.
        /// </param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes a collection of entities.
        /// </summary>
        /// <param name="entities">
        /// The entities to be deleted.
        /// </param>
        void Delete(IEnumerable<TEntity> entities);
    }
}