namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public class AuditLogService : RepositoryServiceBase, IAuditLogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLogService"/> class. 
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public AuditLogService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        #region Event Handlers

        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IAuditLogService, Events.NewEventArgs<IAuditLog>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IAuditLogService, Events.NewEventArgs<IAuditLog>> Created;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IAuditLogService, SaveEventArgs<IAuditLog>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IAuditLogService, SaveEventArgs<IAuditLog>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IAuditLogService, DeleteEventArgs<IAuditLog>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IAuditLogService, DeleteEventArgs<IAuditLog>> Deleted;


        #endregion

        /// <inheritdoc/>
        public IAuditLog GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                return repo.Get(key);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IAuditLog> GetByEntityKey(Guid entityKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                var query = repo.Query.Where(x => x.EntityKey == entityKey);
                return repo.GetByQuery(query);
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                return repo.GetByEntityTfKey(entityTfKey, page, itemsPerPage, sortBy, direction);
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetErrorLogs(long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                return repo.GetErrorLogs(page, itemsPerPage, sortBy, direction);
            }
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(string message, bool isError = false)
        {
            return CreateWithKey(null, null, message, isError);
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            return CreateWithKey(null, null, message, extendedData, isError);
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, bool isError = false)
        {
            var tfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return CreateWithKey(entityKey, tfKey, message, isError);
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            var tfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;
            return CreateWithKey(entityKey, tfKey, message, extendedData, isError);
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, bool isError = false)
        {
            return CreateWithKey(entityKey, entityTfKey, message, new ExtendedDataCollection(), isError);
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            var auditLog = new AuditLog()
            {
                EntityKey = entityKey,
                EntityTfKey = entityTfKey,
                Message = message,
                ExtendedData = extendedData,
                IsError = isError
            };

            if (Creating.IsRaisedEventCancelled(new Events.NewEventArgs<IAuditLog>(auditLog), this))
            {
                auditLog.WasCancelled = true;
                return auditLog;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                repo.AddOrUpdate(auditLog);
                uow.Complete();
            }

            Created.RaiseEvent(new Events.NewEventArgs<IAuditLog>(auditLog, false), this);

            return auditLog;
        }

        /// <inheritdoc/>
        public void Save(IAuditLog auditLog)
        {

            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IAuditLog>(auditLog), this))
            {
                ((AuditLog)auditLog).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                repo.AddOrUpdate(auditLog);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IAuditLog>(auditLog), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IAuditLog> auditLogs)
        {
            var auditLogsArray = auditLogs as IAuditLog[] ?? auditLogs.ToArray();

            Saving.RaiseEvent(new SaveEventArgs<IAuditLog>(auditLogsArray), this);
            
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                foreach (var auditLog in auditLogsArray)
                {
                    repo.AddOrUpdate(auditLog);
                }

                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IAuditLog>(auditLogsArray), this);
        }

        /// <inheritdoc/>
        public void Delete(IAuditLog auditLog)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IAuditLog>(auditLog), this))
            {
                ((AuditLog)auditLog).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                repo.Delete(auditLog);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IAuditLog>(auditLog), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IAuditLog> auditLogs)
        {
            var auditLogsArray = auditLogs as IAuditLog[] ?? auditLogs.ToArray();

            Deleting.RaiseEvent(new DeleteEventArgs<IAuditLog>(auditLogsArray), this);

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IAuditLogRepository>();
                foreach (var auditLog in auditLogsArray)
                {
                    repo.Delete(auditLog);
                }

                uow.Complete();
            }


            Deleted.RaiseEvent(new DeleteEventArgs<IAuditLog>(auditLogsArray), this);
        }

        /// <inheritdoc/>
        public void DeleteErrorLogs()
        {
            Delete(GetErrorLogs(1, int.MaxValue).Items);
        }
    }
}