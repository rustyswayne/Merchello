namespace Merchello.Core.Models.Cache
{
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// The default cloneable cache entry.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    /// <remarks>
    /// Umbraco will not actually clone this as the entity will not implement Umbraco's IDeepCloneable interface.
    /// This class is only used as a default in the core, which is later overridden in Merchello.Umbraco to use an adapted class
    /// which Umbraco will pickup as cloneable.
    /// </remarks>
    internal class DefaultCloneableCacheEntry<TEntity> : CloneableCacheEntityBase<TEntity>
        where TEntity : class, IEntity, IDeepCloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCloneableCacheEntry{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public DefaultCloneableCacheEntry(TEntity entity)
            : base(entity)
        {
        }
    }
}