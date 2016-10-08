namespace Merchello.Tests.Umbraco.Persistence.Querying
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Umbraco.TestHelpers.Base;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceQueryTests : MerchelloDatabaseTestBase
    {
        [Test]
        public void Can_Create_SelectSumSqlWithWhereBetween()
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            Assert.NotNull(dbAdapter);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;

            var sql = dbAdapter.Sql()
                        .SelectSum<InvoiceDto>(x => x.Total)
                           .From<InvoiceDto>()
                           .WhereBetween<InvoiceDto>(x => x.InvoiceDate, start, end);

            Console.WriteLine(sql.SQL);

            // check that the query executes
            // there are no values in this test
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


            var sql = dbAdapter.Sql().SelectSum(sqlx)
                        .From<InvoiceDto>()
                        .InnerJoin<InvoiceItemDto>()
                        .On<InvoiceDto, InvoiceItemDto>(left => left.Key, right => right.ContainerKey)
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