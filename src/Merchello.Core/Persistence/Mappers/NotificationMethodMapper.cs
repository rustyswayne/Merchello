namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="NotificationMethod"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(NotificationMethod))]
    [MapperFor(typeof(INotificationMethod))]
    internal sealed class NotificationMethodMapper : BaseMapper
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

            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.Key, dto => dto.Key);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.ProviderKey, dto => dto.ProviderKey);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.Name, dto => dto.Name);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.Description, dto => dto.Description);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.ServiceCode, dto => dto.ServiceCode);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<NotificationMethod, NotificationMethodDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}