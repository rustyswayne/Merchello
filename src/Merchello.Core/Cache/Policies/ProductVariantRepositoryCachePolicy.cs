namespace Merchello.Core.Cache.Policies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <summary>
    /// Represents a cache policy for the <see cref="ProductVariantRepository"/>.
    /// </summary>
    internal class ProductVariantRepositoryCachePolicy : DefaultRepositoryCachePolicy<IProductVariant>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantRepositoryCachePolicy"/> class.
        /// </summary>
        /// <param name="cache">
        /// The <see cref="IRuntimeCacheProviderAdapter"/>.
        /// </param>
        /// <param name="options">
        /// The <see cref="RepositoryCachePolicyOptions"/>.
        /// </param>
        /// <remarks>
        /// We need to ensure the product cache is updated on product variant updates
        /// </remarks>
        public ProductVariantRepositoryCachePolicy(IRuntimeCacheProviderAdapter cache, RepositoryCachePolicyOptions options)
            : base(cache, options)
        {
        }
    }
}
