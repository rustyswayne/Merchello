namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Store"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Store))]
    [MapperFor(typeof(IStore))]
    internal sealed class StoreMapper : BaseMapper
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

            CacheMap<Store, StoreDto>(src => src.Key, dto => dto.Key);
            CacheMap<Store, StoreDto>(src => src.Name, dto => dto.Name);
            CacheMap<Store, StoreDto>(src => src.Alias, dto => dto.Alias);
            CacheMap<Store, StoreDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Store, StoreDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}