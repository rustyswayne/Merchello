namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="StoreSetting"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(StoreSetting))]
    [MapperFor(typeof(IStoreSetting))]
    internal sealed class StoreSettingMapper : BaseMapper
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

             CacheMap<StoreSetting, StoreSettingDto>(src => src.Key, dto => dto.Key);
             CacheMap<StoreSetting, StoreSettingDto>(src => src.Name, dto => dto.Name);
             CacheMap<StoreSetting, StoreSettingDto>(src => src.Value, dto => dto.Value);
             CacheMap<StoreSetting, StoreSettingDto>(src => src.TypeName, dto => dto.TypeName);
             CacheMap<StoreSetting, StoreSettingDto>(src => src.UpdateDate, dto => dto.UpdateDate);
             CacheMap<StoreSetting, StoreSettingDto>(src => src.CreateDate, dto => dto.CreateDate);
         }
    }
}