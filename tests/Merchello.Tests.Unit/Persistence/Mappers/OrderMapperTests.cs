namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class OrderMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchOrder";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new OrderMapper();
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
        public void Can_Map_OrderNumberPrefix_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OrderNumberPrefix");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[orderNumberPrefix]"));
        }

        [Test]
        public void Can_Map_OrderNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OrderNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[orderNumber]"));
        }

        [Test]
        public void Can_Map_OrderDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OrderDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[orderDate]"));
        }

        [Test]
        public void Can_Map_OrderStatusKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OrderStatusKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[orderStatusKey]"));
        }

        [Test]
        public void Can_Map_VersionKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "VersionKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[versionKey]"));
        }

        [Test]
        public void Can_Map_Exported_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Exported");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[exported]"));
        }
    }
}