namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class WarehouseCatalogMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchWarehouseCatalog";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new WarehouseCatalogMapper();
        }

        [Test]
        public void Can_Map_WarehouseKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "WarehouseKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[warehouseKey]"));
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
    }
}