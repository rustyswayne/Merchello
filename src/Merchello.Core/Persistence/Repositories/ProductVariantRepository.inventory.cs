namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

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
            var sql = Sql().SelectAll()
               .From<CatalogInventoryDto>()
               .InnerJoin<WarehouseCatalogDto>()
               .On<CatalogInventoryDto, WarehouseCatalogDto>(left => left.CatalogKey, right => right.Key)
               .Where<CatalogInventoryDto>(x => x.ProductVariantKey == productVariantKey);

            var dtos = Database.Fetch<CatalogInventoryDto>(sql);

            var collection = new CatalogInventoryCollection();
            var factory = new CatalogInventoryFactory();

            foreach (var dto in dtos)
            {
                collection.Add(factory.BuildEntity(dto));
            }

            return collection;
        }

        /// <inheritdoc/>
        public void SaveCatalogInventory(IProductVariant productVariant)
        {
            var existing = GetCategoryInventoryCollection(productVariant.Key);

            foreach (var inv in existing.Where(inv => !((ProductVariant)productVariant).CatalogInventoryCollection.Contains(inv.CatalogKey)))
            {
                DeleteCatalogInventory(productVariant.Key, inv.CatalogKey);
            }

            foreach (var inv in productVariant.CatalogInventories.Where(inv => !existing.Contains(inv.CatalogKey)))
            {
                AddCatalogInventory(productVariant, inv);
            }

            foreach (var inv in productVariant.CatalogInventories.Where(x => existing.Contains(x.CatalogKey)))
            {
                UpdateCatalogInventory(inv);
            }
        }

        /// <summary>
        /// The save catalog inventory.
        /// </summary>
        /// <param name="productVariants">
        /// The product variants.
        /// </param>
        /// <remarks>
        /// This merely asserts that an association between the warehouse and the variant has been made
        /// </remarks>
        internal void SaveCatalogInventory(IProductVariant[] productVariants)
        {
            var keys = productVariants.Select(v => v.Key).ToArray();
            var sql = Sql().SelectAll()
                .From<CatalogInventoryDto>()
                .InnerJoin<WarehouseCatalogDto>()
                .On<CatalogInventoryDto, WarehouseCatalogDto>(left => left.CatalogKey, right => right.Key);

            if (keys.Any())
            {
                sql = sql.WhereIn<CatalogInventoryDto>(x => x.ProductVariantKey, keys);
            }

            var inventoryDtos = Database.Fetch<CatalogInventoryDto>(sql);

            var isSqlCe = Database.DatabaseType.IsSqlCe();

            string sqlStatement = string.Empty;
            int paramIndex = 0;
            var parms = new List<object>();
            var inserts = new List<CatalogInventoryDto>();

            foreach (var productVariant in productVariants)
            {
                foreach (var dto in inventoryDtos.Where(i => i.ProductVariantKey == productVariant.Key))
                {
                    if (!((ProductVariant)productVariant).CatalogInventoryCollection.Contains(dto.CatalogKey))
                    {
                        if (isSqlCe)
                        {
                            SqlCeDeleteCatalogInventory(dto.ProductVariantKey, dto.CatalogKey);
                        }
                        else
                        {
                            sqlStatement += string.Format(" DELETE FROM merchCatalogInventory WHERE productVariantKey = @{0} AND catalogKey = @{1}", paramIndex++, paramIndex++);
                            parms.Add(dto.ProductVariantKey);
                            parms.Add(dto.CatalogKey);
                        }

                    }
                }

                foreach (var inv in productVariant.CatalogInventories)
                {
                    inv.UpdateDate = DateTime.Now;
                    if (inventoryDtos.Any(i => i.ProductVariantKey == productVariant.Key && i.CatalogKey == inv.CatalogKey))
                    {
                        if (isSqlCe)
                        {
                            SqlCeUpdateCatalogInventory(inv);
                        }
                        else
                        {
                            sqlStatement += string.Format(" UPDATE merchCatalogInventory SET Count = @{0}, LowCount = @{1}, Location = @{2}, UpdateDate = @{3} WHERE catalogKey = @{4} AND productVariantKey = @{5}", paramIndex++, paramIndex++, paramIndex++, paramIndex++, paramIndex++, paramIndex++);
                            parms.Add(inv.Count);
                            parms.Add(inv.LowCount);
                            parms.Add(inv.Location);
                            parms.Add(inv.UpdateDate);
                            parms.Add(inv.CatalogKey);
                            parms.Add(inv.ProductVariantKey);
                        }
                    }
                    else
                    {
                        inv.CreateDate = DateTime.Now;
                        inv.UpdateDate = DateTime.Now;
                        inserts.Add(new CatalogInventoryDto()
                        {
                            CatalogKey = inv.CatalogKey,
                            ProductVariantKey = productVariant.Key,
                            Count = inv.Count,
                            LowCount = inv.LowCount,
                            Location = inv.Location,
                            CreateDate = inv.CreateDate,
                            UpdateDate = inv.UpdateDate
                        });
                    }
                }
            }

            if (!string.IsNullOrEmpty(sqlStatement))
            {
                Database.Execute(sqlStatement, parms.ToArray());
            }

            if (inserts.Any())
            {
                // Database.BulkInsertRecords won't work here because of the many to many and no pk.
                foreach (var ins in inserts) Database.Insert(ins);
            }
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
        /// Updates SQLCE database where bulk update is not available.
        /// </summary>
        /// <param name="inv">
        /// The inventory.
        /// </param>
        private void SqlCeUpdateCatalogInventory(ICatalogInventory inv)
        {
            var sql = Sql().Append(
                    "UPDATE merchCatalogInventory SET Count = @ct, LowCount = @lct, Location = @loc, UpdateDate = @ud WHERE catalogKey = @ck AND productVariantKey = @pvk",
                    new
                    {
                        @ct = inv.Count,
                        @lct = inv.LowCount,
                        @loc = inv.Location,
                        @ud = DateTime.Now,
                        @ck = inv.CatalogKey,
                        @pvk = inv.ProductVariantKey
                    });

            Database.Execute(sql);
        }

        /// <summary>
        /// Deletes catalog inventory where bulk operations are not available.
        /// </summary>
        /// <param name="productVariantKey">
        /// The product variant key.
        /// </param>
        /// <param name="catalogKey">
        /// The catalog key.
        /// </param>
        private void SqlCeDeleteCatalogInventory(Guid productVariantKey, Guid catalogKey)
        {
            var sql = Sql().Append("DELETE FROM merchCatalogInventory WHERE productVariantKey = @pvk AND catalogKey = @ck", new { @pvk = productVariantKey, @ck = catalogKey });
            Database.Execute(sql);
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
