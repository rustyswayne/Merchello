namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <inheritdoc/>
        public void Save(IStoreSetting entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IStoreSetting>(entity), this))
            {
                ((StoreSetting)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IStoreSetting>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IStoreSetting> entities)
        {
            var evtMsg = EventMessagesFactory.Get();
            var settingsArray = entities as IStoreSetting[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IStoreSetting>(settingsArray, true, evtMsg), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                foreach (var entity in settingsArray)
                {
                    repo.AddOrUpdate(entity);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IStoreSetting>(settingsArray, false, evtMsg), this);
        }

        /// <inheritdoc/>
        public void Delete(IStoreSetting entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IStoreSetting>(entity, true), this))
            {
                ((StoreSetting)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IStoreSetting>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IStoreSetting> entities)
        {
            var evtMsg = EventMessagesFactory.Get();
            var settingsArray = entities as IStoreSetting[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IStoreSetting>(settingsArray, true, evtMsg), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                foreach (var entity in settingsArray)
                {
                    repo.Delete(entity);
                }

                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IStoreSetting>(settingsArray, false, evtMsg), this);
        }
    }
}
