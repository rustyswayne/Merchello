namespace Merchello.Core.Events
{
    using System.Collections.Generic;

    /// <summary>
    /// Event argument for entity deletes.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity being deleted.
    /// </typeparam>
    public class DeleteEventArgs<TEntity> : CancellableObjectEventArgs<IEnumerable<TEntity>>
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public DeleteEventArgs(IEnumerable<TEntity> eventObject, bool canCancel, EventMessages eventMessages) 
            : base(eventObject, canCancel, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public DeleteEventArgs(IEnumerable<TEntity> eventObject, EventMessages eventMessages) 
            : base(eventObject, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public DeleteEventArgs(TEntity eventObject, EventMessages eventMessages)
            : base(new List<TEntity> { eventObject }, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public DeleteEventArgs(TEntity eventObject, bool canCancel, EventMessages eventMessages)
            : base(new List<TEntity> { eventObject }, canCancel, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the operation can be cancelled.
        /// </param>
        public DeleteEventArgs(IEnumerable<TEntity> eventObject, bool canCancel) 
            : base(eventObject, canCancel)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        public DeleteEventArgs(IEnumerable<TEntity> eventObject) 
            : base(eventObject)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        public DeleteEventArgs(TEntity eventObject)
			: base(new List<TEntity> { eventObject })
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the operation can be cancelled.
        /// </param>
        public DeleteEventArgs(TEntity eventObject, bool canCancel)
			: base(new List<TEntity> { eventObject }, canCancel)
		{
		}

        /// <summary>
        /// Gets the deleted entities.
        /// </summary>
        public IEnumerable<TEntity> DeletedEntities
		{
			get { return this.EventObject; }
		}
	}
}