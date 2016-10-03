﻿namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="ProductVariant"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(ProductVariant))]
    [MapperFor(typeof(IProductVariant))]
    internal sealed class ProductVariantMapper : BaseMapper
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

            CacheMap<ProductVariant, ProductVariantDto>(src => src.Key, dto => dto.Key);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.ProductKey, dto => dto.ProductKey);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Sku, dto => dto.Sku);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Name, dto => dto.Name);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Price, dto => dto.Price);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.CostOfGoods, dto => dto.CostOfGoods);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.SalePrice, dto => dto.SalePrice);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Manufacturer, dto => dto.Manufacturer);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.ManufacturerModelNumber, dto => dto.ManufacturerModelNumber);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.OnSale, dto => dto.OnSale);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Weight, dto => dto.Weight);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Length, dto => dto.Length);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Height, dto => dto.Height);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Width, dto => dto.Width);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Barcode, dto => dto.Barcode);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Available, dto => dto.Available);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.TrackInventory, dto => dto.TrackInventory);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.OutOfStockPurchase, dto => dto.OutOfStockPurchase);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Taxable, dto => dto.Taxable);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Shippable, dto => dto.Shippable);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Download, dto => dto.Download);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.DownloadMediaId, dto => dto.DownloadMediaId);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.Master, dto => dto.Master);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.VersionKey, dto => dto.VersionKey);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<ProductVariant, ProductVariantDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
