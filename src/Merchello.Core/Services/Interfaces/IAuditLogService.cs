namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    using NPoco;

    /// <summary>
    /// Represents a data service for <see cref="IAuditLog"/>.
    /// </summary>
    public interface IAuditLogService : IService<IAuditLog>
    {
        /// <summary>
        /// Gets a collection of <see cref="IAuditLog"/> for a particular entity
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IAuditLog}"/>.
        /// </returns>
        IEnumerable<IAuditLog> GetByEntityKey(Guid entityKey);

        /// <summary>
        /// Gets a collection of <see cref="IAuditLog"/> for an entity type
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IAuditLog}"/>.
        /// </returns>
        PagedCollection<IAuditLog> GetByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Gets a collection of <see cref="IAuditLog"/> where IsError == true
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IAuditLog}"/>.
        /// </returns>
        PagedCollection<IAuditLog> GetErrorLogs(long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(string message, bool isError = false);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="extendedData">
        /// The extended data.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record.
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(string message, ExtendedDataCollection extendedData, bool isError = false);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record.
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, bool isError = false);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="extendedData">
        /// The extended Data.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record.
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(Guid? entityKey, EntityType entityType, string message, ExtendedDataCollection extendedData, bool isError = false);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record.
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, bool isError = false);

        /// <summary>
        /// Creates an audit record and saves it to the database
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="extendedData">
        /// The extended data.
        /// </param>
        /// <param name="isError">
        /// Designates whether or not this is an error log record
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        IAuditLog CreateWithKey(Guid? entityKey, Guid? entityTfKey, string message, ExtendedDataCollection extendedData, bool isError = false);

        /// <summary>
        /// Deletes all error logs
        /// </summary>
        void DeleteErrorLogs();
    }
}