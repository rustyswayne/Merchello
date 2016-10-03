namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ItemCacheMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchItemCache";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ItemCacheMapper();
        }

        [Test]
        public void Can_Map_EntityKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "EntityKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[entityKey]"));
        }

        [Test]
        public void Can_Map_ItemCacheTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ItemCacheTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[itemCacheTfKey]"));
        }

        [Test]
        public void Can_Map_Version_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "VersionKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[versionKey]"));
        }
    }
}