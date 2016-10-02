namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Migrations;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="MigrationStatus"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(MigrationStatus))]
    [MapperFor(typeof(IMigrationStatus))]
    internal sealed class MigrationStatusMapper : BaseMapper
    {
        /// <summary>
        /// The mapper specific instance of the the property info cache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, DtoMapModel> PropertyInfoCacheInstance = new ConcurrentDictionary<string, DtoMapModel>();

        /// <inheritdoc/>
        internal override ConcurrentDictionary<string, DtoMapModel> PropertyInfoCache => PropertyInfoCacheInstance;

        /// <inheritdoc/>
        protected override void BuildMap()
        {
            if (!PropertyInfoCache.IsEmpty) return;

            CacheMap<MigrationStatus, MigrationStatusDto>(src => src.Key, dto => dto.Key);
            CacheMap<MigrationStatus, MigrationStatusDto>(src => src.MigrationName, dto => dto.Name);
            CacheMap<MigrationStatus, MigrationStatusDto>(src => src.Version, dto => dto.Version);
            CacheMap<MigrationStatus, MigrationStatusDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<MigrationStatus, MigrationStatusDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}