namespace Merchello.Core.EntityCollections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Cache;
    using Merchello.Core.EntityCollections.Providers;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    /// <summary>
    /// A base class for Product based Specified Filter Collections Providers.
    /// </summary>
    public abstract class ProductFilterGroupProviderBase : EntityCollectionProviderBase<IProductService, IProduct>, IProductFilterGroupProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFilterGroupProviderBase"/> class.
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
        protected ProductFilterGroupProviderBase(IProductService productService, IEntityCollectionService entityCollectionService, ICacheHelper cacheHelper, Guid collectionKey)
            : base(productService, entityCollectionService, cacheHelper, collectionKey)
        {
        }

        /// <summary>
        /// Gets the attribute provider type.
        /// </summary>
        public virtual Type FilterProviderType
        {
            get
            {
                return typeof(StaticProductCollectionProvider);
            }
        }

        /// <summary>
        /// Gets the <see cref="IEntityFilterGroup"/>.
        /// </summary>
        public virtual IEntityFilterGroup EntityGroup
        {
            get
            {
                return (IEntityFilterGroup)this.EntityCollection;
            }
        }

        /// <summary>
        /// Gets the collection of child collection keys.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        protected virtual IEnumerable<Guid> GetAttributeCollectionKeys()
        {
            if (!this.EntityGroup.Filters.Any())
            {
                MultiLogHelper.Info<ProductFilterGroupProviderBase>("ProductFilterGroup does not have any child collections. Returning null.");
                return null;
            }

            return this.EntityGroup.Filters.Select(x => x.Key);
        }
    }
}