namespace Merchello.Core.Cache
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Cache;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a factory responsible for building cache objects that will be deep cloned.
    /// </summary>
    /// <remarks>
    /// This factory exists mainly for testing.  It added to the service container in the Core bootstrap process and then replaced 
    /// in the Merchello.Umbraco bootstrap process.
    /// </remarks>
    internal class DefaultCloneableCacheEntityFactory : ICloneableCacheEntityFactory
    {
        /// <inheritdoc/>
        public ICloneableCacheEntity<TEntity> BuildCacheEntity<TEntity>(TEntity entity) where TEntity : class, IEntity, IDeepCloneable
        {
            return new DefaultCloneableCacheEntry<TEntity>(entity);
        }
    }
}