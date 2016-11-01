namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class ItemCacheService : IItemCacheService
    {
        /// <inheritdoc/>
        public IItemCache Create(Guid entityKey, ItemCacheType itemCacheType)
        {
            var tfKey = EnumTypeFieldConverter.ItemItemCache.GetTypeField(itemCacheType).TypeKey;
            return Create(entityKey, tfKey);
        }

        /// <inheritdoc/>
        public IItemCache Create(Guid entityKey, Guid itemCacheTfKey)
        {
            return Create(entityKey, itemCacheTfKey, Guid.NewGuid());
        }

        /// <inheritdoc/>
        public IItemCache Create(Guid entityKey, Guid itemCacheTfKey, Guid versionKey)
        {
            var itemCache = new ItemCache(entityKey, itemCacheTfKey)
            {
                VersionKey = versionKey
            };

            if (Creating.IsRaisedEventCancelled(new NewEventArgs<IItemCache>(itemCache), this))
            {
                itemCache.WasCancelled = true;
                return itemCache;
            }

            Created.RaiseEvent(new NewEventArgs<IItemCache>(itemCache, false), this);
            return itemCache;
        }

        /// <inheritdoc/>
        public void Save(IItemCache entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IItemCache>(entity), this))
            {
                ((ItemCache)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IItemCache>(entity), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IItemCache> entities)
        {
            var itemCacheArr = entities as IItemCache[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IItemCache>(itemCacheArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                foreach (var itemCache in itemCacheArr)
                {
                    repo.AddOrUpdate(itemCache);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IItemCache>(itemCacheArr, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IItemCache entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IItemCache>(entity), this))
            {
                ((ItemCache)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IItemCache>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IItemCache> entities)
        {
            var itemCacheArr = entities as IItemCache[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IItemCache>(itemCacheArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                foreach (var itemCache in itemCacheArr)
                {
                    repo.Delete(itemCache);
                }

                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IItemCache>(itemCacheArr, false), this);
        }
    }
}
