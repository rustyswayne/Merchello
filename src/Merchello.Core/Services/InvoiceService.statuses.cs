namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {
        /// <inheritdoc/>
        public IInvoiceStatus GetInvoiceStatusByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
