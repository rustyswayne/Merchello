namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class TaxMethodMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchTaxMethod";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new TaxMethodMapper();
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
        public void Can_Map_Name_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Name");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[name]"));
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
        public void Can_Map_PercentageTaxRate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PercentageTaxRate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[percentageTaxRate]"));
        }

        [Test]
        public void Can_Map_ProductTaxMethod_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProductTaxMethod");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[productTaxMethod]"));
        }


    }
}