namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a notification factory
    /// </summary>
    internal class NotificationMessageFactory : IEntityFactory<INotificationMessage, NotificationMessageDto>
    {
        /// <summary>
        /// Builds <see cref="INotificationMessage"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="NotificationMessageDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="INotificationMessage"/>.
        /// </returns>
        public INotificationMessage BuildEntity(NotificationMessageDto dto)
        {
            var notification = new NotificationMessage(dto.MethodKey, dto.Name, dto.FromAddress)
            {
                Key = dto.Key,
                Description = dto.Description,
                BodyText = dto.BodyText,
                ReplyTo =  dto.ReplyTo,
                MonitorKey = dto.MonitorKey,
                MaxLength = dto.MaxLength == 0 ? int.MaxValue : dto.MaxLength,                
                Recipients = dto.Recipients,
                BodyTextIsFilePath = dto.BodyTextIsFilePath,
                SendToCustomer = dto.SendToCustomer,
                Disabled = dto.Disabled,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            notification.ResetDirtyProperties();

            return notification;
        }

        /// <summary>
        /// Builds <see cref="NotificationMessageDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="INotificationMessage"/>.
        /// </param>
        /// <returns>
        /// The <see cref="NotificationMessageDto"/>.
        /// </returns>
        public NotificationMessageDto BuildDto(INotificationMessage entity)
        {
            return new NotificationMessageDto()
            {
                Key = entity.Key,
                MethodKey = entity.MethodKey,
                Name = entity.Name,
                FromAddress = entity.FromAddress,
                ReplyTo = entity.ReplyTo,
                MonitorKey = entity.MonitorKey,
                Description = entity.Description,
                BodyText = entity.BodyText,
                BodyTextIsFilePath = entity.BodyTextIsFilePath,
                MaxLength = entity.MaxLength == int.MaxValue ? 0 : entity.MaxLength,                
                Recipients = entity.Recipients,
                SendToCustomer = entity.SendToCustomer,
                Disabled = entity.Disabled,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}