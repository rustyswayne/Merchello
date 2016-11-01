namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class ProductOptionService : IProductOptionService
    {
        /// <inheritdoc/>
        public IProductOption Create(string name, bool shared = false, bool required = true)
        {
            var option = new ProductOption(name)
            {
                UseName = name,
                Shared = shared,
                Required = required
            };

            if (Creating.IsRaisedEventCancelled(new NewEventArgs<IProductOption>(option), this))
            {
                option.WasCancelled = true;
                return option;
            }

            Created.RaiseEvent(new NewEventArgs<IProductOption>(option, false), this);
            return option;
        }

        /// <inheritdoc/>
        public IProductOption CreateProductOptionWithKey(string name, bool shared = false, bool required = true)
        {
            var option = Create(name, shared, required);
            Save(option);
            return option;
        }

        /// <inheritdoc/>
        public void Save(IProductOption entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IProductOption>(entity), this))
            {
                ((ProductOption)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IProductOption>(entity), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IProductOption> entities)
        {
            var productOptionsArr = entities as IProductOption[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IProductOption>(productOptionsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                foreach (var po in productOptionsArr)
                {
                    repo.AddOrUpdate(po);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IProductOption>(productOptionsArr), this);
        }

        /// <inheritdoc/>
        public void Delete(IProductOption entity)
        {
            if (!EnsureSafeOptionDelete(entity))
            {
                MultiLogHelper.Warn<ProductOptionService>("A ProductOption delete attempt was aborted.  The option cannot be deleted due to it being shared with one or more products.");
                return;
            }

            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IProductOption>(entity), this))
            {
                ((ProductOption)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IProductOption>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IProductOption> entities)
        {
            // We have to delete each on seperately so we can ensure sharing rules.
            foreach (var po in entities)
            {
                Delete(po);
            }
        }
    }
}
