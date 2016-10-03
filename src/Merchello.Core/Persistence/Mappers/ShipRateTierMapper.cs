namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ShipRateTier"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ShipRateTier))]
    [MapperFor(typeof(IShipRateTier))]
    internal  sealed class ShipRateTierMapper : BaseMapper
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

            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.Key, dto => dto.Key);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.ShipMethodKey, dto => dto.ShipMethodKey);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.RangeLow, dto => dto.RangeLow);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.RangeHigh, dto => dto.RangeHigh);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.Rate, dto => dto.Rate);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<ShipRateTier, ShipRateTierDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}