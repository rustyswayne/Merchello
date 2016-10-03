namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class OfferRedeemedMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchOfferRedeemed";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new OfferRedeemedMapper();
        }

        [Test]
        public void Can_Map_OfferSettingsKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OfferSettingsKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[offerSettingsKey]"));
        }

        [Test]
        public void Can_Map_OfferCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OfferCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[offerCode]"));
        }

        [Test]
        public void Can_Map_OfferProviderKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OfferProviderKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[offerProviderKey]"));
        }

        [Test]
        public void Can_Map_CustomerKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CustomerKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[customerKey]"));
        }

        [Test]
        public void Can_Map_InvoiceKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InvoiceKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceKey]"));
        }

        [Test]
        public void Can_Map_RedeemedDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "RedeemedDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[redeemedDate]"));
        }

    }
}