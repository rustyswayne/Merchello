﻿namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class ShipmentService : IShipmentService
    {
        /// <inheritdoc/>
        public IShipmentStatus GetShipmentStatusByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentStatusRepository>();
                var status = repo.Get(key);
                uow.Complete();
                return status;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipmentStatus> GetAllShipmentStatuses()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentStatusRepository>();
                var statuses = repo.GetAll();
                uow.Complete();
                return statuses;
            }
        }
    }
}
