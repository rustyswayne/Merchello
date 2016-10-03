namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ShipMethod"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ShipMethod))]
    [MapperFor(typeof(IShipMethod))]
    internal sealed class ShipMethodMapper : BaseMapper
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

            CacheMap<ShipMethod, ShipMethodDto>(src => src.Key, dto => dto.Key);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.Name, dto => dto.Name);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.ProviderKey, dto => dto.ProviderKey);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.ShipCountryKey, dto => dto.ShipCountryKey);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.Surcharge, dto => dto.Surcharge);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.ServiceCode, dto => dto.ServiceCode);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.Taxable, dto => dto.Taxable);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<ShipMethod, ShipMethodDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}