namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ProductOption"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ProductOption))]
    [MapperFor(typeof(IProductOption))]
    internal sealed class ProductOptionMapper : BaseMapper
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

            CacheMap<ProductOption, ProductOptionDto>(src => src.Key, dto => dto.Key);
            CacheMap<ProductOption, ProductOptionDto>(src => src.Name, dto => dto.Name);
            CacheMap<ProductOption, ProductOptionDto>(src => src.DetachedContentTypeKey, dto => dto.DetachedContentTypeKey);
            CacheMap<ProductOption, ProductOptionDto>(src => src.Required, dto => dto.Required);
            CacheMap<ProductOption, ProductOptionDto>(src => src.Shared, dto => dto.Shared);
            CacheMap<ProductOption, ProductOptionDto>(src => src.UiOption, dto => dto.UiOption);
            CacheMap<ProductOption, ProductOptionDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<ProductOption, ProductOptionDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}