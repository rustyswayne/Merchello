namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a repository responsible for <see cref="IInvoice"/> entities.
    /// </summary>
    public interface IInvoiceRepository : INPocoEntityRepository<IInvoice>, ISearchTermRepository<IInvoice>, IEntityCollectionEntityRepository<IInvoice>, IEnsureDocumentNumberRepository, IRepository
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
        Money SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode);

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
        Money SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku);

        /// <summary>
        /// Gets a collection of invoice by invoice date range.
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IInvoice}"/>.
        /// </returns>
        IEnumerable<IInvoice> GetByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets a collection of invoices matching an invoice status.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="invoiceStatusKey">
        /// The invoice status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of invoices matching a search term NOT an invoice status.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="invoiceStatusKey">
        /// The invoice status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of invoices matching an order status.
        /// </summary>
        /// <param name="orderStatusKey">
        /// The order status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of invoices NOT matching an order status.
        /// </summary>
        /// <param name="searchTerm">
        /// The search Term.
        /// </param>
        /// <param name="orderStatusKey">
        /// The order status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of invoices matching term NOT order status.
        /// </summary>
        /// <param name="orderStatusKey">
        /// The order status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of invoices matching a search term NOT order status.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="orderStatusKey">
        /// The order status key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);
    }
}