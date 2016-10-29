namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class OrderService : IOrderService
    {
        /// <inheritdoc/>
        public IOrder Create(Guid orderStatusKey, Guid invoiceKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOrder Create(Guid orderStatusKey, Guid invoiceKey, int orderNumber)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOrder CreateWithKey(Guid orderStatusKey, Guid invoiceKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IOrder entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IOrder> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IOrder entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IOrder> entities)
        {
            throw new NotImplementedException();
        }
    }
}
