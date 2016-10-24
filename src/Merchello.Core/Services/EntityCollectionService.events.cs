namespace Merchello.Core.Services
{
    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class EntityCollectionService : IEntityCollectionService
    {
        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IEntityCollectionService, Events.NewEventArgs<IEntityCollection>> Creating;


        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IEntityCollectionService, Events.NewEventArgs<IEntityCollection>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IEntityCollectionService, SaveEventArgs<IEntityCollection>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IEntityCollectionService, SaveEventArgs<IEntityCollection>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IEntityCollectionService, DeleteEventArgs<IEntityCollection>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IEntityCollectionService, DeleteEventArgs<IEntityCollection>> Deleted;
    }
}
