namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="INotificationMethod"/>.
    /// </summary>
    public interface INotificationMethodService : IService
    {
        /// <summary>
        /// Gets a <see cref="INotificationMethod"/> by it's key
        /// </summary>
        /// <param name="key">The key (Guid) of the <see cref="INotificationMethod"/> to be retrieved</param>
        /// <returns>The <see cref="INotificationMethod"/></returns>
        INotificationMethod GetNotificationMethodByKey(Guid key);

        /// <summary>
        /// Gets a collection of <see cref="INotificationMethod"/> for a give NotificationGatewayProvider
        /// </summary>
        /// <param name="providerKey">The unique 'key' of the NotificationGatewayProvider</param>
        /// <returns>A collection of <see cref="INotificationMethod"/></returns>
        IEnumerable<INotificationMethod> GetNotificationMethodsByProviderKey(Guid providerKey);

        /// <summary>
        /// Gets a collection of all <see cref="INotificationMethod"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{INotificationMethod}"/>.
        /// </returns>
        IEnumerable<INotificationMethod> GetAllNotificationMethods();

        /// <summary>
        /// Creates a <see cref="INotificationMethod"/> and saves it to the database
        /// </summary>
        /// <param name="providerKey">The <see cref="IGatewayProviderSettings"/> key</param>
        /// <param name="name">The name of the notification (used in back office)</param>
        /// <param name="serviceCode">The notification service code</param>
        /// <returns>The <see cref="INotificationMethod"/> created and saved</returns>
        INotificationMethod CreateNotificationMethodWithKey(Guid providerKey, string name, string serviceCode);


        /// <summary>
        /// Saves a single instance of <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="notificationMethod">The <see cref="INotificationMethod"/> to be saved</param>
        void Save(INotificationMethod notificationMethod);

        /// <summary>
        /// Saves a collection of <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="notificationMethods">The collection of <see cref="INotificationMethod"/> to be saved</param>
        void Save(IEnumerable<INotificationMethod> notificationMethods);

        /// <summary>
        /// Deletes a single instance of <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="notificationMethod">The <see cref="INotificationMethod"/> to be deleted</param>
        void Delete(INotificationMethod notificationMethod);

        /// <summary>
        /// Deletes a collection of <see cref="INotificationMethod"/>
        /// </summary>
        /// <param name="notificationMethods">The collection of <see cref="INotificationMethod"/> to be deleted</param>
        void Delete(IEnumerable<INotificationMethod> notificationMethods);
    }
}