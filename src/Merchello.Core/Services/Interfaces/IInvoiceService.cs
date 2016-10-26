namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Represents a data service for <see cref="IInvoice"/>.
    /// </summary>
    public interface IInvoiceService : IEntityCollectionEntityService<IInvoice>, IGetAllService<IInvoice>
    {
        /// <summary>
        /// Gets a collection of <see cref="IInvoice"/> objects that are associated with a <see cref="IPayment"/> by the payments 'key'
        /// </summary>
        /// <param name="paymentKey">The <see cref="IPayment"/> key (GUID)</param>
        /// <returns>A collection of <see cref="IInvoice"/></returns>
        IEnumerable<IInvoice> GetByPaymentKey(Guid paymentKey);

        /// <summary>
        /// Get invoices by a customer key.
        /// </summary>
        /// <param name="customeryKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IInvoice"/>.
        /// </returns>
        IEnumerable<IInvoice> GetByCustomerKey(Guid customeryKey);

        /// <summary>
        /// Get a collection invoices by date range.
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IInvoice"/>.
        /// </returns>
        IEnumerable<IInvoice> GetByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets a <see cref="IInvoice"/> given it's unique 'InvoiceNumber'
        /// </summary>
        /// <param name="invoiceNumber">The invoice number of the <see cref="IInvoice"/> to be retrieved</param>
        /// <returns><see cref="IInvoice"/></returns>
        IInvoice GetByInvoiceNumber(int invoiceNumber);

        /// <summary>
        /// Creates a <see cref="IInvoice"/> without saving it to the database
        /// </summary>
        /// <param name="invoiceStatusKey">The <see cref="IInvoiceStatus"/> key</param>
        /// <returns>The <see cref="IInvoice"/></returns>
        IInvoice Create(Guid invoiceStatusKey);

        /// <summary>
        /// Creates a <see cref="IInvoice"/> with an assigned invoice number without saving it to the database
        /// </summary>
        /// <param name="invoiceStatusKey">
        /// The <see cref="IInvoiceStatus"/> key
        /// </param>
        /// <param name="invoiceNumber">
        /// The invoice Number
        /// </param>
        /// <returns>
        /// <see cref="IInvoice"/>
        /// </returns>
        /// <remarks>
        /// Invoice number must be a positive integer value or zero
        /// </remarks>
        IInvoice Create(Guid invoiceStatusKey, int invoiceNumber);

        /// <summary>
        /// Gets the total count of all invoices
        /// </summary>
        /// <returns>
        /// The <see cref="int"/> representing the count of invoices.
        /// </returns>
        int Count();

        /// <summary>
        /// Gets the total count of all invoices within a date range.
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> representing the count of invoices.
        /// </returns>
        int Count(DateTime startDate, DateTime endDate);

        /// <summary>
        /// The count invoices by customer type
        /// </summary>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="customerType">
        /// The customer type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Count(DateTime startDate, DateTime endDate, CustomerType customerType);

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
        /// Gets distinct currency codes used in invoices.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{String}"/>.
        /// </returns>
        IEnumerable<string> GetDistinctCurrencyCodes();

        /// <summary>
        /// Gets an <see cref="IInvoiceStatus"/> by it's key
        /// </summary>
        /// <param name="key">The <see cref="IInvoiceStatus"/> key</param>
        /// <returns><see cref="IInvoiceStatus"/></returns>
        IInvoiceStatus GetInvoiceStatusByKey(Guid key);

        /// <summary>
        /// Returns a collection of all <see cref="IInvoiceStatus"/>
        /// </summary>
        /// <returns>
        /// The collection of <see cref="IInvoiceStatus"/>.
        /// </returns>
        IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses();

        /// <summary>
        /// Gets invoices matching the search term and the invoice status key.
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
        /// <param name="sortBy">
        /// The sort field.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IInvoice}"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Gets invoices matching the search term but not the invoice status key.
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
        /// <param name="sortBy">
        /// The sort field.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IInvoice}"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);


        /// <summary>
        /// Gets invoices matching the search term and the order status key.
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
        /// <param name="sortBy">
        /// The sort field.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{Guid}"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Gets invoices matching the search term but not the order status key.
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
        /// <param name="sortBy">
        /// The sort field.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IInvoice}"/>.
        /// </returns>
        PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);

    }
}