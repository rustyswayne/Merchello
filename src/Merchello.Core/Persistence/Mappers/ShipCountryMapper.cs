namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ShipCountry"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ShipCountry))]
    [MapperFor(typeof(IShipCountry))]
    internal sealed class ShipCountryMapper : BaseMapper
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

            CacheMap<ShipCountry, ShipCountryDto>(src => src.Key, dto => dto.Key);
            CacheMap<ShipCountry, ShipCountryDto>(src => src.CatalogKey, dto => dto.CatalogKey);
            CacheMap<ShipCountry, ShipCountryDto>(src => src.CountryCode, dto => dto.CountryCode);
            CacheMap<ShipCountry, ShipCountryDto>(src => src.Name, dto => dto.Name);
            CacheMap<ShipCountry, ShipCountryDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<ShipCountry, ShipCountryDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}