namespace Merchello.Core.Cache.Policies
{
    using System;

    using Merchello.Core.Acquired.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <summary>
    /// Represents a cache policy for the <see cref="EntityCollectionRepository"/>.
    /// </summary>
    internal class EntityCollectionRepositoryCachePolicy : DefaultRepositoryCachePolicy<IEntityCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionRepositoryCachePolicy"/> class.
        /// </summary>
        /// <param name="cache">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        /// <param name="options">
        /// The <see cref="RepositoryCachePolicyOptions"/>.
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
            var cacheKey = this.GetEntityCacheKey<IEntityFilterGroup>(key);
            var fromCache = this.Cache.GetCacheItem<IEntityFilterGroup>(cacheKey);

            // if found in cache then return else fetch and cache
            if (fromCache != null)
                return fromCache;
            var entity = performGet(key);

            if (entity != null && entity.HasIdentity)
                this.InsertEntity(cacheKey, entity);

            return entity;
        }

        /// <inheritdoc/>
        public override void ClearAll()
        {
            base.ClearAll();
            this.Cache.ClearCacheByKeySearch(this.GetEntityTypeCacheKey<IEntityFilterGroup>());
        }

        /// <inheritdoc/>
        public override void Update(IEntityCollection entity, Action<IEntityCollection> persistUpdated)
        {
            if (entity.IsFilter)
            this.Cache.ClearCacheItem(
                entity.ParentKey != null
                    ? this.GetEntityCacheKey<IEntityFilterGroup>(entity.ParentKey.Value)
                    : this.GetEntityCacheKey<IEntityFilterGroup>(entity.Key));

            base.Update(entity, persistUpdated);
        }

        /// <inheritdoc/>
        public override void Delete(IEntityCollection entity, Action<IEntityCollection> persistDeleted)
        {
            if (entity.IsFilter)
            this.Cache.ClearCacheItem(
                entity.ParentKey != null
                    ? this.GetEntityCacheKey<IEntityFilterGroup>(entity.ParentKey.Value)
                    : this.GetEntityCacheKey<IEntityFilterGroup>(entity.Key));

            base.Delete(entity, persistDeleted);
        }
    }
}