namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ItemCacheLineItem"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ItemCacheLineItem))]
    [MapperFor(typeof(IItemCacheLineItem))]
    internal sealed class ItemCacheLineItemMapper : BaseMapper
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

            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Key, dto => dto.Key);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.ContainerKey, dto => dto.ContainerKey);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.LineItemTfKey, dto => dto.LineItemTfKey);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Sku, dto => dto.Sku);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Name, dto => dto.Name);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Quantity, dto => dto.Quantity);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Price, dto => dto.Price);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<ItemCacheLineItem, ItemCacheItemDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}