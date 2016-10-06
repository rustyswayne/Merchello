namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOrderLineItem"/> entities.
    /// </summary>
    public interface IOrderLineItemRepository : INPocoEntityRepository<IOrderLineItem>, ILineItemRepository<IOrderLineItem>
    {
        /// <summary>
        /// Updates order line item records that have shipment association with a shipment that is being deleted.
        /// </summary>
        /// <param name="shipmentKey">
        /// The shipment key.
        /// </param>
        /// <remarks>
        /// Sets the shipment = NULL
        /// </remarks>
        void UpdateForShipmentDelete(Guid shipmentKey);
    }
}