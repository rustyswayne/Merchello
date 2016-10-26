namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IStoreSettingService, NewEventArgs<IStoreSetting>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IStoreSettingService, NewEventArgs<IStoreSetting>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IStoreSettingService, SaveEventArgs<IStoreSetting>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IStoreSettingService, SaveEventArgs<IStoreSetting>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IStoreSettingService, DeleteEventArgs<IStoreSetting>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IStoreSettingService, DeleteEventArgs<IStoreSetting>> Deleted;
    }
}
