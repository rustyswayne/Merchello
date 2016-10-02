namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="EntityCollection"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(EntityCollection))]
    [MapperFor(typeof(IEntityCollection))]
    internal sealed class EntityCollectionMapper : BaseMapper
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

            CacheMap<EntityCollection, EntityCollectionDto>(src => src.Key, dto => dto.Key);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.EntityTfKey, dto => dto.EntityTfKey);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.ParentKey, dto => dto.ParentKey);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.ProviderKey, dto => dto.ProviderKey);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.Name, dto => dto.Name);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.SortOrder, dto => dto.SortOrder);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.IsFilter, dto => dto.IsFilter);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<EntityCollection, EntityCollectionDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}