namespace Merchello.Core.Cache
{
    using System;

    using Merchello.Core.Acquired.Cache;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents an EntityCollectionRepositoryCachePolicy.
    /// </summary>
    internal class EntityCollectionRepositoryCachePolicy : DefaultRepositoryCachePolicy<IEntityCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionRepositoryCachePolicy"/> class.
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <remarks>
        /// We need to handle the special filter collection caching here as well.
        /// </remarks>
        public EntityCollectionRepositoryCachePolicy(IRuntimeCacheProviderAdapter cache, RepositoryCachePolicyOptions options)
            : base(cache, options)
        {
        }

        /// <summary>
        /// Gets an IEntityFilterGroup from the cache.
        /// </summary>
        /// <param name="key">
        /// The GUID identifier.
        /// </param>
        /// <param name="performGet">
        /// A function to get the <see cref="IEntityFilterGroup"/>.
        /// </param>
        /// <returns>
        /// The entity with the specified identifier, if it is in the cache already, else null.
        /// </returns>
        /// <remarks>
        /// Does not consider the repository at all.
        /// </remarks>
        public IEntityFilterGroup Get(Guid key, Func<Guid, IEntityFilterGroup> performGet)
        {
            var cacheKey = GetFilterGroupCacheKey(key);
            var fromCache = Cache.GetCacheItem<IEntityFilterGroup>(cacheKey);

            // if found in cache then return else fetch and cache
            if (fromCache != null)
                return fromCache;
            var entity = performGet(key);

            if (entity != null && entity.HasIdentity)
                InsertEntity(cacheKey, entity);

            return entity;
        }

        /// <inheritdoc/>
        public override void ClearAll()
        {
            base.ClearAll();
            this.Cache.ClearCacheByKeySearch(this.GetFilterGroupCacheKey());
        }

        /// <inheritdoc/>
        public override void Update(IEntityCollection entity, Action<IEntityCollection> persistUpdated)
        {
            if (entity.IsFilter)
            this.Cache.ClearCacheItem(
                entity.ParentKey != null
                    ? this.GetFilterGroupCacheKey(entity.ParentKey.Value)
                    : this.GetFilterGroupCacheKey(entity.Key));

            base.Update(entity, persistUpdated);
        }

        /// <inheritdoc/>
        public override void Delete(IEntityCollection entity, Action<IEntityCollection> persistDeleted)
        {
            if (entity.IsFilter)
            this.Cache.ClearCacheItem(
                entity.ParentKey != null
                    ? this.GetFilterGroupCacheKey(entity.ParentKey.Value)
                    : this.GetFilterGroupCacheKey(entity.Key));

            base.Delete(entity, persistDeleted);
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The cache key.
        /// </returns>
        protected string GetFilterGroupCacheKey(Guid key)
        {
            if (key == null || key.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(key));
            return this.GetEntityTypeCacheKey() + key;
        }


        /// <summary>
        /// Gets the entity type cache key.
        /// </summary>
        /// <returns>
        /// The cache key.
        /// </returns>
        protected string GetFilterGroupCacheKey()
        {
            return $"mRepo_{typeof(IEntityFilterGroup).Name}_";
        }
    }
}