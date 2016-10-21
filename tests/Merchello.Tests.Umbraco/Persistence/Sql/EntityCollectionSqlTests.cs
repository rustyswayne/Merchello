namespace Merchello.Tests.Umbraco.Persistence.Sql
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.DI;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NPoco;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionSqlTests : MerchelloDatabaseTestBase
    {
        private IDatabaseAdapter _db;

        [OneTimeSetUp]
        public override void Initialize()
        {
            base.Initialize();

            this._db = MC.Container.GetInstance<IDatabaseAdapter>();
        }

        internal Sql<SqlContext> Sql()
        {
            return NPoco.Sql.BuilderFor(new SqlContext(this._db));
        }

        [Test]
        public void Can_Build_GetEntityCollectionsByProductKey_Query()
        {
            var productKey = Guid.NewGuid();

            var innerSql = this.Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.EntityCollectionKey)
                            .From<Product2EntityCollectionDto>()
                            .Where<Product2EntityCollectionDto>(x => x.ProductKey == productKey);

            var sql =
                this.Sql().SelectAll()
                    .From<EntityCollectionDto>()
                    .Where<EntityCollectionDto>(x => x.IsFilter)
                    .AndIn<EntityCollectionDto>(x => x.Key, innerSql);

            Console.WriteLine(sql.SQL);

            Assert.DoesNotThrow(() => this._db.Database.Fetch<EntityCollectionDto>(sql), "GetEntityCollectionsByProductKey throws error.");
        }
    }
}