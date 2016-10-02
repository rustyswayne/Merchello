namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceLineItemMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchInvoiceItem";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new InvoiceLineItemMapper();
        }

        [Test]
        public void Can_Map_ContainerKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ContainerKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceKey]"));
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
        public void Can_Map_LineItemTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "LineItemTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[lineItemTfKey]"));
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
        public void Can_Map_Quantity_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Quantity");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[quantity]"));
        }

        [Test]
        public void Can_Map_Price_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Price");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[price]"));
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