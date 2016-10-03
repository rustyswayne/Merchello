namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="OfferRedeemed"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(OfferRedeemed))]
    [MapperFor(typeof(IOfferRedeemed))]
    internal sealed class OfferRedeemedMapper : BaseMapper
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
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.Key, dto => dto.Key);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.OfferSettingsKey, dto => dto.OfferSettingsKey);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.OfferCode, dto => dto.OfferCode);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.OfferProviderKey, dto => dto.OfferProviderKey);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.CustomerKey, dto => dto.CustomerKey);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.InvoiceKey, dto => dto.InvoiceKey);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.RedeemedDate, dto => dto.RedeemedDate);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<OfferRedeemed, OfferRedeemedDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}