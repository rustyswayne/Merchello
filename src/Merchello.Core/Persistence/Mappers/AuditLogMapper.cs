namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Merchello.Core.Models.AuditLog"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(AuditLog))]
    [MapperFor(typeof(IAuditLog))]
    internal sealed class AuditLogMapper : BaseMapper
    {
        /// <summary>
        /// The mapper specific instance of the the property info cache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, DtoMapModel> PropertyInfoCacheInstance = new ConcurrentDictionary<string, DtoMapModel>();

        /// <inheritdoc/>
        internal override ConcurrentDictionary<string, DtoMapModel> PropertyInfoCache => PropertyInfoCacheInstance;

        /// <summary>
        /// The build map.
        /// </summary>
        protected override void BuildMap()
        {
            if (!PropertyInfoCache.IsEmpty) return;

            CacheMap<AuditLog, AuditLogDto>(src => src.Key, dto => dto.Key);
            CacheMap<AuditLog, AuditLogDto>(src => src.EntityKey, dto => dto.EntityKey);
            CacheMap<AuditLog, AuditLogDto>(src => src.EntityTfKey, dto => dto.EntityTfKey);
            CacheMap<AuditLog, AuditLogDto>(src => src.Message, dto => dto.Message);
            CacheMap<AuditLog, AuditLogDto>(src => src.Verbosity, dto => dto.Verbosity);
            CacheMap<AuditLog, AuditLogDto>(src => src.IsError, dto => dto.IsError);
            CacheMap<AuditLog, AuditLogDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<AuditLog, AuditLogDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}