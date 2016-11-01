namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class OrderService : IOrderService
    {
        /// <inheritdoc/>
        public IOrderStatus GetOrderStatusByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderStatusRepository>();
                var status = repo.Get(key);
                uow.Complete();
                return status;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOrderStatus> GetAllOrderStatuses()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderStatusRepository>();
                var statuses = repo.GetAll();
                uow.Complete();
                return statuses;
            }
        }
    }
}
