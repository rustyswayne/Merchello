namespace Merchello.Tests.Umbraco.Persistence.Querying
{
    using System;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Umbraco.TestHelpers.Base;

    using NPoco;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionQueryTests : UmbracoApplicationContextTestBase
    {
        private IDatabaseAdapter _db;

        [OneTimeSetUp]
        public override void Initialize()
        {
            base.Initialize();

            _db = IoC.Container.GetInstance<IDatabaseAdapter>();
        }

        internal Sql<SqlContext> Sql()
        {
            return NPoco.Sql.BuilderFor(new SqlContext(_db));
        }

        [Test]
        public void Can_Build_GetEntityCollectionsByProductKey_Query()
        {
            var productKey = Guid.NewGuid();

            var innerSql = Sql().SelectDistinct<Product2EntityCollectionDto>(x => x.EntityCollectionKey)
                            .From<Product2EntityCollectionDto>()
                            .Where<Product2EntityCollectionDto>(x => x.ProductKey == productKey);

            var sql =
                Sql()
                    .Select("*")
                    .From<EntityCollectionDto>()
                    .Where<EntityCollectionDto>(x => x.IsFilter)
                    .AndIn<EntityCollectionDto>(x => x.Key, innerSql);

            Console.WriteLine(sql.SQL);

            _db.Database.Execute(sql);
        }
    }
}