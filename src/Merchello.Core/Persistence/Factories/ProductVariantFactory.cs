namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;

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
        /// The invalid operation exception.
        /// </summary>
        private readonly InvalidOperationException _invalidOperation =
            new InvalidOperationException(
                "Build entity cannot be called when using this empty constructor which is intended ONLY for Bulk imports");

        /// <summary>
        /// The flag set when factory is instantiated for bulk imports.
        /// </summary>
        private bool _isBulkImport;

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
        /// Initializes a new instance of the <see cref="ProductVariantFactory"/> class.
        /// </summary>
        /// <remarks>
        /// Only used in bulk imports
        /// </remarks>
        internal ProductVariantFactory()
        {
            _isBulkImport = true;
        }

        /// <summary>
        /// The build entity.
        /// </summary>
        /// <param name="dto">
        /// The dto.
        /// </param>
        /// <returns>
        /// The <see cref="IProductVariant"/>.
        /// </returns>
        public IProductVariant BuildEntity(ProductVariantDto dto)
        {
            if (_isBulkImport) throw _invalidOperation;

            var entity = new ProductVariant(dto.Name, dto.Sku, dto.Price)
            {
                Key = dto.Key,
                ProductKey = dto.ProductKey,
                CostOfGoods = dto.CostOfGoods,
                SalePrice = dto.SalePrice,
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
        /// The build dto.
        /// </summary>
        /// <param name="entity">
        /// The entity.
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
                Price = entity.Price,
                CostOfGoods = entity.CostOfGoods,
                SalePrice = entity.SalePrice,
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