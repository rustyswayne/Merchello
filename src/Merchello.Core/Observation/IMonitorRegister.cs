namespace Merchello.Core.Observation
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.DI;

    /// <summary>
    /// Represents a register for <see cref="IMonitor"/> classes.
    /// </summary>
    internal interface IMonitorRegister : IRegister<IMonitor>
    {
        /// <summary>
        /// Gets the collection of all registered <see cref="IMonitor"/>s.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the <see cref="IMonitor"/>
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{IMonitor}"/>.
        /// </returns>
        IEnumerable<T> GetAllMonitors<T>();

        /// <summary>
        /// Gets the collection of all registered <see cref="IMonitor"/>s.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMonitor}"/>.
        /// </returns>
        IEnumerable<IMonitor> GetAllMonitors();

        /// <summary>
        /// Gets a <see cref="IMonitor"/> from the register.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the monitor.
        /// </typeparam>
        /// <returns>
        /// A <see cref="IMonitor"/>
        /// </returns>
        IEnumerable<T> GetMonitors<T>();

        /// <summary>
        /// Get's a <see cref="IMonitor"/> by it's attribute Key
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IMonitor"/></typeparam>
        /// <param name="key">The key from the <see cref="MonitorForAttribute"/> (Guid)</param>
        /// <returns>A <see cref="IMonitor"/> of T</returns>
        T GetMonitorByKey<T>(Guid key);

        /// <summary>
        /// Get's a <see cref="IMonitor"/> by it's attribute Key
        /// </summary>
        /// <param name="key">The key from the <see cref="MonitorForAttribute"/> (Guid)</param>
        /// <returns>A <see cref="IMonitor"/> of T</returns>
        IMonitor GetMonitorByKey(Guid key);

        /// <summary>
        /// Gets a collection of all monitors for a particular observable trigger
        /// </summary>
        /// <param name="triggerType">
        /// The Type of the Trigger
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMonitor}"/>.
        /// </returns>
        IEnumerable<IMonitor> GetMonitorsForTrigger(Type triggerType);

        /// <summary>
        /// Gets a collection of all monitors for a particular observable trigger
        /// </summary>
        /// <typeparam name="T">
        /// The Type of the Trigger
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{IMonitor}"/>.
        /// </returns>
        IEnumerable<IMonitor> GetMonitorsForTrigger<T>();
    }
}