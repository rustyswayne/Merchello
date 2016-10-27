namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="INotificationMessage"/>.
    /// </summary>
    public interface INotificationMessageService : IService
    {
        /// <summary>
        /// Gets a <see cref="INotificationMessage"/> by it's unique key (Guid)
        /// </summary>
        /// <param name="key">The key (Guid) for the <see cref="INotificationMessage"/> to be retrieved</param>
        /// <returns>Optional boolean indicating whether or not to raise events</returns>
        INotificationMessage GetNotificationMessageByKey(Guid key);

        /// <summary>
        /// Gets all notification messages.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{INotificationMessage}"/>.
        /// </returns>
        IEnumerable<INotificationMessage> GetAllNotificationMessages();

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/>s base on the notification method
        /// </summary>
        /// <param name="notificationMethodKey">The <see cref="INotificationMethod"/> key</param>
        /// <returns>Optional boolean indicating whether or not to raise events</returns>
        IEnumerable<INotificationMessage> GetNotificationMessagesByMethodKey(Guid notificationMethodKey);

        /// <summary>
        /// Gets a collection of <see cref="INotificationMessage"/>s based on a monitor key
        /// </summary>
        /// <param name="monitorKey">The Notification Monitor Key (Guid)</param>
        /// <returns>A collection of <see cref="INotificationMessage"/></returns>
        IEnumerable<INotificationMessage> GetNotificationMessagesByMonitorKey(Guid monitorKey);

        /// <summary>
        /// Creates a <see cref="INotificationMessage"/> and saves it to the database
        /// </summary>
        /// <param name="methodKey">The <see cref="INotificationMethod"/> key</param>
        /// <param name="name">The name of the message (primarily used in the back office UI)</param>
        /// <param name="description">The description of the message (primarily used in the back office UI)</param>
        /// <param name="fromAddress">The senders or "from" address</param>
        /// <param name="recipients">A collection of recipient address</param>
        /// <param name="bodyText">The body text of the message</param>
        /// <returns>The <see cref="INotificationMessage"/></returns>
        INotificationMessage CreateNotificationMessageWithKey(Guid methodKey, string name, string description, string fromAddress, IEnumerable<string> recipients, string bodyText);

        /// <summary>
        /// Saves a single instance of <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="notificationMessage">The <see cref="NotificationMessage"/> to be saved</param>
        void Save(INotificationMessage notificationMessage);

        /// <summary>
        /// Saves a collection of <see cref="INotificationMessage"/>s
        /// </summary>
        /// <param name="notificationMessages">The collection of <see cref="INotificationMessage"/>s to be saved</param>
        void Save(IEnumerable<INotificationMessage> notificationMessages);

        /// <summary>
        /// Deletes a single instance of <see cref="INotificationMessage"/>
        /// </summary>
        /// <param name="notificationMessage">The <see cref="INotificationMessage"/> to be deleted</param>
        void Delete(INotificationMessage notificationMessage);
    }
}