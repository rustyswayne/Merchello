namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Models;
    using Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Merchello.Core.Models.CatalogInventory"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(CatalogInventory))]
    [MapperFor(typeof(ICatalogInventory))]
    internal sealed class CatalogInventoryMapper : BaseMapper
    {
        /// <summary>
        /// The mapper specific instance of the the property info cache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, DtoMapModel> PropertyInfoCacheInstance = new ConcurrentDictionary<string, DtoMapModel>();

        /// <inheritdoc/>
        internal override ConcurrentDictionary<string, DtoMapModel> PropertyInfoCache => PropertyInfoCacheInstance;

        /// <summary>
        /// Maps <see cref="CatalogInventory"/> properties to <see cref="CatalogInventoryDto"/> fields.
        /// </summary>
        protected override void BuildMap()
        {
            if (!PropertyInfoCache.IsEmpty) return;

            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.CatalogKey, dto => dto.CatalogKey);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.ProductVariantKey, dto => dto.ProductVariantKey);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.Count, dto => dto.Count);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.LowCount, dto => dto.LowCount);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.Location, dto => dto.Location);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<CatalogInventory, CatalogInventoryDto>(src => src.CreateDate, dto => dto.CreateDate);
        } 
    }
}