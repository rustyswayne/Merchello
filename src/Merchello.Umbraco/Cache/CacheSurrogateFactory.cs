namespace Merchello.Umbraco.Cache
{
    using Merchello.Core.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Cache;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a factory responsible for building cache objects that will be deep cloned.
    /// </summary>
    /// <remarks>
    /// This factory is used to override the default factory so that the IDeepCloneable interface can be adapted.
    /// </remarks>
    public class CacheSurrogateFactory : ICloneableCacheEntityFactory
    {
        /// <inheritdoc/>
        public ICloneableCacheEntity<TEntity> BuildCacheEntity<TEntity>(TEntity entity) where TEntity : class, IEntity, IDeepCloneable
        {
            return new CacheSurrogate<TEntity>(entity);
        }
    }
}