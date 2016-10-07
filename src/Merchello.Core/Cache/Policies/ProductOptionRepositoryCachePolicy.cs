namespace Merchello.Core.Cache.Policies
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal class ProductOptionRepositoryCachePolicy : DefaultRepositoryCachePolicy<IProductOption>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOptionRepositoryCachePolicy"/> class.
        /// </summary>
        /// <param name="cache">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        /// <param name="options">
        /// The <see cref="RepositoryCachePolicyOptions"/>.
        /// </param>
        /// <remarks>
        /// We need to ensure the product cache is cleared on "shared option" updates
        /// </remarks>
        public ProductOptionRepositoryCachePolicy(IRuntimeCacheProviderAdapter cache, RepositoryCachePolicyOptions options)
            : base(cache, options)
        {
        }

        /// <summary>
        /// Clears products from cache that include a shared option that was saved.
        /// </summary>
        /// <param name="productKeys">
        /// The product keys.
        /// </param>
        public void ClearProductWithSharedOption(IEnumerable<Guid> productKeys)
        {
            foreach (var key in productKeys)
            {
                Cache.ClearCacheItem(GetEntityCacheKey<IProduct>(key));
            }
        }
    }
}