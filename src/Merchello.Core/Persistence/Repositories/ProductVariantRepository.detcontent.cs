namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.DetachedContent;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IProductVariantRepository
    {
        /// <inheritdoc/>
        public DetachedContentCollection<IProductVariantDetachedContent> GetDetachedContentCollection(Guid productVariantKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void SaveDetachedContents(IProductVariant productVariant)
        {
            throw new NotImplementedException();
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
