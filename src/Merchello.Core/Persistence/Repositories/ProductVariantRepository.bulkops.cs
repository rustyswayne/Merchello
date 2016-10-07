namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : NPocoBulkOperationRepositoryBase<IProductVariant, ProductVariantDto, ProductVariantFactory>
    {
        /// <inheritdoc/>
        public override void PersistNewItems(IEnumerable<IProductVariant> entities)
        {
            var productVariants = entities as IProductVariant[] ?? entities.ToArray();

            if (!MandateProductVariantRules(productVariants)) return;

            //// Mandate.ParameterCondition(!SkuExists(entity.Sku), "The sku must be unique");
            var dtos = new List<ProductVariantDto>();
            foreach (var entity in productVariants)
            {
                ((ProductBase)entity).AddingEntity();
                var factory = new ProductVariantFactory();
                dtos.Add(factory.BuildDto(entity));
            }

            // insert the variants
            Database.BulkInsertRecords<ProductVariantDto>(SqlSyntax, dtos);
            Database.BulkInsertRecords<ProductVariantIndexDto>(SqlSyntax, dtos.Select(v => v.ProductVariantIndexDto));

            // We have to look up the examine ids
            var idDtos = Database.Fetch<ProductVariantIndexDto>("WHERE productVariantKey IN (@pvkeys)", new { @pvkeys = dtos.Select(x => x.Key) });

            foreach (var entity in productVariants)
            {
                var dto = dtos.FirstOrDefault(d => d.VersionKey == entity.VersionKey);
                // ReSharper disable once PossibleNullReferenceException
                entity.Key = dto.Key; // to set HasIdentity

                var productVariantIndexDto = idDtos.FirstOrDefault(id => id.ProductVariantKey == dto.Key);
                if (productVariantIndexDto != null)
                {
                    ((ProductVariant)entity).ExamineId = productVariantIndexDto.Id;
                }

                foreach (var inv in entity.CatalogInventories)
                {
                    ((CatalogInventory)inv).ProductVariantKey = entity.Key;
                }
            }

            var xrefs = new List<ProductVariant2ProductAttributeDto>();

            foreach (var v in productVariants.ToArray())
            {
                var associations = v.Attributes.Select(x =>
                    new ProductVariant2ProductAttributeDto
                    {
                        ProductVariantKey = v.Key,
                        OptionKey = x.OptionKey,
                        ProductAttributeKey = x.Key,
                        UpdateDate = DateTime.Now,
                        CreateDate = DateTime.Now
                    });

                xrefs.AddRange(associations);
            }

            Database.BulkInsertRecords<ProductVariant2ProductAttributeDto>(SqlSyntax, xrefs);

            SaveCatalogInventory(productVariants);

            SaveDetachedContents(productVariants);

            foreach (var entity in productVariants)
            {
                entity.ResetDirtyProperties();
            }
        }

        /// <inheritdoc/>
        public override void PersistUpdatedItems(IEnumerable<IProductVariant> entities)
        {
            var productVariants = entities as IProductVariant[] ?? entities.ToArray();
            if (!MandateProductVariantRules(productVariants)) return;

            if (!SkuExists(productVariants)) throw new InvalidOperationException("Entity cannot be updated.  The sku already exists.");

            ExecuteBatchUpdate(productVariants);

            SaveCatalogInventory(productVariants);

            SaveDetachedContents(productVariants);

            foreach (var entity in productVariants)
            {
                entity.ResetDirtyProperties();
            }
        }

        /// <inheritdoc/>
        protected override void ApplyAddingOrUpdating(RecordPersistenceType transactionType, IProductVariant entity)
        {
            if (transactionType == RecordPersistenceType.Insert)
            {
                ((ProductBase)entity).AddingEntity();
            }
            else
            {
                ((ProductBase)entity).UpdatingEntity();
            }
        }
    }
}
