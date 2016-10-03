namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="WarehouseCatalog"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(WarehouseCatalog))]
    [MapperFor(typeof(IWarehouseCatalog))]
    internal sealed class WarehouseCatalogMapper : BaseMapper
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

            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.Key, dto => dto.Key);
            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.WarehouseKey, dto => dto.WarehouseKey);
            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.Name, dto => dto.Name);
            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.Description, dto => dto.Description);
            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<WarehouseCatalog, WarehouseCatalogDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}