namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
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
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                var order = repo.Get(key);
                uow.Complete();
                return order;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOrder> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                var orders = repo.GetAll(keys);
                uow.Complete();
                return orders;
            }
        }

        /// <inheritdoc/>
        public IOrder GetByOrderNumber(int orderNumber)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                var orders = repo.GetByQuery(repo.Query.Where(x => x.OrderNumber == orderNumber));
                uow.Complete();
                return orders.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOrder> GetByInvoiceKey(Guid invoiceKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                var orders = repo.GetByQuery(repo.Query.Where(x => x.InvoiceKey == invoiceKey));
                uow.Complete();
                return orders;
            }
        }
    }
}