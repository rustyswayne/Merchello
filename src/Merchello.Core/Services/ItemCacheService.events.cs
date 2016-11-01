namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ItemCacheService : IItemCacheService
    {
        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IItemCacheService, NewEventArgs<IItemCache>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IItemCacheService, NewEventArgs<IItemCache>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IItemCacheService, SaveEventArgs<IItemCache>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IItemCacheService, SaveEventArgs<IItemCache>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IItemCacheService, DeleteEventArgs<IItemCache>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IItemCacheService, DeleteEventArgs<IItemCache>> Deleted;

    }
}
