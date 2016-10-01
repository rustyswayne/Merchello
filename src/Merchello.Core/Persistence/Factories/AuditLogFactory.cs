﻿namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents an audit log factory.
    /// </summary>
    internal class AuditLogFactory : IEntityFactory<IAuditLog, AuditLogDto>
    {
        /// <summary>
        /// Builds <see cref="IAuditLog"/>
        /// </summary>
        /// <param name="dto">
        /// The <see cref="AuditLogDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IAuditLog"/>.
        /// </returns>
        public IAuditLog BuildEntity(AuditLogDto dto)
        {
            var entity = new AuditLog()
                {
                    Key = dto.Key,
                    EntityKey = dto.EntityKey,
                    EntityTfKey = dto.EntityTfKey,
                    Message = dto.Message,
                    Verbosity = dto.Verbosity,
                    ExtendedData = new ExtendedDataCollection(dto.ExtendedData),
                    IsError = dto.IsError,
                    CreateDate = dto.CreateDate,
                    UpdateDate = dto.UpdateDate
                };

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <summary>
        /// Builds <see cref="AuditLogDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IAuditLog"/>.
        /// </param>
        /// <returns>
        /// The <see cref="AuditLogDto"/>.
        /// </returns>
        public AuditLogDto BuildDto(IAuditLog entity)
        {
            return new AuditLogDto()
                {
                    Key = entity.Key,
                    EntityKey = entity.EntityKey,
                    EntityTfKey = entity.EntityTfKey,
                    Message = entity.Message,
                    Verbosity = entity.Verbosity,
                    ExtendedData = entity.ExtendedData.SerializeToXml(),
                    IsError = entity.IsError,
                    CreateDate = entity.CreateDate,
                    UpdateDate = entity.UpdateDate
                };
        }
    }
}