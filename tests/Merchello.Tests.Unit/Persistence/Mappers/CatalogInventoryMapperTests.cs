namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class CatalogInventoryMapperTests : EntityMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchCatalogInventory";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new CatalogInventoryMapper();
        }


        [Test]
        public void Can_Map_CatalogKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CatalogKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[catalogKey]"));
        }


        [Test]
        public void Can_Map_ProductVariantKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProductVariantKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[productVariantKey]"));
        }


        [Test]
        public void Can_Map_Count_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Count");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[count]"));
        }


        [Test]
        public void Can_Map_LowCount_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "LowCount");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[lowCount]"));
        }


        [Test]
        public void Can_Map_Location_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Location");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[location]"));
        }
    }
}