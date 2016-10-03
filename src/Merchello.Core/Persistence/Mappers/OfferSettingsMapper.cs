namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="OfferSettings"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(OfferSettings))]
    [MapperFor(typeof(IOfferSettings))]
    internal sealed class OfferSettingsMapper : BaseMapper
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

            CacheMap<OfferSettings, OfferSettingsDto>(src => src.Key, dto => dto.Key);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.Name, dto => dto.Name);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.OfferCode, dto => dto.OfferCode);            
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.OfferProviderKey, dto => dto.OfferProviderKey);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.OfferStartsDate, dto => dto.OfferStartsDate);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.OfferEndsDate, dto => dto.OfferEndsDate);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.Active, dto => dto.Active);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<OfferSettings, OfferSettingsDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}