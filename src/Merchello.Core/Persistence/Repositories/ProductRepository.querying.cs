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
            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where<ProductOptionDto>(x => x.Name == optionName);

            var sql = GetBaseQuery(false)
            .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
            .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(string optionName, string choiceName, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                                .From<ProductVariantDto>()
                                .InnerJoin<ProductVariant2ProductAttributeDto>()
                                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                                .InnerJoin<ProductAttributeDto>()
                                .On<ProductVariant2ProductAttributeDto, ProductAttributeDto>(left => left.ProductAttributeKey, right => right.Key)
                                .InnerJoin<ProductOptionDto>()
                                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                                .Where<ProductOptionDto>(x => x.Name == optionName)
                                .Where<ProductAttributeDto>(x => x.Name == choiceName);

            var sql = Sql().SelectAll()
                        .From<ProductDto>()
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsWithOption(string optionName, IEnumerable<string> choiceNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var tblName = SqlSyntax.GetQuotedTableName("merchProductAttribute");
            var nameCol = SqlSyntax.GetQuotedColumnName("name");

            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductAttributeDto>()
                .On<ProductVariant2ProductAttributeDto, ProductAttributeDto>(left => left.ProductAttributeKey, right => right.Key)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where<ProductOptionDto>(x => x.Name == optionName)
                .Where($"{tblName}.{nameCol} IN (@values)", new { @values = choiceNames});


            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductWithOptions(IEnumerable<string> optionNames, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var tblName = SqlSyntax.GetQuotedTableName("merchProductOption");
            var nameCol = SqlSyntax.GetQuotedColumnName("name");

            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where($"{tblName}.{nameCol} IN (@values)", new { @values = optionNames });

            var sql = GetBaseQuery(false)
            .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
            .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IProduct> GetProductsInPriceRange(decimal min, decimal max, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var pvt = SqlSyntax.GetQuotedTableName("merchProductVariant");
            var osc = SqlSyntax.GetQuotedColumnName("salePrice");

            var innerSql = Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .Where<ProductVariantDto>(x => !x.OnSale)
                .WhereBetween<ProductVariantDto>(x => x.Price, min, max)
                .Append("OR (")
                .Append($"{pvt}.{osc} = 1")
                .AndBetween<ProductVariantDto>(x => x.SalePrice, min, max)
                .Append(")");

            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductVariantDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
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
