namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class OrderService : IOrderService
    {
        /// <inheritdoc/>
        public IOrder Create(Guid orderStatusKey, Guid invoiceKey)
        {
            return Create(orderStatusKey, invoiceKey, 0);
        }

        /// <inheritdoc/>
        public IOrder Create(Guid orderStatusKey, Guid invoiceKey, int orderNumber)
        {
            Ensure.ParameterCondition(!Guid.Empty.Equals(orderStatusKey), "orderStatusKey");
            Ensure.ParameterCondition(!Guid.Empty.Equals(invoiceKey), "invoiceKey");
            Ensure.ParameterCondition(orderNumber >= 0, "orderNumber must be greater than or equal to 0");

            var status = GetOrderStatusByKey(orderStatusKey);

            var order = new Order(status, invoiceKey)
            {
                VersionKey = Guid.NewGuid(),
                OrderNumber = orderNumber,
                OrderDate = DateTime.Now
            };

            if (Creating.IsRaisedEventCancelled(new Events.NewEventArgs<IOrder>(order), this))
            {
                order.WasCancelled = true;
                return order;
            }

          Created.RaiseEvent(new Events.NewEventArgs<IOrder>(order), this);

            return order;
        }

        /// <inheritdoc/>
        public IOrder CreateWithKey(Guid orderStatusKey, Guid invoiceKey)
        {
            var order = Create(orderStatusKey, invoiceKey);
            Save(order);
            return order;
        }

        /// <inheritdoc/>
        public void Save(IOrder entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IOrder>(entity), this))
            {
                ((Order)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IOrder>(entity), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IOrder> entities)
        {
            var ordersArr = entities as IOrder[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IOrder>(ordersArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                foreach (var order in ordersArr)
                {
                    repo.AddOrUpdate(order);
                }
               
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IOrder>(ordersArr), this);
        }

        /// <inheritdoc/>
        public void Delete(IOrder entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IOrder>(entity), this))
            {
                ((Order)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IOrder>(entity), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IOrder> entities)
        {
            var orderArr = entities as IOrder[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IOrder>(orderArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderRepository>();
                foreach (var order in orderArr)
                {
                    repo.Delete(order);
                }
                
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IOrder>(orderArr), this);
        }
    }
}
