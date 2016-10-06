namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IProductVariantRepository
    {
        /// <inheritdoc/>
        public IEnumerable<IProductVariant> GetByWarehouseKey(Guid warehouseKey)
        {
            var sql = Sql().SelectAll()
                .From<CatalogInventoryDto>()
                .InnerJoin<WarehouseCatalogDto>()
                .On<CatalogInventoryDto, WarehouseCatalogDto>(left => left.CatalogKey, right => right.Key)
                .Where<WarehouseCatalogDto>(x => x.WarehouseKey == warehouseKey);

            var dtos = Database.Fetch<CatalogInventoryDto>(sql);

            return GetAll(dtos.DistinctBy(dto => dto.ProductVariantKey).Select(x => x.ProductVariantKey).ToArray());
        }


        /// <inheritdoc/>
        public CatalogInventoryCollection GetCategoryInventoryCollection(Guid productVariantKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void SaveCatalogInventory(IProductVariant productVariant)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Associates a <see cref="IProductVariant"/> with a catalog inventory.
        /// </summary>
        /// <param name="productVariant">
        /// The product variant.
        /// </param>
        /// <param name="inv">
        /// The <see cref="ICatalogInventory"/>.
        /// </param>
        private void AddCatalogInventory(IProductVariant productVariant, ICatalogInventory inv)
        {
            inv.CreateDate = DateTime.Now;
            inv.UpdateDate = DateTime.Now;

            var dto = new CatalogInventoryDto()
            {
                CatalogKey = inv.CatalogKey,
                ProductVariantKey = productVariant.Key,
                Count = inv.Count,
                LowCount = inv.LowCount,
                Location = inv.Location,
                CreateDate = inv.CreateDate,
                UpdateDate = inv.UpdateDate
            };

            Database.Insert(dto);
        }

        /// <summary>
        /// Updates catalog inventory.
        /// </summary>
        /// <param name="inv">
        /// The <see cref="ICatalogInventory"/>.
        /// </param>
        private void UpdateCatalogInventory(ICatalogInventory inv)
        {
            inv.UpdateDate = DateTime.Now;


            Database.Execute(
                "UPDATE [merchCatalogInventory] SET [count] = @invCount, [lowCount] = @invLowCount, [location] = @invLocation, [updateDate] = @invUpdateDate WHERE [catalogKey] = @catalogKey AND [productVariantKey] = @productVariantKey",
                new
                {
                    invCount = inv.Count,
                    invLowCount = inv.LowCount,
                    invLocation = inv.Location,
                    invUpdateDate = inv.UpdateDate,
                    catalogKey = inv.CatalogKey,
                    productVariantKey = inv.ProductVariantKey
                });
        }

        /// <summary>
        /// Deletes catalog inventory.
        /// </summary>
        /// <param name="productVariantKey">
        /// The product variant key.
        /// </param>
        /// <param name="catalogKey">
        /// The catalog key.
        /// </param>
        private void DeleteCatalogInventory(Guid productVariantKey, Guid catalogKey)
        {
            const string Sql = "DELETE FROM [merchCatalogInventory] WHERE [productVariantKey] = @pvKey AND catalogKey = @cKey";

            Database.Execute(Sql, new { @pvKey = productVariantKey, @cKey = catalogKey });
        }
    }
}
