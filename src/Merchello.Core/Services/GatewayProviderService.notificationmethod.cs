namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : INotificationMethodService
    {
        /// <inheritdoc/>
        public INotificationMethod GetNotificationMethodByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                var message = repo.Get(key);
                uow.Complete();
                return message;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<INotificationMethod> GetNotificationMethodsByProviderKey(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                var message = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return message;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<INotificationMethod> GetAllNotificationMethods()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                var messages = repo.GetAll();
                uow.Complete();
                return messages;
            }
        }

        /// <inheritdoc/>
        public Attempt<INotificationMethod> CreateNotificationMethodWithKey(Guid providerKey, string name, string serviceCode)
        {
            if (name.IsNullOrWhiteSpace() || serviceCode.IsNullOrWhiteSpace())
            {
                var empty = new Exception("Cannot create notification method without a name and service code");
                return Attempt<INotificationMethod>.Fail(empty);
            }

            var notificationMethod = new NotificationMethod(providerKey)
            {
                Name = name,
                ServiceCode = serviceCode
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                if (repo.Exists(providerKey, serviceCode))
                {
                    uow.Complete();
                    var invalid = new ConstraintException("Notification method already exists");
                    return Attempt<INotificationMethod>.Fail(invalid);
                }

                repo.AddOrUpdate(notificationMethod);
                uow.Complete();
            }

            return Attempt<INotificationMethod>.Succeed(notificationMethod);
        }

        /// <inheritdoc/>
        public void Save(INotificationMethod notificationMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                repo.AddOrUpdate(notificationMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<INotificationMethod> notificationMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();

                foreach (var method in notificationMethods)
                {
                    repo.AddOrUpdate(method);
                }

                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(INotificationMethod notificationMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();
                repo.Delete(notificationMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<INotificationMethod> notificationMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<INotificationMethodRepository>();

                foreach (var method in notificationMethods)
                {
                    repo.Delete(method);
                }
                
                uow.Complete();
            }
        }
    }
}
