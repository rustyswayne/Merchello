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
    public partial class ShipmentService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IShipmentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseBulkUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public ShipmentService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IShipment GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                var s = repo.Get(key);
                uow.Complete();
                return s;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipment> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                var shipments = repo.GetAll(keys);
                uow.Complete();
                return shipments;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipment> GetByShipMethodKey(Guid shipMethodKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                var shipments = repo.GetByQuery(repo.Query.Where(x => x.ShipMethodKey == shipMethodKey));
                uow.Complete();
                return shipments;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IShipment> GetByOrderKey(Guid orderKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Shipments);

                var orderRepo = uow.CreateRepository<IOrderRepository>();
                var order = orderRepo.Get(orderKey);
                var keys = order.Items.Select(x =>
                    {
                        var shipmentKey = ((IOrderLineItem)x).ShipmentKey;
                        if (shipmentKey != null) return shipmentKey.Value;
                        return Guid.Empty;
                    }).Distinct().ToArray();

                // if there are not any shipments associated with this order line item
                if (!keys.Any())
                {
                    uow.Complete();
                    return Enumerable.Empty<IShipment>();
                }

                var repo = uow.CreateRepository<IShipmentRepository>();
                var shipments = repo.GetAll(keys.Where(x => !x.Equals(Guid.Empty)).ToArray());
                uow.Complete();
                return shipments;
            }
        }
    }
}