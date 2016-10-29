namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class OrderService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IOrderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public OrderService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IOrder GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOrder> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOrder GetByOrderNumber(int orderNumber)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOrder> GetByInvoiceKey(Guid invoiceKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOrderStatus GetOrderStatusByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOrderStatus> GetAllOrderStatuses()
        {
            throw new NotImplementedException();
        }
    }
}