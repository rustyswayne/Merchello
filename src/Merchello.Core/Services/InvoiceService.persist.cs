namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Threading;
    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.Repositories;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {
        /// <inheritdoc/>
        public IInvoice Create(Guid invoiceStatusKey)
        {
            return Create(invoiceStatusKey, 0);
        }

        /// <inheritdoc/>
        public IInvoice Create(Guid invoiceStatusKey, int invoiceNumber)
        {
            Ensure.ParameterCondition(Guid.Empty != invoiceStatusKey, "invoiceStatusKey");
            Ensure.ParameterCondition(invoiceNumber >= 0, "invoiceNumber must be greater than or equal to 0");

            var status = GetInvoiceStatusByKey(invoiceStatusKey);

            var defaultCurrencyCode = this.GetDefaultCurrencyCode();

            var invoice = new Invoice(status)
            {
                VersionKey = Guid.NewGuid(),
                InvoiceNumber = invoiceNumber,
                InvoiceDate = DateTime.Now,
                Total = new Money(0M, defaultCurrencyCode)
            };

            if (Creating.IsRaisedEventCancelled(new Events.NewEventArgs<IInvoice>(invoice), this))
            {
                invoice.WasCancelled = true;
                return invoice;
            }

            Created.RaiseEvent(new Events.NewEventArgs<IInvoice>(invoice), this);

            return invoice;
        }

        /// <inheritdoc/>
        public void Save(IInvoice entity)
        {
            if (!entity.HasIdentity && entity.InvoiceNumber <= 0)
            {
                // We have to generate a new 'unique' invoice number off the configurable value
                entity.InvoiceNumber = _storeSettingService.GetNextInvoiceNumber();
            }

            var includesStatusChange = entity.IsPropertyDirty("InvoiceStatusKey") &&
                                       entity.HasIdentity == false;
           
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IInvoice>(entity), this))
            {
                ((Invoice)entity).WasCancelled = true;
                return;
            }

            if (includesStatusChange) StatusChanging.RaiseEvent(new StatusChangeEventArgs<IInvoice>(entity), this);

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }
            
            Saved.RaiseEvent(new SaveEventArgs<IInvoice>(entity), this);
            if (includesStatusChange) StatusChanged.RaiseEvent(new StatusChangeEventArgs<IInvoice>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IInvoice> entities)
        {
            // Generate Invoice Number for new Invoices in the collection
            var invoicesArray = entities as IInvoice[] ?? entities.ToArray();
            var newInvoiceCount = invoicesArray.Count(x => x.InvoiceNumber <= 0 && !x.HasIdentity);
            if (newInvoiceCount > 0)
            {
                var lastInvoiceNumber =
                    _storeSettingService.GetNextInvoiceNumber(newInvoiceCount);
                foreach (var newInvoice in invoicesArray.Where(x => x.InvoiceNumber <= 0 && !x.HasIdentity))
                {
                    newInvoice.InvoiceNumber = lastInvoiceNumber;
                    lastInvoiceNumber = lastInvoiceNumber - 1;
                }
            }

            var existingInvoicesWithStatusChanges = invoicesArray
                        .Where(x => x.HasIdentity == false && x.IsPropertyDirty("InvoiceStatusKey")).ToArray();


            Saving.RaiseEvent(new SaveEventArgs<IInvoice>(invoicesArray), this);
            if (existingInvoicesWithStatusChanges.Any())
                StatusChanging.RaiseEvent(new StatusChangeEventArgs<IInvoice>(existingInvoicesWithStatusChanges), this);

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                foreach (var invoice in invoicesArray)
                {
                    repo.AddOrUpdate(invoice);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IInvoice>(invoicesArray, false), this);
            if (existingInvoicesWithStatusChanges.Any())
                StatusChanged.RaiseEvent(new StatusChangeEventArgs<IInvoice>(existingInvoicesWithStatusChanges), this);
        }

        /// <inheritdoc/>
        public void Delete(IInvoice entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IInvoice>(entity), this))
            {
                ((Invoice)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IInvoice>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IInvoice> entities)
        {
            var invoicesArr = entities as IInvoice[] ?? entities.ToArray();
            if (!Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IInvoice>(invoicesArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                foreach (var invoice in invoicesArr)
                {
                    repo.Delete(invoice);
                }

                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IInvoice>(invoicesArr, false), this);
        }

        /// <summary>
        /// Gets the default currency code.
        /// </summary>
        /// <returns>
        /// The currency code saved in the store settings.
        /// </returns>
        internal string GetDefaultCurrencyCode()
        {
            return _storeSettingService.GetByKey(Constants.StoreSetting.CurrencyCodeKey).Value;
        }
    }
}
