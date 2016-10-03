namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ProductVariantDetachedContent"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ProductVariantDetachedContent))]
    [MapperFor(typeof(IProductVariantDetachedContent))]
    internal sealed class ProductVariantDetachedContentMapper : BaseMapper
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

            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.Key, dto => dto.Key);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.ProductVariantKey, dto => dto.ProductVariantKey);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.CultureName, dto => dto.CultureName);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.TemplateId, dto => dto.TemplateId);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.Slug, dto => dto.Slug);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.CanBeRendered, dto => dto.CanBeRendered);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<ProductVariantDetachedContent, ProductVariantDetachedContentDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}