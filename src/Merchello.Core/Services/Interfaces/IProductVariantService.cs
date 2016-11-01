namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;


    /// <summary>
    /// Represents a data service for <see cref="IProductVariant"/>.
    /// </summary>
    public interface IProductVariantService
    {
        /// <summary>
        /// Gets an <see cref="IProductVariant"/> object by its unique key
        /// </summary>
        /// <param name="key">key of the Product to retrieve</param>
        /// <returns><see cref="IProductVariant"/></returns>
        IProductVariant GetProductVariantByKey(Guid key);

        /// <summary>
        /// Gets an <see cref="IProductVariant"/> object by it's unique SKU.
        /// </summary>
        /// <param name="sku">
        /// The SKU.
        /// </param>
        /// <returns>
        /// The <see cref="IProductVariant"/>.
        /// </returns>
        IProductVariant GetProductVariantBySku(string sku);

        /// <summary>
        /// Gets list of <see cref="IProductVariant"/> objects given a list of Unique keys
        /// </summary>
        /// <param name="keys">List of keys for ProductVariant objects to retrieve</param>
        /// <returns>List of <see cref="IProduct"/></returns>
        IEnumerable<IProductVariant> GetAllProductVariants(params Guid[] keys);

        /// <summary>
        /// Gets a collection of <see cref="IProductVariant"/> objects for a given Product Key
        /// </summary>
        /// <param name="productKey">GUID product key of the <see cref="IProductVariant"/> collection to retrieve</param>
        /// <returns>A collection of <see cref="IProductVariant"/></returns>
        IEnumerable<IProductVariant> GetProductVariantsByProductKey(Guid productKey);

        /// <summary>
        /// Gets a collection of <see cref="IProductVariant"/> objects associated with a given warehouse 
        /// </summary>
        /// <param name="warehouseKey">The 'unique' key of the warehouse</param>
        /// <returns>A collection of <see cref="IProductVariant"/></returns>
        IEnumerable<IProductVariant> GetProductVariantsByWarehouseKey(Guid warehouseKey);

        /// <summary>
        /// True/false indicating whether or not a SKU is already exists in the database
        /// </summary>
        /// <param name="sku">
        /// The SKU to be tested
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> indicating whether or not the SKU exists.
        /// </returns>
        bool SkuExists(string sku);
    }
}