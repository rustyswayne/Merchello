namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : INotificationMessageService
    {
        /// <inheritdoc/>
        public INotificationMessage GetNotificationMessageByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                var message = repo.Get(key);
                uow.Complete();
                return message;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<INotificationMessage> GetAllNotificationMessages()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                var messages = repo.GetAll();
                uow.Complete();
                return messages;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<INotificationMessage> GetNotificationMessagesByMethodKey(Guid notificationMethodKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                var message = repo.GetByQuery(repo.Query.Where(x => x.MethodKey == notificationMethodKey));
                uow.Complete();
                return message;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<INotificationMessage> GetNotificationMessagesByMonitorKey(Guid monitorKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                var message = repo.GetByQuery(repo.Query.Where(x => x.MonitorKey == monitorKey));
                uow.Complete();
                return message;
            }
        }

        /// <inheritdoc/>
        public INotificationMessage CreateNotificationMessageWithKey(Guid methodKey, string name, string description, string fromAddress, IEnumerable<string> recipients, string bodyText)
        {
            var recipientArray = recipients as string[] ?? recipients.ToArray();

            Ensure.ParameterCondition(methodKey != Guid.Empty, "methodKey");
            Ensure.ParameterNotNullOrEmpty(name, "name");
            Ensure.ParameterNotNullOrEmpty(fromAddress, "fromAddress");

            var message = new NotificationMessage(methodKey, name, fromAddress)
            {
                Description = description,
                BodyText = bodyText,
                Recipients = string.Join(",", recipientArray)
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                repo.AddOrUpdate(message);
                uow.Complete();
            }

            return message;
        }

        /// <inheritdoc/>
        public void Save(INotificationMessage notificationMessage)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                repo.AddOrUpdate(notificationMessage);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<INotificationMessage> notificationMessages)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                foreach (var message in notificationMessages)
                {
                    repo.AddOrUpdate(message);
                }
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(INotificationMessage notificationMessage)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMessageRepository>();
                repo.Delete(notificationMessage);
                uow.Complete();
            }
        }
    }
}
