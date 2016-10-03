namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ItemCacheLineItem"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ItemCache))]
    [MapperFor(typeof(IItemCache))]
    internal sealed class ItemCacheMapper : BaseMapper
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

            CacheMap<ItemCache, ItemCacheDto>(src => src.Key, dto => dto.Key);
            CacheMap<ItemCache, ItemCacheDto>(src => src.ItemCacheTfKey, dto => dto.ItemCacheTfKey);
            CacheMap<ItemCache, ItemCacheDto>(src => src.EntityKey, dto => dto.EntityKey);
            CacheMap<ItemCache, ItemCacheDto>(src => src.VersionKey, dto => dto.VersionKey);
            CacheMap<ItemCache, ItemCacheDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<ItemCache, ItemCacheDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
