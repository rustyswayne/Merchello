namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IAuditLog"/> entities.
    /// </summary>
    public interface IAuditLogRepository : INPocoEntityRepository<IAuditLog>, ISearchTermRepository<IAuditLog>
    {
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
    }
}