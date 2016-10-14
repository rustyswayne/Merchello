namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <summary>
    /// A base service for services that manage entities that can be associated with <see cref="IEntityCollection"/>
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    /// <typeparam name="TUowProvider">
    /// The type of the unit of work provider.
    /// </typeparam>
    /// <typeparam name="TRepo">
    /// The type of the repository.
    /// </typeparam>
    public abstract class EntityCollectionEntityServiceBase<TEntity, TUowProvider, TRepo> : RepositoryServiceBase<TUowProvider>, IEntityCollectionEntityService<TEntity>
        where TEntity : IEntity
        where TUowProvider : class, IDatabaseUnitOfWorkProvider<IDatabaseUnitOfWork>
        where TRepo : IEntityCollectionEntityRepository<TEntity>, IRepository
    {
        /// <summary>
        /// The lock id.
        /// </summary>
        private readonly int _lockId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionEntityServiceBase{TEntity,TUowProvider,TRepo}"/> class. 
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        /// <param name="lockId">
        /// The lock Id.
        /// </param>
        protected EntityCollectionEntityServiceBase(TUowProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory, int lockId)
            : base(provider, logger, eventMessagesFactory)
        {
            _lockId = lockId;
        }


        /// <inheritdoc/>
        public void AddToCollection(TEntity entity, IEntityCollection collection)
        {
            AddToCollection(entity.Key, collection.Key);
        }

        /// <inheritdoc/>
        public void AddToCollection(TEntity entity, Guid collectionKey)
        {
            AddToCollection(entity.Key, collectionKey);
        }

        /// <inheritdoc/>
        public void AddToCollection(Guid entityKey, Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();

                repo.AddToCollection(entityKey, collectionKey);

                uow.Complete();
            }
        }


        /// <inheritdoc/>
        public void RemoveFromCollection(TEntity entity, IEntityCollection collection)
        {
            RemoveFromCollection(entity.Key, collection.Key);
        }

        /// <inheritdoc/>
        public void RemoveFromCollection(TEntity entity, Guid collectionKey)
        {
            RemoveFromCollection(entity.Key, collectionKey);
        }

        /// <inheritdoc/>
        public void RemoveFromCollection(Guid entityKey, Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();

                repo.RemoveFromCollection(entityKey, collectionKey);

                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public bool ExistsInCollection(Guid entityKey, Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var exists = repo.ExistsInCollection(entityKey, collectionKey);
                uow.Complete();
                return exists;
            }
        }

        /// <inheritdoc/>
        public bool ExistsInAtLeastOneCollection(Guid entityKey, IEnumerable<Guid> collectionKeys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var exists = repo.ExistsInAtLeastOneCollection(entityKey, collectionKeys.ToArray());
                uow.Complete();
                return exists;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesFromCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesFromCollection(collectionKey, page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesFromCollection(Guid collectionKey, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesFromCollection(collectionKey, searchTerm, page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesThatExistInAllCollections(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesThatExistInAllCollections(collectionKeys.ToArray(), page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesThatExistInAllCollections(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesThatExistInAllCollections(collectionKeys.ToArray(), searchTerm, page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesThatExistInAnyCollection(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesThatExistInAnyCollection(collectionKeys.ToArray(), page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesThatExistInAnyCollection(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesThatExistInAnyCollection(collectionKeys.ToArray(), searchTerm, page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesNotInCollection(Guid collectionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesNotInCollection(collectionKey,  page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesNotInAnyCollection(IEnumerable<Guid> collectionKeys, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesNotInAnyCollection(collectionKeys.ToArray(), page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<TEntity> GetEntitiesNotInAnyCollection(IEnumerable<Guid> collectionKeys, string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(_lockId);
                var repo = uow.CreateRepository<TRepo>();
                var results = repo.GetEntitiesNotInAnyCollection(collectionKeys.ToArray(), searchTerm, page, itemsPerPage, orderExpression, direction);
                uow.Complete();
                return results;
            }
        }
    }
}