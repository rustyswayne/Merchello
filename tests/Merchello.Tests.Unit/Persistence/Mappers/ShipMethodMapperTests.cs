namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ShipMethodMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchShipMethod";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ShipMethodMapper();
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
        public void Can_Map_ShipCountryKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipCountryKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipCountryKey]"));
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
        public void Can_Map_ServiceCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ServiceCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[serviceCode]"));
        }

        [Test]
        public void Can_Map_Surcharge_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Surcharge");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[surcharge]"));
        }

        [Test]
        public void Can_Map_Taxable_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Taxable");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[taxable]"));
        }
    }
}