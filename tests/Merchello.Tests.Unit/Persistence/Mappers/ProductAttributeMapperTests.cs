namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ProductAttributeMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchProductAttribute";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ProductAttributeMapper();
        }

        [Test]
        public void Can_Map_OptionKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OptionKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[optionKey]"));
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
        public void Can_Map_Sku_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Sku");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[sku]"));
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
        public void Can_Map_IsDefaultChoice_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "IsDefaultChoice");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[isDefaultChoice]"));
        }
    }
}