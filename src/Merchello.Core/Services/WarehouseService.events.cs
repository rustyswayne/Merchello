namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class WarehouseService : IWarehouseService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IWarehouseService, Events.NewEventArgs<IWarehouse>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IWarehouseService, Events.NewEventArgs<IWarehouse>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IWarehouseService, SaveEventArgs<IWarehouse>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IWarehouseService, SaveEventArgs<IWarehouse>> Saved;


        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IWarehouseService, DeleteEventArgs<IWarehouse>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IWarehouseService, DeleteEventArgs<IWarehouse>> Deleted;
    }
}
