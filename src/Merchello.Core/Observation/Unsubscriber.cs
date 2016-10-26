namespace Merchello.Core.Observation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Utility class to dispose observers
    /// </summary>
    /// <typeparam name="T">The type of the observer to be disposed</typeparam>
    internal class Unsubscriber<T> : IDisposable
    {
        /// <summary>
        /// The observers.
        /// </summary>
        private readonly List<IObserver<T>> _observers;

        /// <summary>
        /// The observer.
        /// </summary>
        private readonly IObserver<T> _observer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Unsubscriber{T}"/> class.
        /// </summary>
        /// <param name="observers">
        /// The observers.
        /// </param>
        /// <param name="observer">
        /// The observer.
        /// </param>
        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        /// <summary>
        /// Disposes the observers.
        /// </summary>
        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer)) _observers.Remove(_observer);
        }
    }
}