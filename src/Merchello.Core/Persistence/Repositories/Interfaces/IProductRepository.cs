namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IProduct"/> entities.
    /// </summary>
    public interface IProductRepository : ISearchTermRepository<IProduct>, IEntityCollectionEntityRepository<IProduct>
    {
        /// <summary>
        /// Gets a collection of <see cref="IProduct"/> that has detached content of type.
        /// </summary>
        /// <param name="detachedContentTypeKey">
        /// The detached content type key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProduct}"/>.
        /// </returns>
        IEnumerable<IProduct> GetByDetachedContentType(Guid detachedContentTypeKey);

        /// <summary>
        /// Gets the product key for a slug.
        /// </summary>
        /// <param name="slug">
        /// The slug.
        /// </param>
        /// <returns>
        /// The <see cref="IProduct"/>.
        /// </returns>
        IProduct GetKeyForSlug(string slug);

        /// <summary>
        /// Gets or sets a value Indicating whether or not a SKU is already exists in the database
        /// </summary>
        /// <param name="sku">The SKU to be tested</param>
        /// <returns>A value indicating whether or not a SKU is already exists in the database</returns>
        bool SkuExists(string sku);


        PagedCollection<IProduct> GetProductsWithOption(Guid optionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsWithOption(string optionName, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsWithOption(string optionName, string choiceName, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsWithOption(string optionName, IEnumerable<string> choiceNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductWithOptions(IEnumerable<string> optionNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsInPriceRange(decimal min, decimal max, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsInPriceRange(decimal min, decimal max, decimal taxModifier, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsByManufacturer(string manufacturer, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsKeysByManufacturers(IEnumerable<string> manufacturer, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsByBarcode(string barcode, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsByBarcode(IEnumerable<string> barcodes, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        PagedCollection<IProduct> GetProductsInStock(long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending, bool includeAllowOutOfStockPurchase = false);

        PagedCollection<IProduct> GetProductsOnSale(long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        int CountKeysThatExistInAllCollections(Guid[] collectionKeys);

        IEnumerable<Tuple<IEnumerable<Guid>, int>> CountKeysThatExistInAllCollections(IEnumerable<Guid[]> collectionKeysGroups);
    }
}