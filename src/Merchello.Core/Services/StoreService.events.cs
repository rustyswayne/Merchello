namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class StoreService : IStoreService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IStoreService, Events.NewEventArgs<IStore>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IStoreService, Events.NewEventArgs<IStore>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IStoreService, SaveEventArgs<IStore>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IStoreService, SaveEventArgs<IStore>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IStoreService, DeleteEventArgs<IStore>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IStoreService, DeleteEventArgs<IStore>> Deleted;
    }
}
