namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Strategies.Packaging;

    /// <inheritdoc/>
    public partial class ShipmentService : IShipmentService
    {
        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus)
        {
            return Create(shipmentStatus, new Address(), new Address(), new LineItemCollection());
        }

        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination)
        {
            return Create(shipmentStatus, origin, destination, new LineItemCollection());
        }

        /// <inheritdoc/>
        public IShipment Create(IShipmentStatus shipmentStatus, IAddress origin, IAddress destination, LineItemCollection items)
        {
            Ensure.ParameterNotNull(shipmentStatus, "shipmentStatus");
            Ensure.ParameterNotNull(origin, "origin");
            Ensure.ParameterNotNull(destination, "destination");
            Ensure.ParameterNotNull(items, "items");

            // Use the visitor to filter out and validate shippable line items
            var visitor = new ShippableProductVisitor();
            items.Accept(visitor);

            var lineItemCollection = new LineItemCollection();

            foreach (var item in visitor.ShippableItems)
            {
                lineItemCollection.Add(item);
            }

            var shipment = new Shipment(shipmentStatus, origin, destination, lineItemCollection)
            {
                VersionKey = Guid.NewGuid()
            };

            if (Creating.IsRaisedEventCancelled(new NewEventArgs<IShipment>(shipment), this))
            {
                shipment.WasCancelled = true;
                return shipment;
            }

            Created.RaiseEvent(new NewEventArgs<IShipment>(shipment, false), this);

            return shipment;
        }

        /// <inheritdoc/>
        public void Save(IShipment entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IShipment>(entity), this))
            {
                ((Shipment)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IShipment>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IShipment> entities)
        {
            var shipmentsArr = entities as IShipment[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IShipment>(shipmentsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                foreach (var shipment in shipmentsArr)
                {
                    repo.AddOrUpdate(shipment);
                }
                
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IShipment>(shipmentsArr, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IShipment entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IShipment>(entity), this))
            {
                ((Shipment)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                repo.Delete(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IShipment>(entity), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IShipment> entities)
        {
            var shipmentsArr = entities as IShipment[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IShipment>(shipmentsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Shipments);
                var repo = uow.CreateRepository<IShipmentRepository>();
                foreach (var shipment in shipmentsArr)
                {
                    repo.Delete(shipment);
                }
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IShipment>(shipmentsArr), this);
        }
    }
}
