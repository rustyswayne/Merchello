namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class ProductRepository : IProductRepository
    {
        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(Guid optionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(string optionName, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(string optionName, string choiceName, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(string optionName, IEnumerable<string> choiceNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductWithOptions(IEnumerable<string> optionNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsInPriceRange(decimal min, decimal max, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsInPriceRange(decimal min, decimal max, decimal taxModifier, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsByManufacturer(string manufacturer, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsKeysByManufacturers(IEnumerable<string> manufacturer, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsByBarcode(string barcode, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsByBarcode(IEnumerable<string> barcodes, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsInStock(long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending, bool includeAllowOutOfStockPurchase = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsOnSale(long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
