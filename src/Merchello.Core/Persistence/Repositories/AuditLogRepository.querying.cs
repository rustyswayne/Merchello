namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class AuditLogRepository : IAuditLogRepository
    {
        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            var sql = GetBaseQuery(false)
                        .Where<AuditLogDto>(x => x.EntityTfKey == entityTfKey)
                        .AppendOrderExpression<AuditLogRepository>(ValidateSortByField(sortBy), direction);

            return Database.Page<AuditLogDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IAuditLog> GetErrorLogs(long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            var sql = GetBaseQuery(false)
                        .Where<AuditLogDto>(x => x.IsError == false)
                        .AppendOrderExpression<AuditLogRepository>(ValidateSortByField(sortBy), direction);

            return Database.Page<AuditLogDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
