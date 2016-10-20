namespace Merchello.Tests.Umbraco.Persistence.Sql
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.DI;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    using InvoiceDto = Merchello.Tests.Umbraco.Migrations.V2Dtos.InvoiceDto;

    [TestFixture]
    public class InvoiceSqlTests : MerchelloDatabaseTestBase
    {
        [Test]
        public void Can_GetFieldsForInvoiceDto()
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            var expresionist = new PocoToSqlExpressionHelper<InvoiceDto>(dbAdapter.Sql().SqlContext);

            Assert.NotNull(expresionist);
        }

        [Test]
        public void Can_Create_SelectSumSqlWithWhereBetween()
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            Assert.NotNull(dbAdapter);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;

            //var sql = dbAdapter.Sql().SelectSum<InvoiceDto>(x => x.Total);
            var sql = dbAdapter.Sql()
                        .SelectSum<InvoiceDto>(x => x.Total)
                           .From<InvoiceDto>()
                          .WhereBetween<InvoiceDto>(x => x.InvoiceDate, start, end);

            Console.WriteLine(sql.SQL);

            //// check that the query executes
            //// there are no values in this test
            var value = dbAdapter.Database.ExecuteScalar<decimal>(sql);

            Assert.That(value, Is.EqualTo(0));
        }

        [Test]
        public void Can_Execute_SqlFor_Invoice_SumLineItemTotalsBySku()
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            var SqlSyntax = dbAdapter.SqlSyntax;

            var sku = "test";
            var currencyCode = "USD";
            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;

            var sqlx = string.Format(
                    "{0}.{1} * {0}.{2}",
                    SqlSyntax.GetQuotedTableName("merchInvoiceItem"),
                    SqlSyntax.GetQuotedColumnName("quantity"),
                    SqlSyntax.GetQuotedColumnName("price"));

            // Console.WriteLine(sqlx);

            var sql = dbAdapter.Sql().SelectSum(sqlx)
                        .From<InvoiceDto>()
                        .InnerJoin<InvoiceItemDto>()
                        .On<InvoiceDto, InvoiceItemDto>(left => left.Key, right => right.ContainerKey)
                        .Where<InvoiceDto>(x => x.CurrencyCode == currencyCode)
                        .Where<InvoiceItemDto>(x => x.Sku == sku)
                        .Where<InvoiceDto>(x => x.CurrencyCode == currencyCode)
                        .WhereBetween<InvoiceDto>(x => x.InvoiceDate, start, end);

            Console.WriteLine(sql.SQL);

            // check that the query executes
            // there are no values in this test
            var value = dbAdapter.Database.ExecuteScalar<decimal>(sql);

            Assert.That(value, Is.EqualTo(0));
        }
    }
}