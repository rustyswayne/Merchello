namespace Merchello.Tests.Umbraco.Persistence.Sql
{
    using System;

    using Merchello.Core;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NPoco;

    using NUnit.Framework;

    [TestFixture]
    public class ItemCacheSqlTests : MerchelloDatabaseTestBase
    {
        [Test]
        public void Can_GetItemCachePage()
        {
            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            Assert.NotNull(dbAdapter);

            var SqlSyntax = dbAdapter.SqlSyntax;

            var itfkey = Guid.NewGuid();
            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;

            var icTbl = SqlSyntax.GetQuotedTableName("merchItemCacheItem");
            var ick = SqlSyntax.GetQuotedColumnName("itemCacheKey");
            var iciQ = SqlSyntax.GetQuotedName("itemCacheItemQ");

            

            var joinOn = string.Format(
                "{0}.{1} = {2}.{3}",
                SqlSyntax.GetQuotedName("itemCacheItemQ"),
                ick,
                SqlSyntax.GetQuotedTableName("merchItemCache"),
                SqlSyntax.GetQuotedColumnName("pk"));

            var innerSql =
                dbAdapter.Sql()
                    .Select($"{icTbl}.{ick}, COUNT(*) AS itemCount")
                    .From<ItemCacheItemDto>()
                    .GroupBy<ItemCacheItemDto>(x => x.ContainerKey);


            var sql =
                dbAdapter.Sql()
                    .SelectAll()
                    .FromQuery(innerSql, "itemCacheItemQ")
                    .InnerJoin<ItemCacheDto>()
                    .On(joinOn)
                    .InnerJoin<CustomerDto>()
                    .On<ItemCacheDto, CustomerDto>(left => left.EntityKey, right => right.Key)
                    .Where<ItemCacheDto>(x => x.ItemCacheTfKey == itfkey)
                    .WhereBetween<CustomerDto>(x => x.LastActivityDate, start, end)
                    .Where($"{iciQ}.itemCount > 0");

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => dbAdapter.Database.Fetch<ItemCacheDto>(sql), "GetItemCachePage Query throws error.");
        }


        [Test]
        [TestCase(CustomerType.Anonymous)]
        [TestCase(CustomerType.Customer)]
        public void Can_Count(CustomerType customerType)
        {

            var dbAdapter = IoC.Container.GetInstance<IDatabaseAdapter>();
            Assert.NotNull(dbAdapter);

            var start = DateTime.Now.AddDays(-1);
            var end = DateTime.Now;
            var SqlSyntax = dbAdapter.SqlSyntax;

            Sql<SqlContext> sql;

            var countQuery = dbAdapter.Sql()
                                .SelectField<ItemCacheItemDto>(x => x.ContainerKey)
                                .From<ItemCacheItemDto>()
                                .GroupBy<ItemCacheItemDto>(x => x.ContainerKey)
                                .Append("HAVING COUNT(*) > 0");

            if (customerType == CustomerType.Anonymous)
            {
                sql = dbAdapter.Sql()
                            .SelectCount()
                            .From<AnonymousCustomerDto>()
                            .InnerJoin<ItemCacheDto>()
                            .On<AnonymousCustomerDto, ItemCacheDto>(left => left.Key, right => right.EntityKey)
                            .WhereBetween<AnonymousCustomerDto>(x => x.LastActivityDate, start, end)
                            .Where<ItemCacheDto>(x => x.ItemCacheTfKey == Constants.TypeFieldKeys.ItemCache.BasketKey)
                            .AndIn<ItemCacheDto>(x => x.Key, countQuery);
            }
            else
            {
                sql = dbAdapter.Sql()
                    .SelectCount()
                    .From<CustomerDto>()
                    .InnerJoin<ItemCacheDto>()
                    .On<CustomerDto, ItemCacheDto>(left => left.Key, right => right.EntityKey)
                    .WhereBetween<CustomerDto>(x => x.LastActivityDate, start, end)
                    .Where<ItemCacheDto>(x => x.ItemCacheTfKey == Constants.TypeFieldKeys.ItemCache.BasketKey)
                    .AndIn<ItemCacheDto>(x => x.Key, countQuery);
            }

            


            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => dbAdapter.Database.ExecuteScalar<int>(sql));

            var count = dbAdapter.Database.ExecuteScalar<int>(sql);
            Assert.That(0, Is.EqualTo(count));
        }
    }
}