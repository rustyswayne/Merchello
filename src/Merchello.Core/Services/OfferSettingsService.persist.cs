namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class OfferSettingsService : IOfferSettingsService
    {
        /// <inheritdoc/>
        public IOfferSettings Create(string name, string offerCode, Guid offerProviderKey)
        {
            return Create(name, offerCode, offerProviderKey, new OfferComponentDefinitionCollection());
        }

        /// <inheritdoc/>
        public IOfferSettings Create(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions)
        {
            var offerSettings = new OfferSettings(name, offerCode, offerProviderKey, componentDefinitions);

            if (Creating.IsRaisedEventCancelled(new NewEventArgs<IOfferSettings>(offerSettings), this))
            {
                offerSettings.WasCancelled = true;
                return offerSettings;
            }

            Created.RaiseEvent(new NewEventArgs<IOfferSettings>(offerSettings, false), this);
            return offerSettings;
        }

        /// <inheritdoc/>
        public IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey)
        {
            return CreateWithKey(name, offerCode, offerProviderKey, new OfferComponentDefinitionCollection());
        }

        /// <inheritdoc/>
        public IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions)
        {
            var offerSettings = Create(name, offerCode, offerProviderKey, componentDefinitions);
            Save(offerSettings);
            return offerSettings;
        }

        /// <inheritdoc/>
        public void Save(IOfferSettings entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IOfferSettings>(entity), this))
            {
                ((OfferSettings)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IOfferSettings>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IOfferSettings> entities)
        {
            var offerSettingsArr = entities as IOfferSettings[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IOfferSettings>(offerSettingsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                foreach (var setting in offerSettingsArr)
                {
                    repo.AddOrUpdate(setting);
                }
                
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IOfferSettings>(offerSettingsArr, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IOfferSettings entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IOfferSettings>(entity), this))
            {
                ((OfferSettings)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IOfferSettings>(entity), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IOfferSettings> entities)
        {
            var offerSettingsArr = entities as IOfferSettings[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IOfferSettings>(offerSettingsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                foreach (var setting in offerSettingsArr)
                {
                    repo.Delete(setting);
                }
                
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IOfferSettings>(offerSettingsArr), this);
        }
    }
}
