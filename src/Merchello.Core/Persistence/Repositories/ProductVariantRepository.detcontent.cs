namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IProductVariantRepository
    {
        /// <inheritdoc/>
        public DetachedContentCollection<IProductVariantDetachedContent> GetDetachedContentCollection(Guid productVariantKey)
        {
            var contents = this.GetProductVariantDetachedContents(productVariantKey);
            return new DetachedContentCollection<IProductVariantDetachedContent> { contents };
        }

        /// <inheritdoc/>
        public void SaveDetachedContents(IProductVariant productVariant)
        {
            var existing = this.GetDetachedContentCollection(productVariant.Key).ToArray();

            if (!productVariant.DetachedContents.Any() && !existing.Any()) return;

            if (!productVariant.DetachedContents.Any())
            {
                foreach (var dc in existing)
                {
                    this.DeleteDetachedContent(dc);
                }

                return;
            }

            foreach (var exist in existing.Where(x => !productVariant.DetachedContents.Contains(x.CultureName)))
            {
                this.DeleteDetachedContent(exist);
            }

            foreach (var dc in productVariant.DetachedContents)
            {
                var slug = PathHelper.ConvertToSlug(productVariant.Name);
                this.SaveDetachedContent(dc, slug);
            }
        }

        /// <summary>
        /// Gets detached content associated with the product variant.
        /// </summary>
        /// <param name="productVariantKey">
        /// The product variant key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProductVariantDetachedContent}"/>.
        /// </returns>
        internal IEnumerable<IProductVariantDetachedContent> GetProductVariantDetachedContents(Guid productVariantKey)
        {
            var sql =
                Sql()
                    .SelectAll()
                    .From<ProductVariantDetachedContentDto>()
                    .InnerJoin<DetachedContentTypeDto>()
                    .On<ProductVariantDetachedContentDto, DetachedContentTypeDto>(
                        left => left.DetachedContentTypeKey,
                        right => right.Key)
                    .Where<ProductVariantDetachedContentDto>(x => x.Key == productVariantKey);

            var dtos = Database.Fetch<ProductVariantDetachedContentDto>(sql);

            var factory = new ProductVariantDetachedContentFactory();

            return dtos.Select(factory.BuildEntity);
        }

        /// <summary>
        /// Gets detached content associated with the product variant.
        /// </summary>
        /// <param name="productVariantKeys">
        /// The product variant keys.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProductVariantDetachedContent}"/>.
        /// </returns>
        internal IEnumerable<IProductVariantDetachedContent> GetProductVariantDetachedContents(IEnumerable<Guid> productVariantKeys)
        {
            var sql = Sql().SelectAll()
                .From<ProductVariantDetachedContentDto>()
                .InnerJoin<DetachedContentTypeDto>()
                .On<ProductVariantDetachedContentDto, DetachedContentTypeDto>(
                    left => left.DetachedContentTypeKey,
                    right => right.Key)
                .WhereIn<ProductVariantDetachedContentDto>(x => x.ProductVariantKey, productVariantKeys);

            var dtos = Database.Fetch<ProductVariantDetachedContentDto>(sql);

            var factory = new ProductVariantDetachedContentFactory();

            return dtos.Select(factory.BuildEntity);
        }

        /// <summary>
        /// The save detached content.
        /// </summary>
        /// <param name="detachedContent">
        /// The detached content.
        /// </param>
        /// <param name="slug">
        /// The generated slug
        /// </param>
        internal void SaveDetachedContent(IProductVariantDetachedContent detachedContent, string slug)
        {
            var factory = new ProductVariantDetachedContentFactory();

            if (!detachedContent.HasIdentity)
            {
                ((Entity)detachedContent).AddingEntity();
                detachedContent.Slug = this.EnsureSlug(detachedContent, slug);
                var dto = factory.BuildDto(detachedContent);
                Database.Insert(dto);
                detachedContent.Key = dto.Key;
            }
            else
            {
                ((Entity)detachedContent).UpdatingEntity();
                detachedContent.Slug = this.EnsureSlug(detachedContent, detachedContent.Slug);
                var dto = factory.BuildDto(detachedContent);

                const string Update = "UPDATE [merchProductVariantDetachedContent] SET [merchProductVariantDetachedContent].[detachedContentTypeKey] = @Dctk, [merchProductVariantDetachedContent].[templateId] = @Tid, [merchProductVariantDetachedContent].[slug] = @Slug, [merchProductVariantDetachedContent].[values] = @Vals, [merchProductVariantDetachedContent].[canBeRendered] = @Cbr, [merchProductVariantDetachedContent].[updateDate] = @Ud WHERE [merchProductVariantDetachedContent].[cultureName] = @Cn AND [merchProductVariantDetachedContent].[productVariantKey] = @Pvk";

                Database.Execute(
                    Update,
                    new
                    {
                        @Dctk = dto.DetachedContentTypeKey,
                        @Tid = dto.TemplateId,
                        @Slug = dto.Slug,
                        @Vals = dto.Values,
                        @Cbr = dto.CanBeRendered,
                        @Ud = dto.UpdateDate,
                        @Cn = dto.CultureName,
                        @Pvk = dto.ProductVariantKey
                    });
            }
        }

        /// <summary>
        /// Bulk save detached contents.
        /// </summary>
        /// <param name="productVariants">
        /// The product variants.
        /// </param>
        internal void SaveDetachedContents(IEnumerable<IProductVariant> productVariants)
        {
            var variants = productVariants as IProductVariant[] ?? productVariants.ToArray();
            var factory = new ProductVariantDetachedContentFactory();
            var existing = this.GetProductVariantDetachedContents(variants.Select(x => x.Key)).ToArray();

            var sqlStatement = string.Empty;

            var parms = new List<object>();
            var paramIndex = 0;
            var inserts = new List<ProductVariantDetachedContentDto>();

            foreach (var variant in variants)
            {
                if (variant.DetachedContents.Any() || existing.Any(x => x.ProductVariantKey == variant.Key))
                {
                    if (existing.Any(x => x.ProductVariantKey == variant.Key) && !variant.DetachedContents.Any())
                    {
                        sqlStatement += string.Format(" DELETE [merchProductVariantDetachedContent] WHERE [productVariantKey] = @{0}", paramIndex++);
                    }

                    var slug = PathHelper.ConvertToSlug(variant.Name);
                    foreach (var dc in variant.DetachedContents)
                    {
                        if (!dc.HasIdentity)
                        {
                            ((Entity)dc).AddingEntity();
                            dc.Slug = this.EnsureSlug(dc, slug);
                            var dto = factory.BuildDto(dc);
                            inserts.Add(dto);
                        }
                        else
                        {
                            ((Entity)dc).UpdatingEntity();
                            var dto = factory.BuildDto(dc);
                            sqlStatement +=
                                string.Format(
                                    " UPDATE [merchProductVariantDetachedContent] SET [merchProductVariantDetachedContent].[detachedContentTypeKey] = @{0}, [merchProductVariantDetachedContent].[templateId] = @{1}, [merchProductVariantDetachedContent].[slug] = @{2}, [merchProductVariantDetachedContent].[values] = @{3}, [merchProductVariantDetachedContent].[canBeRendered] = @{4}, [merchProductVariantDetachedContent].[updateDate] = @{5} WHERE [merchProductVariantDetachedContent].[cultureName] = @{6} AND [merchProductVariantDetachedContent].[productVariantKey] = @{7}",
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++,
                                    paramIndex++);

                            parms.Add(dto.DetachedContentTypeKey);
                            parms.Add(dto.TemplateId);
                            parms.Add(dto.Slug);
                            parms.Add(dto.Values);
                            parms.Add(dto.CanBeRendered);
                            parms.Add(dto.UpdateDate);
                            parms.Add(dto.CultureName);
                            parms.Add(dto.ProductVariantKey);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(sqlStatement))
            {
                Database.Execute(sqlStatement, parms.ToArray());
            }

            if (inserts.Any())
            {
                // .BulkInsertRecordsWithKey<ProductVariantDetachedContentDto>(inserts)
                this.Database.BulkInsertRecords<ProductVariantDetachedContentDto>(SqlSyntax, inserts);
            }
        }


        /// <summary>
        /// Deletes <see cref="IProductVariantDetachedContent"/>.
        /// </summary>
        /// <param name="detachedContent">
        /// The <see cref="IProductVariantDetachedContent"/> to be deleted.
        /// </param>
        internal void DeleteDetachedContent(IProductVariantDetachedContent detachedContent)
        {
            Database.Execute(
                "DELETE [merchProductVariantDetachedContent] WHERE pk = @Key",
                new { @Key = detachedContent.Key });
        }

        /// <summary>
        /// Ensures the slug is valid.
        /// </summary>
        /// <param name="detachedContent">
        /// The detached content.
        /// </param>
        /// <param name="slug">
        /// The slug.
        /// </param>
        /// <returns>
        /// A slug incremented with a count if necessary.
        /// </returns>
        private string EnsureSlug(IProductVariantDetachedContent detachedContent, string slug)
        {
            if (slug == null) throw new ArgumentNullException(nameof(slug));

            var check = slug;

            var sql = Sql().SelectCount()
                            .From<ProductVariantDetachedContentDto>()
                            .Where<ProductVariantDetachedContentDto>(x => x.Slug == check && x.ProductVariantKey != detachedContent.ProductVariantKey);

            var count = Database.ExecuteScalar<int>(sql);
            if (count > 0) slug = $"{slug}-{count + 1}";
            return slug;
        }
    }
}
