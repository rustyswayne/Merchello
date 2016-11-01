namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class OrderService : IOrderService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IOrderService, NewEventArgs<IOrder>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IOrderService, NewEventArgs<IOrder>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IOrderService, SaveEventArgs<IOrder>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IOrderService, SaveEventArgs<IOrder>> Saved;

        /// <summary>
        /// Occurs before an invoice status has changed
        /// </summary>
        public static event TypedEventHandler<IOrderService, StatusChangeEventArgs<IOrder>> StatusChanging;

        /// <summary>
        /// Occurs after an invoice status has changed
        /// </summary>
        public static event TypedEventHandler<IOrderService, StatusChangeEventArgs<IOrder>> StatusChanged;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IOrderService, DeleteEventArgs<IOrder>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IOrderService, DeleteEventArgs<IOrder>> Deleted;
    }
}
