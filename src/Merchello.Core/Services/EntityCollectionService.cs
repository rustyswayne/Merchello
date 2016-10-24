namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class EntityCollectionService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IEntityCollectionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollectionService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public EntityCollectionService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IEntityCollection GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collection = repo.Get(key);
                uow.Complete();
                return collection;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetAll(keys);
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByEntityType(EntityType entityType)
        {
            var validTypes = new[] { EntityType.Product, EntityType.Invoice, EntityType.Customer };
            if (!validTypes.Contains(entityType))
            {
                var invalid = new NotImplementedException("Collections for the entity type passed have not been implemented.");
                MultiLogHelper.Error<IEntityCollectionService>("Invalid entityType", invalid);
                throw invalid;
            }

            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return GetByEntityTfKey(entityTfKey);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByEntityTfKey(Guid entityTfKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.EntityTfKey == entityTfKey));
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByProviderKey(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetChildren(Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.ParentKey == collectionKey));
                uow.Complete();
                return collections.OrderBy(x => x.SortOrder);
            }
        }

        /// <inheritdoc/>
        public bool ContainsChildCollection(Guid? parentKey, Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var count = repo.Count(repo.Query.Where(x => x.ParentKey == parentKey && x.Key == collectionKey));
                uow.Complete();
                return count > 0;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.ParentKey == null));
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType)
        {
            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return GetRootLevelEntityCollections(entityTfKey);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType, Guid providerKey)
        {
            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return GetRootLevelEntityCollections(entityTfKey, providerKey);
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.EntityTfKey == entityTfKey));
                uow.Complete();
                return collections.OrderBy(x => x.SortOrder);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey, Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetByQuery(repo.Query.Where(x => x.EntityTfKey == entityTfKey && x.ProviderKey == providerKey));
                uow.Complete();
                return collections.OrderBy(x => x.SortOrder);
            }
        }

        /// <inheritdoc/>
        public int ChildEntityCollectionCount(Guid collectionKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var count = repo.Count(repo.Query.Where(x => x.ParentKey == collectionKey));
                uow.Complete();
                return count;
            }
        }

        /// <inheritdoc/>
        public bool HasChildEntityCollections(Guid collectionKey)
        {
            return ChildEntityCollectionCount(collectionKey) > 0;
        }

        /// <inheritdoc/>
        public int CollectionCountManagedByProvider(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var count = repo.Count(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return count;
            }
        }
    }
}