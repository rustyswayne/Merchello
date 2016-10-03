namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="NotificationMessage"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(NotificationMessage))]
    [MapperFor(typeof(INotificationMessage))]
    internal sealed class NotificationMessageMapper : BaseMapper
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

            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.Key, dto => dto.Key);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.MethodKey, dto => dto.MethodKey);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.MonitorKey, dto => dto.MonitorKey);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.Name, dto => dto.Name);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.FromAddress, dto => dto.FromAddress);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.ReplyTo, dto => dto.ReplyTo);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.Description, dto => dto.Description);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.BodyText, dto => dto.BodyText);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.MaxLength, dto => dto.MaxLength);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.BodyTextIsFilePath, dto => dto.BodyTextIsFilePath);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.Recipients, dto => dto.Recipients);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.SendToCustomer, dto => dto.SendToCustomer);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.Disabled, dto => dto.Disabled);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<NotificationMessage, NotificationMessageDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}