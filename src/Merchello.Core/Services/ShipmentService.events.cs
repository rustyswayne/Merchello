namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ShipmentService : IShipmentService
    {
        /// <summary>
        /// Occurs before creating.
        /// </summary>
        public static event TypedEventHandler<IShipmentService, NewEventArgs<IShipment>> Creating;

        /// <summary>
        /// Occurs after creating.
        /// </summary>
        public static event TypedEventHandler<IShipmentService, NewEventArgs<IShipment>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IShipmentService, SaveEventArgs<IShipment>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IShipmentService, SaveEventArgs<IShipment>> Saved;

        /// <summary>
        /// Occurs before an invoice status has changed
        /// </summary>
        public static event TypedEventHandler<IShipmentService, StatusChangeEventArgs<IShipment>> StatusChanging;

        /// <summary>
        /// Occurs after an invoice status has changed
        /// </summary>
        public static event TypedEventHandler<IShipmentService, StatusChangeEventArgs<IShipment>> StatusChanged;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IShipmentService, DeleteEventArgs<IShipment>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IShipmentService, DeleteEventArgs<IShipment>> Deleted;

        /// <summary>
        /// Special event that fires when an order record is updated
        /// </summary>
        internal static event TypedEventHandler<IShipmentService, SaveEventArgs<IOrder>> UpdatedOrder;
    }
}
