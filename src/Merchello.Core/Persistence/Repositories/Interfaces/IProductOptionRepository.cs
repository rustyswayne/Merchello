﻿namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;

    using NPoco;

    /// <summary>
    /// Represents a product option repository.
    /// </summary>
    public interface IProductOptionRepository : INPocoEntityRepository<IProductOption>, ISearchTermRepository<IProductOption>
    {
        /// <summary>
        /// Saves the product options for a given product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// A collection of product keys of products that need to be refreshed in the current cache and examine.
        /// </returns>
        IEnumerable<Guid> SaveForProduct(IProduct product);

        /// <summary>
        /// Creates the attribute association between product attribute and product variant.
        /// </summary>
        /// <param name="variant">
        /// The variant.
        /// </param>
        /// <returns>
        /// A collection of product keys of products that need to be refreshed in the current cache and examine.
        /// </returns>
        IEnumerable<Guid> CreateAttributeAssociationForProductVariant(IProductVariant variant);

        /// <summary>
        /// Queries for product options by a collection of keys.
        /// </summary>
        /// <param name="optionKeys">
        /// The option Keys.
        /// </param>
        /// <param name="sharedOnly">
        /// The shared Only.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProductOption}"/>.
        /// </returns>
        IEnumerable<IProductOption> GetProductOptions(Guid[] optionKeys, bool sharedOnly = false);

        /// <summary>
        /// Gets a product attribute by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IProductAttribute"/>.
        /// </returns>
        IProductAttribute GetProductAttributeByKey(Guid key);

        /// <summary>
        /// Gets <see cref="IProductAttribute"/> by a an array of keys.
        /// </summary>
        /// <param name="attributeKeys">
        /// The attribute keys.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEnumerable{IProductAttribute}"/>.
        /// </returns>
        IEnumerable<IProductAttribute> GetProductAttributes(Guid[] attributeKeys);

        /// <summary>
        /// Gets the <see cref="ProductOptionCollection"/> for a given product key.
        /// </summary>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="ProductOptionCollection"/>.
        /// </returns>
        /// <remarks>
        /// The method manages the sort order of the options with respect to the product
        /// This query is never cached and is intended to generate objects that will be cached in 
        /// individual product collections
        /// </remarks>
        ProductOptionCollection GetProductOptionCollection(Guid productKey);


        /// <summary>
        /// Gets a <see cref="ProductAttributeCollection"/> for a product variant.
        /// </summary>
        /// <param name="productVariantKey">
        /// The product variant key.
        /// </param>
        /// <returns>
        /// The <see cref="ProductAttributeCollection"/>.
        /// </returns>
        ProductAttributeCollection GetProductAttributeCollectionForVariant(Guid productVariantKey);

        /// <summary>
        /// Gets a paged collection of <see cref="IProductOption"/>.
        /// </summary>
        /// <param name="searchTerm">
        /// A search term to filter by
        /// </param>
        /// <param name="page">
        /// The page requested.
        /// </param>
        /// <param name="itemsPerPage">
        /// The number of items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The sort by field.
        /// </param>
        /// <param name="sharedOnly">
        /// Indicates whether or not to only include shared option.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IProductOption}"/>.
        /// </returns>
        PagedCollection<IProductOption> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, bool sharedOnly, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets use count information for an option and its choices.
        /// </summary>
        /// <param name="option">
        /// The option key.
        /// </param>
        /// <returns>
        /// The <see cref="ProductOptionUseCount"/>.
        /// </returns>
        /// <remarks>
        /// Used for determining shared option usage
        /// </remarks>
        IProductOptionUseCount GetProductOptionUseCount(IProductOption option);

        /// <summary>
        /// Deletes all products options.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <remarks>
        /// Used when deleting a product
        /// </remarks>
        /// <returns>
        /// A collection of product keys of products that need to be refreshed in the current cache and examine.
        /// </returns>
        IEnumerable<Guid> DeleteAllProductOptions(IProduct product);

        /// <summary>
        /// Deletes all product attributes from a product variant.
        /// </summary>
        /// <param name="variant">
        /// The variant.
        /// </param>
        /// <remarks>
        /// Used when deleting a product variant
        /// </remarks>
        /// <returns>
        /// A collection of product keys of products that need to be refreshed in the current cache and examine.
        /// </returns>
        [Obsolete("Move to cache policy")]
        IEnumerable<Guid> DeleteAllProductVariantAttributes(IProductVariant variant);

        /// <summary>
        /// Updates an attribute.
        /// </summary>
        /// <param name="attribute">
        /// The attribute.
        /// </param>
        void UpdateAttribute(IProductAttribute attribute);

        /// <summary>
        /// Gets the count of the number of product associations for an option
        /// </summary>
        /// <param name="optionKey">
        /// The option key.
        /// </param>
        /// <returns>
        /// The count.
        /// </returns>
        int GetSharedProductOptionCount(Guid optionKey);

        /// <summary>
        /// Gets the count of the product variant associations for a product attribute.
        /// </summary>
        /// <param name="attributeKey">
        /// The attribute key.
        /// </param>
        /// <returns>
        /// The count.
        /// </returns>
        int GetSharedProductAttributeCount(Guid attributeKey);

        /// <summary>
        /// Updates product options that have detached content association with a detached content item that is being deleted.
        /// </summary>
        /// <param name="detachedContentTypeKey">
        /// The detached content type key.
        /// </param>
        /// <remarks>
        /// Sets the detachedContentTypeKey = NULL
        /// </remarks>
        void UpdateForDetachedContentDelete(Guid detachedContentTypeKey);
    }
}