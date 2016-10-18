namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Services;


    /// <summary>
    /// The entity collection provider base.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of the service
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of entity
    /// </typeparam>
    public abstract class EntityCollectionProviderBase<TService, TEntity> : EntityCollectionProviderBase, IEntityCollectionProvider<TService, TEntity>
        where TService : class, IEntityCollectionEntityService<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionProviderBase{TService,TEntity}"/> class. 
        /// </summary>
        /// <param name="wrappedService">
        /// The wrapped Service.
        /// </param>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>.
        /// </param>
        /// <param name="cacheHelper">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        protected EntityCollectionProviderBase(TService wrappedService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(entityCollectionService, cacheHelper, collectionKey)
        {
            Ensure.ParameterNotNull(wrappedService, nameof(wrappedService));
            Service = wrappedService;
        }

        /// <inheritdoc/>
        public TService Service { get; }

        /// <summary>
        /// Returns true if the entity exists in the collection.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(TEntity entity)
        {
            var cacheKey = GetExistsCacheKey(entity);
            var exists = Cache.GetCacheItem(cacheKey);

            if (exists != null) return (bool)exists;


            return (bool)Cache.GetCacheItem(cacheKey, () => this.PerformExists(entity));
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetEntitiesFromCollection()
        {
            return GetEntitiesFromCollection(1, long.MaxValue, string.Empty).Items;
        }


        /// <inheritdoc/>
        public virtual bool ExistsInCollection(Guid entityKey)
        {
            return Service.ExistsInCollection(entityKey, this.CollectionKey);
        }

        /// <inheritdoc/>
        public virtual PagedCollection<TEntity> GetEntitiesFromCollection(long page, long itemsPerPage, string orderExpression = "", Direction direction = Direction.Descending)
        {
            return Service.GetEntitiesFromCollection(this.CollectionKey, page, itemsPerPage, orderExpression, direction);
        }

        /// <inheritdoc/>
        public virtual PagedCollection<TEntity> GetEntitiesFromCollection(string searchTerm, long page, long itemsPerPage, string orderExpression = "", Direction direction = Direction.Descending)
        {
            return Service.GetEntitiesFromCollection(this.CollectionKey, searchTerm, page, itemsPerPage, orderExpression, direction);
        }

        /// <inheritdoc/>
        public virtual PagedCollection<TEntity> GetEntitiesNotInCollection(long page, long itemsPerPage, string orderExpression = "", Direction direction = Direction.Descending)
        {
            return Service.GetEntitiesNotInCollection(this.CollectionKey, page, itemsPerPage, orderExpression, direction);
        }


        /// <summary>
        /// Returns true if the entity exists in the collection.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected virtual bool PerformExists(TEntity entity)
        {
            return Service.ExistsInCollection(entity.Key, this.CollectionKey);
        }


        /// <summary>
        /// Gets the request cache key for the exists query.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetExistsCacheKey(TEntity entity)
        {
            return string.Format("{0}.{1}.exists", typeof(TEntity), entity.Key);
        }
    }
}