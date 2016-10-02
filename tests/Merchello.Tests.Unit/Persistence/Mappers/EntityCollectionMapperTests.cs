namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class EntityCollectionMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchEntityCollection";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new EntityCollectionMapper();
        }

        [Test]
        public void Can_Map_ParentKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ParentKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[parentKey]"));
        }

        [Test]
        public void Can_Map_EntityTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "EntityTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[entityTfKey]"));
        }

        [Test]
        public void Can_Map_Name_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Name");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[name]"));
        }

        [Test]
        public void Can_Map_SortOrder_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "SortOrder");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[sortOrder]"));
        }

        [Test]
        public void Can_Map_ProviderKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProviderKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[providerKey]"));
        }

        [Test]
        public void Can_Map_IsFilter_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "IsFilter");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[isFilter]"));
        }
    }
}