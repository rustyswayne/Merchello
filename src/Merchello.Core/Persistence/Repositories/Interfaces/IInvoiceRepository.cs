namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Merchello.Core.Models;

    using NPoco;

        /// <summary>
    /// Represents a repository responsible for <see cref="IInvoice"/> entities.
    /// </summary>
    public interface IInvoiceRepository : ISearchTermRepository<IInvoice>, IEntityCollectionEntityRepository<IInvoice>, IEnsureDocumentNumberRepository
    {
        /// <summary>
        /// Gets distinct currency codes used in invoices.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{String}"/>.
        /// </returns>
        IEnumerable<string> GetDistinctCurrencyCodes();

        /// <summary>
        /// Gets the totals of invoices in a date range for a specific currency code.
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <returns>
        /// The sum of the invoice totals.
        /// </returns>
        decimal SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode);

        /// <summary>
        /// Gets the total of line items for a give SKU invoiced in a specific currency across the date range.
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code.
        /// </param>
        /// <param name="sku">
        /// The SKU.
        /// </param>
        /// <returns>
        /// The total of line items for a give SKU invoiced in a specific currency across the date range.
        /// </returns>
        decimal SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku);


        PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);
        
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);
        
        PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);

        PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);

        PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);

        PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, SortDirection sortDirection = SortDirection.Descending);
    }
}