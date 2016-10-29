namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ShipmentService : IShipmentService
    {
        /// <inheritdoc/>
        public IShipmentStatus GetShipmentStatusByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IShipmentStatus> GetAllShipmentStatuses()
        {
            throw new NotImplementedException();
        }
    }
}
