namespace Merchello.Core.Events
{
    /// <summary>
    /// Event arguments for entity creation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object
    /// </typeparam>
    public class NewEventArgs<T> : CancellableObjectEventArgs<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewEventArgs{T}"/> class. 
        /// Constructor accepting entities in a creating operation
        /// </summary>
        /// <param name="eventObject">
        /// The object associated with the event
        /// </param>
        public NewEventArgs(T eventObject)
            : base(eventObject, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewEventArgs{T}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The event object.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public NewEventArgs(T eventObject, EventMessages eventMessages)
            : base(eventObject, true, eventMessages)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewEventArgs{T}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The event object.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the event operation can be cancelled.
        /// </param>
        public NewEventArgs(T eventObject, bool canCancel)
            : base(eventObject, canCancel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewEventArgs{T}"/> class.
        /// </summary>
        /// <param name="eventObject">
        /// The event object.
        /// </param>
        /// <param name="canCancel">
        /// A value indicating the event operation can be cancelled.
        /// </param>
        /// <param name="eventMessages">
        /// The event messages.
        /// </param>
        public NewEventArgs(T eventObject, bool canCancel, EventMessages eventMessages)
            : base(eventObject, canCancel, eventMessages)
        {
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        public T Entity
        {
            get { return EventObject; }
        }
    }
}
