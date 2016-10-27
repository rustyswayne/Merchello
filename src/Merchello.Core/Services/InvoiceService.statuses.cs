namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    public partial class InvoiceService : IInvoiceService
    {

        public IInvoiceStatus GetInvoiceStatusByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses()
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(
            string searchTerm,
            Guid invoiceStatusKey,
            long page,
            long itemsPerPage,
            string sortBy = "",
            Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(
            string searchTerm,
            Guid invoiceStatusKey,
            long page,
            long itemsPerPage,
            string sortBy = "",
            Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(
            string searchTerm,
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string sortBy = "",
            Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(
            string searchTerm,
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string sortBy = "",
            Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
