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
    public partial class EntityCollectionService : IEntityCollectionService
    {
        /// <inheritdoc/>
        public IEntityCollection Create(EntityType entityType, Guid providerKey, string name)
        {
            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return Create(entityTfKey, providerKey, name);
        }

        /// <inheritdoc/>
        public IEntityCollection Create(Guid entityTfKey, Guid providerKey, string name)
        {
            var collection = new EntityCollection(entityTfKey, providerKey) { Name = name, ExtendedData = new ExtendedDataCollection() };

            if (!Creating.IsRaisedEventCancelled(new NewEventArgs<IEntityCollection>(collection), this))
            {
                Created.RaiseEvent(new NewEventArgs<IEntityCollection>(collection, false), this);
                return collection;
            }

            collection.WasCancelled = true;
            return collection;
        }

        /// <inheritdoc/>
        public IEntityCollection CreateWithKey(EntityType entityType, Guid providerKey, string name)
        {
            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return CreateWithKey(entityTfKey, providerKey, name);
        }

        /// <inheritdoc/>
        public IEntityCollection CreateWithKey(Guid entityTfKey, Guid providerKey, string name)
        {
            var collection = Create(entityTfKey, providerKey, name);
            Save(collection);
            return collection;
        }

        /// <inheritdoc/>
        public void Save(IEntityCollection entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IEntityCollection>(entity), this))
            {
                ((EntityCollection)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IEntityCollection>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IEntityCollection> entities)
        {
            var evtMsgs = EventMessagesFactory.Get();
            var collections = entities as IEntityCollection[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IEntityCollection>(collections, true, evtMsgs), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                foreach (var collection in collections)
                {
                    repo.AddOrUpdate(collection);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IEntityCollection>(collections, false, evtMsgs), this);
        }

        /// <inheritdoc/>
        public void Delete(IEntityCollection entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IEntityCollection>(entity), this))
            {
                ((EntityCollection)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IEntityCollection>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IEntityCollection> entities)
        {
            var evtMsgs = EventMessagesFactory.Get();
            var collections = entities as IEntityCollection[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IEntityCollection>(collections, true, evtMsgs), this))
            {
                return;
            }


            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                foreach (var collection in collections)
                {
                    repo.Delete(collection);
                }

                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IEntityCollection>(collections, false, evtMsgs), this);
        }
    }
}
