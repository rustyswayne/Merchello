namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IShipment"/> entities.
    /// </summary>
    public interface IShipmentRepository : INPocoEntityRepository<IShipment>, IEnsureDocumentNumberRepository
    {
        /// <summary>
        /// Updates shipment records that have ship method association with a ship method that is being deleted.
        /// </summary>
        /// <param name="shipMethodKey">
        /// The ship method key.
        /// </param>
        /// <remarks>
        /// Sets the shipMethodKey = NULL
        /// </remarks>
        void UpdateForShipMethodDelete(Guid shipMethodKey);
    }
}