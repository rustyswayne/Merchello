namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ProductOptionMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchProductOption";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ProductOptionMapper();
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
        public void Can_Map_DetachedContentTypeKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "DetachedContentTypeKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[detachedContentTypeKey]"));
        }

        [Test]
        public void Can_Map_Required_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Required");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[required]"));
        }

        [Test]
        public void Can_Map_Shared_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Shared");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shared]"));
        }

        [Test]
        public void Can_Map_UiOption_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "UiOption");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[uiOption]"));
        }
    }
}