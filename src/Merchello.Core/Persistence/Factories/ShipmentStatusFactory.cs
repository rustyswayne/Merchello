﻿namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// The shipment status factory.
    /// </summary>
    internal class ShipmentStatusFactory : IEntityFactory<IShipmentStatus, ShipmentStatusDto>
    {
        /// <summary>
        /// Builds <see cref="IShipmentStatus"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ShipmentStatusDto"/> .
        /// </param>
        /// <returns>
        /// The <see cref="IShipmentStatus"/>.
        /// </returns>
        public IShipmentStatus BuildEntity(ShipmentStatusDto dto)
        {
            var shipmentStatus = new ShipmentStatus()
            {
                Key = dto.Key,
                Name = dto.Name,
                Alias = dto.Alias,
                Reportable = dto.Reportable,
                Active = dto.Active,
                SortOrder = dto.SortOrder,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            shipmentStatus.ResetDirtyProperties();

            return shipmentStatus;
        }

        /// <summary>
        /// Builds <see cref="ShipmentStatusDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IShipmentStatus"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ShipmentStatusDto"/>.
        /// </returns>
        public ShipmentStatusDto BuildDto(IShipmentStatus entity)
        {
            var dto = new ShipmentStatusDto()
            {
                Key = entity.Key,
                Name = entity.Name,
                Alias = entity.Alias,
                Reportable = entity.Reportable,
                Active = entity.Active,
                SortOrder = entity.SortOrder,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };

            return dto;
        }
    }
}