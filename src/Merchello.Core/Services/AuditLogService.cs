namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
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
        public IAuditLog CreateWithKey(string message, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, ExtendedDataCollection extendedData, bool isError = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IAuditLog auditLog)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IAuditLog> auditLogs)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IAuditLog auditLog, bool raiseEvents = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IAuditLog> auditLogs)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void DeleteErrorLogs()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IAuditLog> GetByEntityKey(Guid entityKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetErrorLogs(long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}