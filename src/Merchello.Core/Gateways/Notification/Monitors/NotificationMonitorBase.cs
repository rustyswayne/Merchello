namespace Merchello.Core.Gateways.Notification.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Logging;

    using Models;
    using Observation;

    /// <summary>
    /// Defines a <see cref="NotificationMonitorBase{T}"/> base class
    /// </summary>
    /// <typeparam name="T">
    /// The Type of the model passed to the monitor
    /// </typeparam>
    public abstract class NotificationMonitorBase<T> : MonitorBase<T>, INotificationMonitorBase
    {
        /// <summary>
        /// The notification context.
        /// </summary>
        private readonly INotificationContext _notificationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMonitorBase{T}"/> class. 
        /// </summary>
        /// <param name="notificationContext">
        /// The notification context.
        /// </param>
        protected NotificationMonitorBase(INotificationContext notificationContext)
        {
            Ensure.ParameterNotNull(notificationContext, "notificationContext");
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Gets the message model type.
        /// </summary>
        public Type MessageModelType => typeof(T);

        /// <summary>
        /// Gets the cached collection of <see cref="INotificationMessage"/>
        /// </summary>
        protected IEnumerable<INotificationMessage> Messages => this.GetNotificationMessages();

        /// <summary>
        /// Method used in Lazy collection instantiation of <see cref="INotificationMessage"/>
        /// </summary>
        /// <returns>
        /// A collection of <see cref="INotificationMessage"/>
        /// </returns>
        private IEnumerable<INotificationMessage> GetNotificationMessages()
        {
            var key = GetType().GetCustomAttribute<MonitorForAttribute>(false).Key;
            return ((NotificationContext)_notificationContext).GetNotificationMessagesByMonitorKey(key);
        }
    }
}