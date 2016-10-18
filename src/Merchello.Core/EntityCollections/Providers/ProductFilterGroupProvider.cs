namespace Merchello.Core.EntityCollections.Providers
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Services;

    /// <summary>
    /// Represents the product based filter collection.
    /// </summary>
    /// <remarks>
    /// EntityFilterProviders need to implement <see cref="IEntityFilterGroupProvider"/>
    /// </remarks>
    [EntityCollectionProvider("5316C16C-E967-460B-916B-78985BB7CED2", "9F923716-A022-4089-A110-1E9B4E1F2AD1", "Product Filter Collection", "A collection of product filters that could be used for product filters and custom product groupings", false)]
    public class ProductFilterGroupProvider : ProductFilterGroupProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFilterGroupProvider"/> class.
        /// </summary>
        /// <param name="productService">
        /// The <see cref="IProductService"/>.
        /// </param>
        /// <param name="entityCollectionService">
        /// The <see cref="IEntityCollectionService"/>.
        /// </param>
        /// <param name="cacheHelper">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="collectionKey">
        /// The collection Key.
        /// </param>
        public ProductFilterGroupProvider(IProductService productService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(productService, entityCollectionService, cacheHelper, collectionKey)
        {
        }
    }
}