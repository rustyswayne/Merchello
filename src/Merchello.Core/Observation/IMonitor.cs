namespace Merchello.Core.Observation
{
    using System;

    /// <summary>
    /// Marker interface for Monitor observers
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// Gets the type being observed
        /// </summary>
        Type ObservesType { get; }

        /// <summary>
        /// Subscribes the monitor to a trigger.
        /// </summary>
        /// <param name="register">
        /// The <see cref="ITriggerRegister"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        IDisposable Subscribe(ITriggerRegister register);
    }
}