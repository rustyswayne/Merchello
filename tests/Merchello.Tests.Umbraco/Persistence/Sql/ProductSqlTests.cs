namespace Merchello.Tests.Umbraco.Persistence.Sql
{
    using System;

    using Core.Acquired.Persistence;
    using global::Umbraco.Core.Persistence;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Umbraco.TestHelpers.Base;
    using NPoco;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    using SqlContext = global::Merchello.Core.Persistence.SqlContext;

    [TestFixture]
    public class ProductSqlTests : MerchelloDatabaseTestBase
    {
        private readonly string orderExpression = "name";

        private readonly Direction direction = Direction.Ascending;

        private IDatabaseAdapter _dbAdapter;

        public override void Initialize()
        {
            base.Initialize();

            _dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();

        }

        [Test]
        public void GetProductsWithOption_ByOptionKey()
        {
            var optionKey = Guid.NewGuid();

            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where<ProductOptionDto>(x => x.Key == optionKey);

            var sql = GetBaseQuery(false)
                .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                .AppendOrderExpression<ProductVariantDto>("name", direction);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }


        [Test]
        public void GetProductsWithOption_ByOptionName()
        {
            var values = new object[] { "test" };

            var tblName = _dbAdapter.Sql().SqlContext.SqlSyntax.GetQuotedTableName("merchProductOption");
            var nameCol = _dbAdapter.Sql().SqlContext.SqlSyntax.GetQuotedColumnName("name");

            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where($"{tblName}.{nameCol} IN (@values)", new { values });

            var sql = GetBaseQuery(false)
            .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
            .AppendOrderExpression<ProductVariantDto>("name", direction);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }

        [Test]
        public void GetProductsWithOption_ByOptionNameAndChoiceName()
        {
            var name = "test";

            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                    .From<ProductVariantDto>()
                    .InnerJoin<ProductVariant2ProductAttributeDto>()
                    .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                    .InnerJoin<ProductAttributeDto>()
                    .On<ProductVariant2ProductAttributeDto, ProductAttributeDto>(left => left.ProductAttributeKey, right => right.Key)
                    .InnerJoin<ProductOptionDto>()
                    .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                    .Where<ProductOptionDto>(x => x.Name == name)
                    .Where<ProductAttributeDto>(x => x.Name == name);

            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductVariantDto>(orderExpression, direction);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }

        [Test]
        public void GetProductWithOption_ByOptionNameAndChoiceNames()
        {
            var optionName = "test";
            var choiceNames = new[] { "choice1", "choice2", "choice3" };

            var tblName = _dbAdapter.SqlSyntax.GetQuotedTableName("merchProductAttribute");
            var nameCol = _dbAdapter.SqlSyntax.GetQuotedColumnName("name");

            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariant2ProductAttributeDto>()
                .On<ProductVariantDto, ProductVariant2ProductAttributeDto>(left => left.Key, right => right.ProductVariantKey)
                .InnerJoin<ProductAttributeDto>()
                .On<ProductVariant2ProductAttributeDto, ProductAttributeDto>(left => left.ProductAttributeKey, right => right.Key)
                .InnerJoin<ProductOptionDto>()
                .On<ProductVariant2ProductAttributeDto, ProductOptionDto>(left => left.OptionKey, right => right.Key)
                .Where<ProductOptionDto>(x => x.Name == optionName)
                .Where($"{tblName}.{nameCol} IN (@values)", new { @values = choiceNames });


            var sql = GetBaseQuery(false)
                        .SingleWhereIn<ProductDto>(x => x.Key, innerSql)
                        .AppendOrderExpression<ProductVariantDto>(orderExpression, direction);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }


        [Test]
        public void GetProductsInPriceRange()
        {
            var pvt = _dbAdapter.SqlSyntax.GetQuotedTableName("merchProductVariant");
            var osc = _dbAdapter.SqlSyntax.GetQuotedColumnName("salePrice");

            var min = 10M;
            var max = 20M;

            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                .From<ProductVariantDto>()
                .Where<ProductVariantDto>(x => !x.OnSale)
                .WhereBetween<ProductVariantDto>(x => x.Price, min, max)
                .Append("OR (")
                .Append($"{pvt}.{osc} = 1")
                .AndBetween<ProductVariantDto>(x => x.SalePrice, min, max)
                .Append(")");

            var sql = GetBaseQuery(false).SingleWhereIn<ProductDto>(x => x.Key, innerSql);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }

        [Test]
        public void GetProductsInStock()
        {
            var innerSql =
                _dbAdapter.Sql()
                    .SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                    .From<ProductVariantDto>()
                    .InnerJoin<CatalogInventoryDto>()
                    .On<ProductVariantDto, CatalogInventoryDto>(left => left.Key, right => right.ProductVariantKey)
                    .Where<ProductVariantDto>(x => x.TrackInventory)
                    .Where<CatalogInventoryDto>(x => x.Count > 0);

            //.Or<ProductVariantDto>(x => !x.TrackInventory);

            Console.WriteLine(innerSql.SQL);

            //Assert.DoesNotThrow(() => _dbAdapter.Database.Execute(innerSql));
        }

        [Test]
        public void GetProductsOnSql()
        {
            var innerSql = _dbAdapter.Sql().SelectDistinct<ProductVariantDto>(x => x.ProductKey)
                            .From<ProductVariantDto>()
                            .Where<ProductVariantDto>(x => x.OnSale);

            var sql = GetBaseQuery(false).SingleWhereIn<ProductDto>(x => x.Key, innerSql);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => _dbAdapter.Database.Fetch<ProductDto>(sql));
        }

        private Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            var tbl = _dbAdapter.Sql().SqlContext.SqlSyntax.GetQuotedTableName("merchProductVariant");
            var col = _dbAdapter.Sql().SqlContext.SqlSyntax.GetQuotedColumnName("master");

            return _dbAdapter.Sql().Select(isCount ? "COUNT(*)" : "*")
               .From<ProductDto>()
               .InnerJoin<ProductVariantDto>()
               .On<ProductDto, ProductVariantDto>(left => left.Key, right => right.ProductKey)
               .InnerJoin<ProductVariantIndexDto>()
               .On<ProductVariantDto, ProductVariantIndexDto>(left => left.Key, right => right.ProductVariantKey)
               .Where<ProductVariantDto>(x => x.Master);
               //.Where($"{tbl}.{col} = 1");
        }
    }
}