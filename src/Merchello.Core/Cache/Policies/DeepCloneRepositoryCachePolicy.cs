namespace Merchello.Core.Cache.Policies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Cache;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents the deep clone cache policy.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity which is wrapped so that Umbraco's cache provider handles the cloning/caching</typeparam>
    /// <remarks>
    /// <para>The default cache policy caches entities with a 5 minutes sliding expiration.</para>
    /// <para>Each entity is cached individually.</para>
    /// <para>If options.GetAllCacheAllowZeroCount then a 'zero-count' array is cached when GetAll finds nothing.</para>
    /// <para>If options.GetAllCacheValidateCount then we check against the db when getting many entities.</para>
    /// </remarks>
    internal class DeepcloneRepositoryCachePolicy<TEntity> : RepositoryCachePolicyBase<TEntity>
        where TEntity : class, IEntity, IDeepCloneable
    {
        /// <summary>
        /// The empty entities.
        /// </summary>
        private static readonly ICloneableCacheEntity<TEntity>[] EmptyEntities = new ICloneableCacheEntity<TEntity>[0];

        /// <summary>
        /// The repository caching options.
        /// </summary>
        private readonly RepositoryCachePolicyOptions _options;

        /// <summary>
        /// The <see cref="ICloneableCacheEntityFactory"/>.
        /// </summary>
        private ICloneableCacheEntityFactory _cacheModelFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeepcloneRepositoryCachePolicy{TEntity}"/> class. 
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="cacheModelFactory">
        /// The <see cref="ICloneableCacheEntityFactory"/>
        /// </param>
        public DeepcloneRepositoryCachePolicy(IRuntimeCacheProviderAdapter cache, RepositoryCachePolicyOptions options, ICloneableCacheEntityFactory cacheModelFactory)
            : base(cache)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            this._options = options;

            Ensure.ParameterNotNull(cacheModelFactory, nameof(cacheModelFactory));
            _cacheModelFactory = cacheModelFactory;
        }

        /// <inheritdoc/>
        /// <returns></returns>
        public override TEntity Get(Guid key, Func<Guid, TEntity> performGet, Func<Guid[], IEnumerable<TEntity>> performGetAll)
        {
            var cacheKey = this.GetEntityCacheKey(key);
            var fromCache = (ICloneableCacheEntity<TEntity>)this.Cache.GetCacheItem(cacheKey);

            // if found in cache then return else fetch and cache
            if (fromCache != null)
                return fromCache.Model;

            var entity = performGet(key);

            if (entity != null && entity.HasIdentity)
                this.InsertEntity(cacheKey, entity);

            return entity;
        }

        /// <inheritdoc/>
        public override TEntity GetCached(Guid key)
        {
            var cacheKey = this.GetEntityCacheKey(key);
            var fromCache = (ICloneableCacheEntity<TEntity>)this.Cache.GetCacheItem(cacheKey);
            return fromCache?.Model;
        }


        /// <inheritdoc/>
        public override bool Exists(Guid key, Func<Guid, bool> performExists, Func<Guid[], IEnumerable<TEntity>> performGetAll)
        {
            // if found in cache the return else check
            var cacheKey = this.GetEntityCacheKey(key);
            var fromCache = (ICloneableCacheEntity<TEntity>)this.Cache.GetCacheItem(cacheKey);
            return fromCache != null || performExists(key);
        }

        /// <inheritdoc/>
        public override void Create(TEntity entity, Action<TEntity> persistNew)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                persistNew(entity);

                // just to be safe, we cannot cache an item without an identity
                if (entity.HasIdentity)
                {
                    this.Cache.InsertCacheItem(this.GetEntityCacheKey(entity.Key), () => _cacheModelFactory.BuildCacheEntity(entity), TimeSpan.FromMinutes(5), true);
                }

                // if there's a GetAllCacheAllowZeroCount cache, ensure it is cleared
                this.Cache.ClearCacheItem(this.GetEntityTypeCacheKey());
            }
            catch
            {
                // if an exception is thrown we need to remove the entry from cache
                this.Cache.ClearCacheItem(this.GetEntityCacheKey(entity.Key));

                // if there's a GetAllCacheAllowZeroCount cache, ensure it is cleared
                this.Cache.ClearCacheItem(this.GetEntityTypeCacheKey());

                throw;
            }
        }

        /// <inheritdoc/>
        public override void Update(TEntity entity, Action<TEntity> persistUpdated)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                persistUpdated(entity);

                // just to be safe, we cannot cache an item without an identity
                if (entity.HasIdentity)
                {
                    this.Cache.InsertCacheItem(this.GetEntityCacheKey(entity.Key), () => _cacheModelFactory.BuildCacheEntity(entity), TimeSpan.FromMinutes(5), true);
                }

                // if there's a GetAllCacheAllowZeroCount cache, ensure it is cleared
                this.Cache.ClearCacheItem(this.GetEntityTypeCacheKey());
            }
            catch
            {
                // if an exception is thrown we need to remove the entry from cache
                this.Cache.ClearCacheItem(this.GetEntityCacheKey(entity.Key));

                // if there's a GetAllCacheAllowZeroCount cache, ensure it is cleared
                this.Cache.ClearCacheItem(this.GetEntityTypeCacheKey());

                throw;
            }
        }

        /// <inheritdoc/>
        public override void Delete(TEntity entity, Action<TEntity> persistDeleted)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                persistDeleted(entity);
            }
            finally
            {
                // whatever happens, clear the cache
                var cacheKey = this.GetEntityCacheKey(entity.Key);
                this.Cache.ClearCacheItem(cacheKey);

                // if there's a GetAllCacheAllowZeroCount cache, ensure it is cleared
                this.Cache.ClearCacheItem(this.GetEntityTypeCacheKey());
            }
        }

        /// <inheritdoc/>
        public override TEntity[] GetAll(Guid[] keys, Func<Guid[], IEnumerable<TEntity>> performGetAll)
        {
            if (keys.Length > 0)
            {
                // try to get each entity from the cache
                // if we can find all of them, return
                var entities = keys.Select(this.GetCached).WhereNotNull().ToArray();
                if (keys.Length.Equals(entities.Length))
                    return entities; // no need for null checks, we are not caching nulls
            }
            else
            {
                // get everything we have
                var entities = this.Cache.GetCacheItemsByKeySearch<ICloneableCacheEntity<TEntity>>(this.GetEntityTypeCacheKey())
                    .ToArray(); // no need for null checks, we are not caching nulls

                if (entities.Length > 0)
                {
                    // if some of them were in the cache...
                    if (this._options.GetAllCacheValidateCount)
                    {
                        // need to validate the count, get the actual count and return if ok
                        var totalCount = this._options.PerformCount();
                        if (entities.Length == totalCount)
                            return entities.Select(x => x.Model).ToArray();
                    }
                    else
                    {
                        // no need to validate, just return what we have and assume it's all there is
                        return entities.Select(x => x.Model).ToArray();
                    }
                }
                else if (this._options.GetAllCacheAllowZeroCount)
                {
                    // if none of them were in the cache
                    // and we allow zero count - check for the special (empty) entry
                    var empty = this.Cache.GetCacheItem<ICloneableCacheEntity<TEntity>[]>(this.GetEntityTypeCacheKey());
                    if (empty != null) return empty.Select(x => x.Model).ToArray();
                }
            }

            // cache failed, get from repo and cache
            var repoEntities = performGetAll(keys)
                .WhereNotNull() // exclude nulls!
                .Where(x => x.HasIdentity) // be safe, though would be weird...
                .ToArray();

            // note: if empty & allow zero count, will cache a special (empty) entry
            this.InsertEntities(keys, repoEntities);

            return repoEntities;
        }

        /// <inheritdoc/>
        public override void ClearAll()
        {
            this.Cache.ClearCacheByKeySearch(this.GetEntityTypeCacheKey());
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
        protected string GetEntityCacheKey(Guid key)
        {
            return this.GetEntityCacheKey<TEntity>(key);
        }

        /// <summary>
        /// Gets the entity type cache key.
        /// </summary>
        /// <returns>
        /// The cache key.
        /// </returns>
        protected string GetEntityTypeCacheKey()
        {
            return this.GetEntityTypeCacheKey<TEntity>();
        }

        /// <inheritdoc/>
        protected void InsertEntity(string cacheKey, TEntity entity)
        {
            this.Cache.InsertCacheItem(cacheKey, () => _cacheModelFactory.BuildCacheEntity<TEntity>(entity), TimeSpan.FromMinutes(5), true);
        }

        /// <inheritdoc/>
        protected void InsertEntities(Guid[] keys, TEntity[] entities)
        {
            if (keys.Length == 0 && entities.Length == 0 && this._options.GetAllCacheAllowZeroCount)
            {
                // Getting all of them, and finding nothing.
                // if we can cache a zero count, cache an empty array,
                // for as long as the cache is not cleared (no expiration)
                this.Cache.InsertCacheItem(this.GetEntityTypeCacheKey(), () => EmptyEntities);
            }
            else
            {
                // individually cache each item
                foreach (var entity in entities)
                {
                    var capture = entity;
                    this.Cache.InsertCacheItem(this.GetEntityCacheKey(entity.Key), () => _cacheModelFactory.BuildCacheEntity(capture), TimeSpan.FromMinutes(5), true);
                }
            }
        }
    }
}