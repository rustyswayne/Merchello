namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="DetachedContentType"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(DetachedContentType))]
    [MapperFor(typeof(IDetachedContentType))]
    internal sealed class DetachedContentTypeMapper : BaseMapper
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

            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.Key, dto => dto.Key);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.Name, dto => dto.Name);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.Description, dto => dto.Description);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.EntityTfKey, dto => dto.EntityTfKey);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.ContentTypeKey, dto => dto.ContentTypeKey);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<DetachedContentType, DetachedContentTypeDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}