namespace Merchello.Core.Gateways.Notification
{
    using System.Collections.Generic;

    using Models;
    using Services;

    /// <summary>
    /// Represents a NotificationGatewayMethodBase object
    /// </summary>
    public abstract class NotificationGatewayMethodBase : INotificationGatewayMethod
    {
        /// <summary>
        /// The <see cref="INotificationMethod"/>.
        /// </summary>
        private readonly INotificationMethod _notificationMethod;

        /// <summary>
        /// The collection of <see cref="INotificationMessage"/>.
        /// </summary>
        private IEnumerable<INotificationMessage> _notificationMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationGatewayMethodBase"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="notificationMethod">
        /// The notification method.
        /// </param>
        protected NotificationGatewayMethodBase(IGatewayProviderService gatewayProviderService, INotificationMethod notificationMethod)
        {
            Ensure.ParameterNotNull(gatewayProviderService, "gatewayProviderService");
            Ensure.ParameterNotNull(notificationMethod, "notificationMethod");

            _notificationMethod = notificationMethod;
            this.GatewayProviderService = gatewayProviderService;
        }

        /// <summary>
        /// Gets the <see cref="INotificationMethod"/>
        /// </summary>
        public INotificationMethod NotificationMethod => this._notificationMethod;

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/>s associated with this NotificationMethod
        /// </summary>
        public IEnumerable<INotificationMessage> NotificationMessages => this._notificationMessages ??
                                                                         (this._notificationMessages =
                                                                          this.GatewayProviderService.GetNotificationMessagesByMethodKey(this._notificationMethod.Key));

        /// <summary>
        /// Gets the <see cref="IGatewayProviderService"/>
        /// </summary>
        protected IGatewayProviderService GatewayProviderService { get; }

        /// <summary>
        /// Creates a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="name">A name for the message (used in Back Office UI)</param>
        /// <param name="description">A description for the message (used in Back Office UI)</param>
        /// <param name="fromAddress">The senders or "From Address"</param>
        /// <param name="recipients">A collection of recipients</param>
        /// <param name="bodyText">The body text for the message</param>
        /// <returns>A <see cref="INotificationMessage"/></returns>
        public INotificationMessage CreateNotificationMessage(string name, string description, string fromAddress, IEnumerable<string> recipients, string bodyText)
        {
            return GatewayProviderService.CreateNotificationMessageWithKey(_notificationMethod.Key, name, description, fromAddress, recipients, bodyText);
        }

        /// <summary>
        /// Saves a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to be saved</param>
        public void SaveNotificationMessage(INotificationMessage message)
        {
            GatewayProviderService.Save(message);

            _notificationMessages = null;
        }

        /// <summary>
        /// Deletes a <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="INotificationMessage"/> to be deleted</param>
        public void DeleteNotificationMessage(INotificationMessage message)
        {
            GatewayProviderService.Delete(message);

            _notificationMessages = null;
        }

        /// <summary>
        /// Sends a <see cref="IFormattedNotificationMessage"/>
        /// </summary>
        /// <param name="notificationMessage">The <see cref="IFormattedNotificationMessage"/> to be sent</param>
        public virtual void Send(INotificationMessage notificationMessage)
        {
            Send(notificationMessage, new DefaultFormatter());
        }

        /// <summary>
        /// Sends a <see cref="IFormattedNotificationMessage"/>
        /// </summary>
        /// <param name="notificationMessage">The <see cref="IFormattedNotificationMessage"/> to be sent</param>
        /// <param name="formatter">The <see cref="IMessageFormatter"/> to use to format the message</param>
        public virtual void Send(INotificationMessage notificationMessage, IMessageFormatter formatter)
        {
            PerformSend(new FormattedNotificationMessage(notificationMessage, formatter)); 
        }

        /// <summary>
        /// Does the actual work of sending the <see cref="IFormattedNotificationMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="IFormattedNotificationMessage"/> to be sent</param>
        public abstract void PerformSend(IFormattedNotificationMessage message);        
    }
}