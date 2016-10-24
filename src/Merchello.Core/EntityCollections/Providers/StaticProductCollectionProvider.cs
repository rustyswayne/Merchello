namespace Merchello.Core.EntityCollections.Providers
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// The static product collection provider.
    /// </summary>
    [EntityCollectionProvider("4700456D-A872-4721-8455-1DDAC19F8C16", "9F923716-A022-4089-A110-1E9B4E1F2AD1", "Product Collection", "A static product collection that could be used for product categories and product groupings", false)]
    internal sealed class StaticProductCollectionProvider : EntityCollectionProviderBase<IProductService, IProduct>, IProductCollectionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticProductCollectionProvider"/> class.
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
        public StaticProductCollectionProvider(IProductService productService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(productService, entityCollectionService, cacheHelper, collectionKey)
        {
        }
    }
}