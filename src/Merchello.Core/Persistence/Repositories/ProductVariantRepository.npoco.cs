namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IProductVariantRepository
    {
        /// <inheritdoc/>
        protected override IProductVariant PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false)
               .Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.Fetch<ProductDto>(sql).FirstOrDefault();

            if (dto == null || dto.ProductVariantDto == null)
                return null;

            var factory = new ProductVariantFactory(_productOptionRepository.GetProductAttributeCollectionForVariant, GetCategoryInventoryCollection, GetDetachedContentCollection);
            var variant = factory.BuildEntity(dto.ProductVariantDto);

            variant.ResetDirtyProperties();

            return variant;
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProductVariant> PerformGetAll(params Guid[] keys)
        {
            var factory = new ProductVariantFactory(_productOptionRepository.GetProductAttributeCollectionForVariant, GetCategoryInventoryCollection, GetDetachedContentCollection);

            if (keys.Any())
            {
                return Database.Fetch<ProductDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => factory.BuildEntity(x.ProductVariantDto));
            }

            return Database.Fetch<ProductDto>().Select(x => factory.BuildEntity(x.ProductVariantDto));
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProductVariant> PerformGetByQuery(IQuery<IProductVariant> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<IProductVariant>(sqlClause, query);
            var sql = translator.Translate();

            var factory = new ProductVariantFactory(_productOptionRepository.GetProductAttributeCollectionForVariant, GetCategoryInventoryCollection, GetDetachedContentCollection);
            var dtos = Database.Fetch<ProductDto>(sql);
            return dtos.Select(x => factory.BuildEntity(x.ProductVariantDto));
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IProductVariant entity)
        {
            if (!MandateProductVariantRules(entity)) return;

            if (!SkuExists(entity.Sku)) throw new InvalidOperationException("The SKU must be unique.");

            ((Entity)entity).AddingEntity();

            ((ProductVariant)entity).VersionKey = Guid.NewGuid();

            var factory = new ProductVariantFactory(
                pa => ((ProductVariant)entity).ProductAttributes,
                ci => ((ProductVariant)entity).CatalogInventoryCollection,
                dc => ((ProductVariant)entity).DetachedContents);

            var dto = factory.BuildDto(entity);

            // insert the variant
            Database.Insert(dto);
            entity.Key = dto.Key; // to set HasIdentity

            Database.Insert(dto.ProductVariantIndexDto);
            ((ProductVariant)entity).ExamineId = dto.ProductVariantIndexDto.Id;

            // insert associations for every attribute
            foreach (var association in entity.Attributes.Select(att => new ProductVariant2ProductAttributeDto()
            {
                ProductVariantKey = entity.Key,
                OptionKey = att.OptionKey,
                ProductAttributeKey = att.Key,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.Now
            }))
            {
                Database.Insert(association);
            }

            SaveCatalogInventory(entity);

            SaveDetachedContents(entity);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IProductVariant entity)
        {
            if (!MandateProductVariantRules(entity)) return;
     
            if (!SkuExists(entity.Sku, entity.Key)) throw new InvalidOperationException("Entity cannot be updated.  The sku already exists.");

            ((Entity)entity).UpdatingEntity();
            ((ProductVariant)entity).VersionKey = Guid.NewGuid();

            var factory = new ProductVariantFactory(
                pa => ((ProductVariant)entity).ProductAttributes,
                ci => ((ProductVariant)entity).CatalogInventoryCollection,
                dc => ((ProductVariant)entity).DetachedContents);

            var dto = factory.BuildDto(entity);

            // update the variant
            Database.Update(dto);

            SaveCatalogInventory(entity);

            SaveDetachedContents(entity);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            var sql = Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ProductDto>()
                .InnerJoin<ProductVariantDto>()
                .On<ProductDto, ProductVariantDto>(left => left.Key, right => right.ProductKey)
                .InnerJoin<ProductVariantIndexDto>()
                .On<ProductVariantDto, ProductVariantIndexDto>(left => left.Key, right => right.ProductVariantKey);

            return sql;
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchProductVariant.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchCatalogInventory WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariantDetachedContent WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariantIndex WHERE productVariantKey = @Key",
                "DELETE FROM merchProductVariant WHERE pk = @Key"
            };

            return list;
        }
    }
}
