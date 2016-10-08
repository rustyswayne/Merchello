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

            Assert.DoesNotThrow(() => _dbAdapter.Database.Execute(sql));
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