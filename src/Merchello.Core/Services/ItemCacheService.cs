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
    public partial class ItemCacheService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IItemCacheService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCacheService"/> class.
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
        public ItemCacheService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IItemCache GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var itemCache = repo.Get(key);
                uow.Complete();
                return itemCache;
            }
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCacheType)
        {
            return GetItemCacheWithKey(customer, itemCacheType, Guid.NewGuid());
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCacheType, Guid versionKey)
        {
            Ensure.ParameterCondition(Guid.Empty != versionKey, "versionKey");

            // determine if the consumer already has a item cache of this type, if so return it.
            var itemCache = GetItemCacheByCustomer(customer, itemCacheType);
            if (itemCache != null) return itemCache;

            var itemCacheTfKey = EnumTypeFieldConverter.ItemItemCache.GetTypeField(itemCacheType).TypeKey;
            itemCache = Create(customer.Key, itemCacheTfKey, versionKey);

            Save(itemCache);

            return itemCache;
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetItemCaches(Guid entityKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var itemCaches = repo.GetByQuery(repo.Query.Where(x => x.EntityKey == entityKey));
                uow.Complete();
                return itemCaches;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetEntityItemCaches(Guid entityKey, Guid itemCacheTfKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var itemCaches = repo.GetByQuery(repo.Query.Where(x => x.EntityKey == entityKey && x.ItemCacheTfKey == itemCacheTfKey));
                uow.Complete();
                return itemCaches;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetItemCacheByCustomer(ICustomerBase customer)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var itemCaches = repo.GetByQuery(repo.Query.Where(x => x.EntityKey == customer.Key));
                uow.Complete();
                return itemCaches;
            }
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheByCustomer(ICustomerBase customer, Guid itemCacheTfKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var itemCaches = repo.GetByQuery(repo.Query.Where(x => x.EntityKey == customer.Key && x.ItemCacheTfKey == itemCacheTfKey));
                uow.Complete();
                return itemCaches.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheByCustomer(ICustomerBase customer, ItemCacheType itemCacheType)
        {
            var typeKey = EnumTypeFieldConverter.ItemItemCache.GetTypeField(itemCacheType).TypeKey;
            return GetItemCacheByCustomer(customer, typeKey);
        }
    }
}