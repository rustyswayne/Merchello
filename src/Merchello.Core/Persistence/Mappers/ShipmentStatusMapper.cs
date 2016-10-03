namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Models;
    using Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ShipmentStatus"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ShipmentStatus))]
    [MapperFor(typeof(IShipmentStatus))]
    internal sealed class ShipmentStatusMapper : BaseMapper
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

            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.Key, dto => dto.Key);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.Name, dto => dto.Name);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.Alias, dto => dto.Alias);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.Reportable, dto => dto.Reportable);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.Active, dto => dto.Active);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.SortOrder, dto => dto.SortOrder);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<ShipmentStatus, ShipmentStatusDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
