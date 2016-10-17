namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
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
        public IEnumerable<IEntityCollection> GetByEntityTfKey(Guid entityTfKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByProviderKey(Guid providerKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetChildren(Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool ContainsChildCollection(Guid? parentKey, Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(EntityType entityType, Guid providerKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetRootLevelEntityCollections(Guid entityTfKey, Guid providerKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int ChildEntityCollectionCount(Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasChildEntityCollections(Guid collectionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int CollectionCountManagedByProvider(Guid providerKey)
        {
            throw new NotImplementedException();
        }
    }
}