namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ProductAttribute"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ProductAttribute))]
    [MapperFor(typeof(IProductAttribute))]
    internal sealed class ProductAttributeMapper : BaseMapper
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
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.Key, dto => dto.Key);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.OptionKey, dto => dto.OptionKey);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.Name, dto => dto.Name);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.Sku, dto => dto.Sku);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.SortOrder, dto => dto.SortOrder);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.IsDefaultChoice, dto => dto.IsDefaultChoice);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<ProductAttribute, ProductAttributeDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}