namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IGatewayProviderSettingsService
    {
        /// <inheritdoc/>
        public IGatewayProviderSettings GetGatewayProviderSettingsByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                var setting = repo.Get(key);
                uow.Complete();
                return setting;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByType(GatewayProviderType gatewayProviderType)
        {
            var tfKey = EnumTypeFieldConverter.GatewayProvider.GetTypeField(gatewayProviderType).TypeKey;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                var settings = repo.GetByQuery(repo.Query.Where(x => x.ProviderTfKey == tfKey));
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProviderSettings> GetGatewayProvidersByShipCountry(IShipCountry shipCountry)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                var settings = repo.GetByShipCountryKey(shipCountry.Key);
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProviderSettings> GetAllGatewayProviderSettings()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProvidersTree);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                var settings = repo.GetAll();
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public void Save(IGatewayProviderSettings entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IGatewayProviderSettings>(entity), this))
            {
                ((Entity)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IGatewayProviderSettings>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IGatewayProviderSettings> entities)
        {
            var msgs = EventMessagesFactory.Get();
            var entitiesArr = entities as IGatewayProviderSettings[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IGatewayProviderSettings>(entitiesArr, msgs), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                foreach (var entity in entitiesArr)
                {
                    repo.AddOrUpdate(entity);
                }
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IGatewayProviderSettings>(entitiesArr, false, msgs), this);
        }

        /// <inheritdoc/>
        public void Delete(IGatewayProviderSettings entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IGatewayProviderSettings>(entity), this))
            {
               ((Entity)entity).WasCancelled = true;
               return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IGatewayProviderSettings>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IGatewayProviderSettings> entities)
        {
            var settingsArr = entities as IGatewayProviderSettings[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IGatewayProviderSettings>(settingsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IGatewayProviderSettingsRepository>();
                foreach (var entity in settingsArr)
                {
                    repo.Delete(entity);
                }
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IGatewayProviderSettings>(settingsArr, false), this);
        }
    }
}
