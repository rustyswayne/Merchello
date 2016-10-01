namespace Merchello.Core.Persistence.Factories
{
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a ship rate tier factory.
    /// </summary>
    internal class ShipRateTierFactory : IEntityFactory<IShipRateTier, ShipRateTierDto>
    {
        /// <summary>
        /// Builds <see cref="IShipRateTier"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ShipRateTierDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IShipRateTier"/>.
        /// </returns>
        public IShipRateTier BuildEntity(ShipRateTierDto dto)
        {
            var entity = new ShipRateTier(dto.ShipMethodKey)
                {
                    Key = dto.Key,
                    RangeLow = dto.RangeLow,
                    RangeHigh = dto.RangeHigh,
                    Rate = dto.Rate,
                    UpdateDate = dto.UpdateDate,
                    CreateDate = dto.CreateDate
                };

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <summary>
        /// Builds <see cref="ShipRateTierDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IShipRateTier"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ShipRateTierDto"/>.
        /// </returns>
        public ShipRateTierDto BuildDto(IShipRateTier entity)
        {
            return new ShipRateTierDto()
                {
                    Key = entity.Key,
                    ShipMethodKey = entity.ShipMethodKey,
                    RangeLow = entity.RangeLow,
                    RangeHigh = entity.RangeHigh,
                    Rate = entity.Rate,
                    UpdateDate = entity.UpdateDate,
                    CreateDate = entity.CreateDate
                };
        }
    }
}