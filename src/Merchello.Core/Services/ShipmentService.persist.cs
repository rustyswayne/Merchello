namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ShipmentService : IShipmentService
    {
        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination, LineItemCollection items)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IShipment entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IShipment> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IShipment entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IShipment> entities)
        {
            throw new NotImplementedException();
        }
    }
}
