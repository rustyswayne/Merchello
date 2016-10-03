namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ShipCountryMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchShipCountry";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ShipCountryMapper();
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
        public void Can_Map_CountryCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CountryCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[countryCode]"));
        }

        [Test]
        public void Can_Map_Name_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Name");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[name]"));
        }
    }
}