namespace Merchello.Core.Cache
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Cache;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a factory that builds cloneable cache entries.
    /// </summary>
    public interface ICloneableCacheEntityFactory
    {
        /// <summary>
        /// Builds a cloneable cache entry.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <typeparam name="TEntity">
        /// The type of the entity
        /// </typeparam>
        /// <returns>
        /// The <see cref="ICloneableCacheEntity{TEntity}"/>.
        /// </returns>
        ICloneableCacheEntity<TEntity> BuildCacheEntity<TEntity>(TEntity entity) where TEntity : class, IEntity, IDeepCloneable;
    }
}