namespace Merchello.Core.Gateways.Notification.Monitors
{
    using System;

    using Observation;

    /// <summary>
    /// Defines the base NotificationMonitor
    /// </summary>
    public interface INotificationMonitorBase : IMonitor
    {
        /// <summary>
        /// Gets the message model type.
        /// </summary>
        Type MessageModelType { get; }
    }
}