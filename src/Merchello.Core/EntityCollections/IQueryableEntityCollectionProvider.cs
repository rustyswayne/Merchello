namespace Merchello.Core.EntityCollections
{
    using System.Collections.Generic;

    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents an queryable entity collection provider.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of the wrapped service.
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    public interface IQueryableEntityCollectionProvider<out TService, TEntity> : IEntityCollectionProvider<TService, TEntity>
        where TService : class, IEntityCollectionEntityService<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// The get entities.
        /// </summary>
        /// <param name="args">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The collection of entities.
        /// </returns>
        IEnumerable<TEntity> GetEntities(Dictionary<string, object> args);
    }
}