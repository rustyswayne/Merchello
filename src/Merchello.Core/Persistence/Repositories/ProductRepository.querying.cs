namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class ProductRepository : IProductRepository
    {
        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(Guid optionKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where<ProductOptionDto>(x => x.Key == optionKey);

            var sql = GetBaseQuery(false)
                .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);

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
            //sql.Append("SELECT *")
            //    .Append("FROM [merchProductVariant]")
            //    .Append("WHERE [merchProductVariant].[productKey] IN (")
            //    .Append("SELECT DISTINCT([productKey])")
            //    .Append("FROM (")
            //    .Append("SELECT	[merchProductVariant].[productKey]")
            //    .Append("FROM [merchProductVariant]")
            //    .Append("INNER JOIN [merchProductVariant2ProductAttribute]")
            //    .Append("ON	[merchProductVariant].[pk] = [merchProductVariant2ProductAttribute].[productVariantKey]")
            //    .Append("INNER JOIN [merchProductOption]")
            //    .Append("ON [merchProductVariant2ProductAttribute].[optionKey] = [merchProductOption].[pk]")
            //    .Append("WHERE [merchProductOption].[name] IN (@names)", new { @names = optionNames })
            //    .Append(") [merchProductVariant]")
            //    .Append(")")
            //    .Append("AND [merchProductVariant].[master] = 1");

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
