namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {
        /// <inheritdoc/>
        public IInvoiceStatus GetInvoiceStatusByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceStatusRepository>();
                var status = repo.Get(key);
                uow.Complete();
                return status;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceStatusRepository>();
                var statuses = repo.GetAll();
                uow.Complete();
                return statuses;
            }
        }
    }
}
