namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents the NotificationMethodFactory
    /// </summary>
    internal class NotificationMethodFactory : IEntityFactory<INotificationMethod, NotificationMethodDto>
    {
        /// <summary>
        /// builds <see cref="INotificationMethod"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="NotificationMethodDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="INotificationMethod"/>.
        /// </returns>
        public INotificationMethod BuildEntity(NotificationMethodDto dto)
        {
            var method = new NotificationMethod(dto.ProviderKey)
            {
                Key = dto.Key,
                Name = dto.Name,
                Description = dto.Description,
                ServiceCode = dto.ServiceCode,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            method.ResetDirtyProperties();

            return method;
        }

        /// <summary>
        /// Builds <see cref="NotificationMethodDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="INotificationMethod"/>.
        /// </param>
        /// <returns>
        /// The <see cref="NotificationMethodDto"/>.
        /// </returns>
        public NotificationMethodDto BuildDto(INotificationMethod entity)
        {
            return new NotificationMethodDto()
            {
                Key = entity.Key,
                ProviderKey = entity.ProviderKey,
                Name = entity.Name,
                Description = entity.Description,
                ServiceCode = entity.ServiceCode,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}