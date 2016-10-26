namespace Merchello.Core.Gateways.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Observation;

    using Models;

    using Services;

    /// <summary>
    /// Represents a NotificationContext
    /// </summary>
    internal class NotificationContext : GatewayProviderTypedContextBase<NotificationGatewayProviderBase>, INotificationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationContext"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="register">
        /// The <see cref="IGatewayProviderRegister"/>.
        /// </param>
        public NotificationContext(Lazy<IGatewayProviderService> gatewayProviderService, IGatewayProviderRegister register)
            : base(gatewayProviderService, register)
        {
        }

        /// <summary>
        /// Returns an instance of an 'active' GatewayProvider associated with a GatewayMethod based given the unique Key (GUID) of the GatewayMethod
        /// </summary>
        /// <param name="gatewayMethodKey">The unique key (GUID) of the <see cref="IGatewayMethod"/></param>
        /// <returns>An instantiated GatewayProvider</returns>
        public override NotificationGatewayProviderBase GetProviderByMethodKey(Guid gatewayMethodKey)
        {
            return GetAllActivatedProviders()
                .FirstOrDefault(x => ((INotificationGatewayProvider)x)
                    .NotificationMethods.Any(y => y.Key == gatewayMethodKey)) as NotificationGatewayProviderBase;
        }

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/>s by a Monitor Key (GUID)
        /// </summary>
        /// <param name="monitorKey">The GUID identifier of the the <see cref="IMonitor"/></param>
        /// <returns>A collection of NotificationMessage</returns>
        public IEnumerable<INotificationMessage> GetNotificationMessagesByMonitorKey(Guid monitorKey)
        {
            return GatewayProviderService.GetNotificationMessagesByMonitorKey(monitorKey).Select(x => x.ShallowCopy());
        }

        /// <summary>
        /// Sends a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to be sent</param>
        public void Send(INotificationMessage message)
        {
            this.Send(message, new DefaultFormatter());
        }

        /// <summary>
        /// Sends a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to be sent</param>
        /// <param name="formatter">The <see cref="IMessageFormatter"/> to use when formatting the message</param>
        public void Send(INotificationMessage message, IMessageFormatter formatter)
        {
            var activeProviders = GetAllActivatedProviders();

            var provider = activeProviders.FirstOrDefault(x => ((INotificationGatewayProvider)x).NotificationMethods.Any(y => y.Key == message.MethodKey)) as NotificationGatewayProviderBase;

            if (provider == null) return;

            var method = provider.GetNotificationGatewayMethodByKey(message.MethodKey);
            
            method.Send(message, formatter);
        }
    }
}