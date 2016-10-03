namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ShipRateTierMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchShipRateTier";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ShipRateTierMapper();
        }

        [Test]
        public void Can_Map_ShipMethodKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipMethodKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipMethodKey]"));
        }

        [Test]
        public void Can_Map_RangeLow_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "RangeLow");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[rangeLow]"));
        }

        [Test]
        public void Can_Map_RangeHigh_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "RangeHigh");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[rangeHigh]"));
        }

        [Test]
        public void Can_Map_Rate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Rate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[rate]"));
        }
    }
}