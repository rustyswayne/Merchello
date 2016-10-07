namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductRepository : NPocoRepositoryBase<IProduct>
    {
        /// <inheritdoc/>
        protected override IProduct PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false)
                .Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.Fetch<ProductDto>(sql).FirstOrDefault();

            if (dto == null)
                return null;

            var factory = new ProductFactory(
                _productOptionRepository.GetProductAttributeCollectionForVariant,
                _productVariantRepository.GetCategoryInventoryCollection,
                _productOptionRepository.GetProductOptionCollection,
                _productVariantRepository.GetProductVariantCollection,
                _productVariantRepository.GetDetachedContentCollection);

            var product = factory.BuildEntity(dto);


            product.ResetDirtyProperties();

            return product;
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProduct> PerformGetAll(params Guid[] keys)
        {
            if (keys.Any())
            {
                return Database.Fetch<ProductDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => Get(x.Key));
            }

            return Database.Fetch<ProductDto>().Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        /// FYI this should not be used since we can't map to the ProductVariantDto via the Model Mapper
        protected override IEnumerable<IProduct> PerformGetByQuery(IQuery<IProduct> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<IProduct>(sqlClause, query);
            var sql = translator.Translate();

            var dtos = Database.Fetch<ProductDto>(sql);

            return GetAll(dtos.Select(x => x.Key).ToArray());
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IProduct entity)
        {
            if (!SkuExists(entity.Sku)) throw new InvalidOperationException("Sku must be unique.");

            ((Product)entity).AddingEntity();
            ((ProductVariant)((Product)entity).MasterVariant).VersionKey = Guid.NewGuid();

            var factory = new ProductFactory();
            var dto = factory.BuildDto(entity);

            // save the product
            Database.Insert(dto);
            entity.Key = dto.Key;

            // setup and save the master (singular) variant
            dto.ProductVariantDto.ProductKey = dto.Key;
            Database.Insert(dto.ProductVariantDto);
            Database.Insert(dto.ProductVariantDto.ProductVariantIndexDto);

            ((Product)entity).MasterVariant.ProductKey = dto.ProductVariantDto.ProductKey;
            ((Product)entity).MasterVariant.Key = dto.ProductVariantDto.Key;
            ((ProductVariant)((Product)entity).MasterVariant).ExamineId = dto.ProductVariantDto.ProductVariantIndexDto.Id;

            // save the product options
            _productOptionRepository.SaveForProduct(entity);

            // synchronize the inventory
            _productVariantRepository.SaveCatalogInventory(((Product)entity).MasterVariant);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IProduct entity)
        {
            var cachedKeys = entity.ProductVariants.Select(x => x.Key).ToList();
            cachedKeys.Add(((ProductVariant)((Product)entity).MasterVariant).Key);

            ((Product)entity).UpdatingEntity();
            ((ProductVariant)((Product)entity).MasterVariant).VersionKey = Guid.NewGuid();

            var factory = new ProductFactory();
            var dto = factory.BuildDto(entity);

            Database.Update(dto);
            Database.Update(dto.ProductVariantDto);

            _productOptionRepository.SaveForProduct(entity);

            // synchronize the inventory
            _productVariantRepository.SaveCatalogInventory(((Product)entity).MasterVariant);

            _productVariantRepository.SaveDetachedContents(((Product)entity).MasterVariant);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(IProduct entity)
        {
            _productOptionRepository.DeleteAllProductOptions(entity);

            base.PersistDeletedItem(entity);
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            var sql = Sql().Select(isCount ? "COUNT(*)" : "*")
               .From<ProductDto>()
               .InnerJoin<ProductVariantDto>()
               .On<ProductDto, ProductVariantDto>(left => left.Key, right => right.ProductKey)
               .InnerJoin<ProductVariantIndexDto>()
               .On<ProductVariantDto, ProductVariantIndexDto>(left => left.Key, right => right.ProductVariantKey)
               .Where<ProductVariantDto>(x => x.Master);

            return sql;
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchProduct.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchCatalogInventory WHERE productVariantKey IN (SELECT pk FROM merchProductVariant WHERE productKey = @Key)",
                    "DELETE FROM merchProductVariantDetachedContent WHERE productVariantKey IN (SELECT pk FROM merchProductVariant WHERE productKey = @Key)",
                    "DELETE FROM merchProductVariantIndex WHERE productVariantKey IN (SELECT pk FROM merchProductVariant WHERE productKey = @Key)",
                    "DELETE FROM merchProductVariant WHERE productKey = @Key",
                    "DELETE FROM merchProduct2EntityCollection WHERE productKey = @Key",
                    "DELETE FROM merchProduct WHERE pk = @Key"
                };

            return list;
        }
    }
}
