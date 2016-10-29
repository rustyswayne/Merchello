namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IOrder"/>.
    /// </summary>
    public interface IOrderService : IGetAllService<IOrder>
    {
        /// <summary>
        /// Gets a <see cref="IOrder"/> given it's unique 'OrderNumber'
        /// </summary>
        /// <param name="orderNumber">The order number of the <see cref="IOrder"/> to be retrieved</param>
        /// <returns><see cref="IOrder"/></returns>
        IOrder GetByOrderNumber(int orderNumber);

        /// <summary>
        /// Gets a collection of <see cref="IOrder"/> for a given <see cref="IInvoice"/> key
        /// </summary>
        /// <param name="invoiceKey">The <see cref="IInvoice"/> key</param>
        /// <returns>A collection of <see cref="IOrder"/></returns>
        IEnumerable<IOrder> GetByInvoiceKey(Guid invoiceKey);

        /// <summary>
        /// Gets an <see cref="IOrderStatus"/> by it's key
        /// </summary>
        /// <param name="key">The <see cref="IInvoiceStatus"/> key</param>
        /// <returns><see cref="IInvoiceStatus"/></returns>
        IOrderStatus GetOrderStatusByKey(Guid key);

        /// <summary>
        /// Returns a collection of all <see cref="IOrderStatus"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IOrderStatus}"/>.
        /// </returns>
        IEnumerable<IOrderStatus> GetAllOrderStatuses();

        /// <summary>
        /// Creates a <see cref="IOrder"/> without saving it to the database
        /// </summary>
        /// <param name="orderStatusKey">The <see cref="IOrderStatus"/> key</param>
        /// <param name="invoiceKey">The invoice key</param>
        /// <returns>The <see cref="IOrder"/></returns>
        IOrder Create(Guid orderStatusKey, Guid invoiceKey);

        /// <summary>
        /// Creates a <see cref="IOrder"/> without saving it to the database
        /// </summary>
        /// <param name="orderStatusKey">
        /// The <see cref="IOrderStatus"/> key
        /// </param>
        /// <param name="invoiceKey">
        /// The invoice key
        /// </param>
        /// <param name="orderNumber">
        /// The order Number.
        /// </param>
        /// <returns>
        /// The <see cref="IOrder"/>.
        /// </returns>
        /// <remarks>
        /// Order number must be a positive integer value or zero
        /// </remarks>
        IOrder Create(Guid orderStatusKey, Guid invoiceKey, int orderNumber);

        /// <summary>
        /// Creates a <see cref="IOrder"/> and saves it to the database
        /// </summary>
        /// <param name="orderStatusKey">The <see cref="IOrderStatus"/> key</param>
        /// <param name="invoiceKey">The invoice key</param>
        /// <returns><see cref="IOrder"/></returns>
        IOrder CreateWithKey(Guid orderStatusKey, Guid invoiceKey);
    }
}