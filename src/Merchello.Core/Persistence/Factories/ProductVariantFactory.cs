﻿namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;

    using NodaMoney;

    /// <summary>
    /// A class responsible for building ProductVariant entities and DTO objects.
    /// </summary>
    internal class ProductVariantFactory : IEntityFactory<IProductVariant, ProductVariantDto>
    {
        /// <summary>
        /// The <see cref="ProductAttributeCollection"/>.
        /// </summary>
        private readonly Func<Guid, ProductAttributeCollection> _productAttributeCollection;

        /// <summary>
        /// The <see cref="CatalogInventoryCollection"/>.
        /// </summary>
        private readonly Func<Guid, CatalogInventoryCollection> _catalogInventories;

        /// <summary>
        /// The <see cref="DetachedContentCollection{IProductVariantDetachedContent}"/>.
        /// </summary>
        private readonly Func<Guid, DetachedContentCollection<IProductVariantDetachedContent>> _detachedContentCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantFactory"/> class.
        /// </summary>
        public ProductVariantFactory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantFactory"/> class.
        /// </summary>
        /// <param name="productAttributes">
        /// The product attributes.
        /// </param>
        /// <param name="catalogInventories">
        /// The catalog inventories.
        /// </param>
        /// <param name="detachedContentCollection">
        /// The <see cref="DetachedContentCollection{IProductVariantDetachedContent}"/>
        /// </param>
        public ProductVariantFactory(
            Func<Guid, ProductAttributeCollection> productAttributes,
            Func<Guid, CatalogInventoryCollection> catalogInventories,
            Func<Guid, DetachedContentCollection<IProductVariantDetachedContent>> detachedContentCollection)
        {
            _productAttributeCollection = productAttributes;
            _catalogInventories = catalogInventories;
            _detachedContentCollection = detachedContentCollection;
        }

        /// <summary>
        /// Builds <see cref="IProductVariant"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="ProductVariantDto"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IProductVariant"/>.
        /// </returns>
        public IProductVariant BuildEntity(ProductVariantDto dto)
        {
            var entity = new ProductVariant(dto.Name, dto.Sku, new Money(dto.Price))
            {
                Key = dto.Key,
                ProductKey = dto.ProductKey,
                CostOfGoods = dto.CostOfGoods != null ? new Money(dto.CostOfGoods.Value) : (Money?)null,
                SalePrice = dto.SalePrice != null ? new Money(dto.SalePrice.Value) : (Money?)null,
                OnSale = dto.OnSale,
                Manufacturer = dto.Manufacturer,
                ManufacturerModelNumber = dto.ManufacturerModelNumber,
                Weight = dto.Weight,
                Length = dto.Length,
                Height = dto.Height,
                Width = dto.Width,
                Barcode = dto.Barcode,
                Available = dto.Available,
                TrackInventory = dto.TrackInventory,
                OutOfStockPurchase = dto.OutOfStockPurchase,
                Taxable = dto.Taxable,
                Shippable = dto.Shippable,
                Download = dto.Download,
                DownloadMediaId = dto.DownloadMediaId,
                Master = dto.Master,
                ExamineId = dto.ProductVariantIndexDto.Id, 
                CatalogInventoryCollection = _catalogInventories.Invoke(dto.Key),
                ProductAttributes = _productAttributeCollection.Invoke(dto.Key),
                DetachedContents = _detachedContentCollection.Invoke(dto.Key),
                VersionKey = dto.VersionKey,
                UpdateDate = dto.UpdateDate,
                CreateDate = dto.CreateDate
            };

            entity.ResetDirtyProperties();
            return entity;
        }

        /// <summary>
        /// Builds <see cref="ProductVariantDto"/>.
        /// </summary>
        /// <param name="entity">
        /// The <see cref="IProductVariant"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ProductVariantDto"/>.
        /// </returns>
        public ProductVariantDto BuildDto(IProductVariant entity)
        {
            return new ProductVariantDto()
            {
                Key = entity.Key,
                ProductKey = entity.ProductKey,
                Name = entity.Name,
                Sku = entity.Sku,
                Price = entity.Price.Amount,
                CostOfGoods = entity.CostOfGoods.Amount(),
                SalePrice = entity.SalePrice.Amount(),
                Manufacturer = entity.Manufacturer,
                ManufacturerModelNumber = entity.ManufacturerModelNumber,
                OnSale = entity.OnSale,
                Weight = entity.Weight,
                Length = entity.Length,
                Height = entity.Height,
                Width = entity.Width,
                Barcode = entity.Barcode,
                Available = entity.Available,
                TrackInventory = entity.TrackInventory,
                OutOfStockPurchase = entity.OutOfStockPurchase,
                Taxable = entity.Taxable,
                Shippable = entity.Shippable,
                Download = entity.Download,
                DownloadMediaId = entity.DownloadMediaId,
                Master = ((ProductVariant)entity).Master,
                ProductVariantIndexDto = new ProductVariantIndexDto()
                    {
                      Id = ((ProductVariant)entity).ExamineId,
                      ProductVariantKey = entity.Key,
                      UpdateDate = entity.UpdateDate,
                      CreateDate = entity.CreateDate
                    },
                VersionKey = entity.VersionKey,
                UpdateDate = entity.UpdateDate,
                CreateDate = entity.CreateDate
            };
        }
    }
}