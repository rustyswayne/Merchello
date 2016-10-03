namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="TaxMethod"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(TaxMethod))]
    [MapperFor(typeof(ITaxMethod))]
    internal sealed class TaxMethodMapper : BaseMapper
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

             CacheMap<TaxMethod, TaxMethodDto>(src => src.Key, dto => dto.Key);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.ProviderKey, dto => dto.ProviderKey);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.Name, dto => dto.Name);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.CountryCode, dto => dto.CountryCode);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.PercentageTaxRate, dto => dto.PercentageTaxRate);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.ProductTaxMethod, dto => dto.ProductTaxMethod);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.UpdateDate, dto => dto.UpdateDate);
             CacheMap<TaxMethod, TaxMethodDto>(src => src.CreateDate, dto => dto.CreateDate);
         }
    }
}