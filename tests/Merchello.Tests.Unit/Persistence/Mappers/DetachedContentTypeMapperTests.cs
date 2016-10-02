namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class DetachedContentTypeMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchDetachedContentType";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new DetachedContentTypeMapper();
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
        public void Can_Map_Description_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Description");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[description]"));
        }

        [Test]
        public void Can_Map_ContentTypeKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ContentTypeKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[contentTypeKey]"));
        }
    }
}