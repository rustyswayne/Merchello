namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class OfferSettingsMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchOfferSettings";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new OfferSettingsMapper();
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
        public void Can_Map_OfferStartsDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OfferStartsDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[offerStartsDate]"));
        }

        [Test]
        public void Can_Map_OfferEndsDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OfferEndsDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[offerEndsDate]"));
        }

        [Test]
        public void Can_Map_Active_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Active");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[active]"));
        }
    }
}