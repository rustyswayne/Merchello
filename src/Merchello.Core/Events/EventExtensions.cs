namespace Merchello.Core.Events
{
    using System;

    /// <summary>
	/// Extension methods for cancellable event operations
	/// </summary>
	public static class EventExtensions
	{
        /// <summary>
        /// Raises the event and returns a boolean value indicating if the event was cancelled
        /// </summary>
        /// <typeparam name="TSender">The type of the sender</typeparam>
        /// <typeparam name="TArgs">The type of the argument</typeparam>
        /// <param name="eventHandler">
        /// The event Handler.
        /// </param>
        /// <param name="args">
        /// The event arguments.
        /// </param>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the event was cancelled.
        /// </returns>
        public static bool IsRaisedEventCancelled<TSender, TArgs>(this TypedEventHandler<TSender, TArgs> eventHandler, TArgs args, TSender sender)
			where TArgs : CancellableEventArgs
		{
			if (eventHandler != null)
				eventHandler(sender, args);

			return args.Cancel;
		}

        /// <summary>
        /// Raises the event
        /// </summary>
        /// <typeparam name="TSender">
        /// The type of the sender
        /// </typeparam>
        /// <typeparam name="TArgs">
        /// The type of the argument
        /// </typeparam>
        /// <param name="eventHandler">
        /// The event Handler.
        /// </param>
        /// <param name="args">
        /// The event arguments.
        /// </param>
        /// <param name="sender">
        /// The sender.
        /// </param>
        public static void RaiseEvent<TSender, TArgs>(this TypedEventHandler<TSender, TArgs> eventHandler, TArgs args, TSender sender)
			where TArgs : EventArgs
		{
			if (eventHandler != null)
				eventHandler(sender, args);
		}
	}
}