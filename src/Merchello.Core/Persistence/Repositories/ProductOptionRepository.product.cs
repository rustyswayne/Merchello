namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Guid> SaveForProduct(IProduct product)
        {
            // Ensures the sort order with respect to this product
            if (!product.ProductOptions.Any())
                EnsureProductOptionsSortOrder(product.ProductOptions);

            // Reset the Product Options Collection so that updated values are ordered and cached correctly
            product.ProductOptions = SaveForProduct(product.ProductOptions.AsEnumerable(), product.Key);

            return product.ProductOptions.Where(x => x.Shared).Select(x => x.Key);
        }

        /// <inheritdoc/>
        public ProductOptionCollection GetProductOptionCollection(Guid productKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> DeleteAllProductOptions(IProduct product)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Saves a collection of product options for a given product.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The saved collection <see cref="IEnumerable{IProductOption}"/>.
        /// </returns>
        internal ProductOptionCollection SaveForProduct(IEnumerable<IProductOption> options, Guid productKey)
        {
            throw new NotImplementedException();
            //// Order the options by sort order
            //var savers = options.OrderBy(x => x.SortOrder);

            //// Find the product options to find any that need to be removed 
            //var existing = GetByProductKey(productKey).ToArray();

            //if (existing.Any())
            //{
            //    // Remove any options that previously existed in the product option collection that are not present in the new collection
            //    this.SafeRemoveSharedOptionsFromProduct(savers, existing, productKey);
            //}

            //foreach (var o in savers)
            //{
            //    this.SafeAddOrUpdateProductWithProductOption(o, existing.Any(x => x.Key == o.Key), productKey);
            //}

            //return GetProductOptionCollection(productKey);
        }


        /// <summary>
        /// Ensures the sort order of product options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        private void EnsureProductOptionsSortOrder(ProductOptionCollection options)
        {
            if (!options.Any()) return;

            var sort = 1;
            var sorted = options.OrderBy(x => x.SortOrder);
            foreach (var option in sorted)
            {
                if (option.SortOrder != sort) option.SortOrder = sort;
                sort++;
            }
        }
    }
}
