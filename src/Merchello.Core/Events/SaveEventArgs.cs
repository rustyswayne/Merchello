namespace Merchello.Core.Events
{
    using System.Collections.Generic;

    /// <summary>
    /// Event argument for entity saves.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The object to be saved
    /// </typeparam>
    public class SaveEventArgs<TEntity> : CancellableObjectEventArgs<IEnumerable<TEntity>>
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <param name="additionalData">
        /// The additional data.
        /// </param>
        public SaveEventArgs(IEnumerable<TEntity> eventObject, bool canCancel, EventMessages messages, IDictionary<string, object> additionalData) 
            : base(eventObject, canCancel, messages, additionalData)
	    {
	    }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public SaveEventArgs(IEnumerable<TEntity> eventObject, bool canCancel, EventMessages eventMessages)
            : base(eventObject, canCancel, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public SaveEventArgs(IEnumerable<TEntity> eventObject, EventMessages eventMessages)
            : base(eventObject, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="messages">
        /// The messages.
        /// </param>
        /// <param name="additionalData">
        /// The additional data.
        /// </param>
        public SaveEventArgs(TEntity eventObject, bool canCancel, EventMessages messages, IDictionary<string, object> additionalData)
            : base(new List<TEntity> { eventObject }, canCancel, messages, additionalData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public SaveEventArgs(TEntity eventObject, EventMessages eventMessages)
            : base(new List<TEntity> { eventObject }, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public SaveEventArgs(TEntity eventObject, bool canCancel, EventMessages eventMessages)
            : base(new List<TEntity> { eventObject }, canCancel, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        public SaveEventArgs(IEnumerable<TEntity> eventObject, bool canCancel)
			: base(eventObject, canCancel)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The objects associated with the event.
        /// </param>
        public SaveEventArgs(IEnumerable<TEntity> eventObject)
			: base(eventObject)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        public SaveEventArgs(TEntity eventObject)
			: base(new List<TEntity> { eventObject })
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event.
        /// </param>
        /// <param name="canCancel">
        ///  A value indicating the operation can be cancelled.
        /// </param>
        public SaveEventArgs(TEntity eventObject, bool canCancel)
			: base(new List<TEntity> { eventObject }, canCancel)
		{
		}

        /// <summary>
        /// Gets the saved entities.
        /// </summary>
        public IEnumerable<TEntity> SavedEntities
		{
			get { return this.EventObject; }
		}
	}
}