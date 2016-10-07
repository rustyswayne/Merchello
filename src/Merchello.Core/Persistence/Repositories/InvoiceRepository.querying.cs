using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Persistence.Repositories
{
    using System.Web.UI.WebControls;

    using Merchello.Core.Models;

    internal partial class InvoiceRepository : IInvoiceRepository
    {
        public PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(
            string searchTerm,
            Guid invoiceStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(
            string searchTerm,
            Guid invoiceStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(
            string searchTerm,
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }

        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(
            string searchTerm,
            Guid orderStatusKey,
            long page,
            long itemsPerPage,
            string orderExpression,
            SortDirection sortDirection = SortDirection.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
