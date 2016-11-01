namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {
        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetInvoicesMatchingInvoiceStatus(searchTerm, invoiceStatusKey, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetInvoicesMatchingTermNotInvoiceStatus(searchTerm, invoiceStatusKey, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetInvoicesMatchingOrderStatus(searchTerm, orderStatusKey, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetInvoicesMatchingTermNotOrderStatus(searchTerm, orderStatusKey, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return invoices;
            }
        }
    }
}
