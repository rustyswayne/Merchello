namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Services;

    /// <summary>
    /// The cached queryable entity collection provider base.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of the service to wrap
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of entity
    /// </typeparam>
    public abstract class QueryableEntityCollectionProviderBase<TService, TEntity> : EntityCollectionProviderBase<TService, TEntity>, IQueryableEntityCollectionProvider<TService, TEntity>
        where TService : class, IEntityCollectionEntityService<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryableEntityCollectionProviderBase{TService,TEntity}"/> class.
        /// </summary>
        /// <param name="queryService">
        /// The wrapped query service
        /// </param>
        /// <param name="entityCollectionService">
        /// The entity collection service.
        /// </param>
        /// <param name="cacheHelper">
        /// The cache helper.
        /// </param>
        /// <param name="collectionKey">
        /// The collection key.
        /// </param>
        protected QueryableEntityCollectionProviderBase(TService queryService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(queryService, entityCollectionService, cacheHelper, collectionKey)
        {
        }

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> GetEntities(Dictionary<string, object> args);
    }
}