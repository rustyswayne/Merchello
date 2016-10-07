namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOrder"/> entities.
    /// </summary>
    public interface IOrderRepository : INPocoEntityRepository<IOrder>, IEnsureDocumentNumberRepository
    {
        /// <summary>
        /// Gets an order collection for an invoice.
        /// </summary>
        /// <param name="invoiceKey">
        /// The invoice key.
        /// </param>
        /// <returns>
        /// The <see cref="OrderCollection"/>.
        /// </returns>
        OrderCollection GetOrderCollection(Guid invoiceKey);
    }
}