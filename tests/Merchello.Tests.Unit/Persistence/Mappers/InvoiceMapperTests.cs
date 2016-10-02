namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchInvoice";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new InvoiceMapper();
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
        public void Can_Map_InvoiceNumberPrefix_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InvoiceNumberPrefix");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceNumberPrefix]"));
        }

        [Test]
        public void Can_Map_InvoiceNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InvoiceNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceNumber]"));
        }

        [Test]
        public void Can_Map_PoNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PoNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[poNumber]"));
        }

        [Test]
        public void Can_Map_InvoiceDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InvoiceDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceDate]"));
        }

        [Test]
        public void Can_Map_InvoiceStatusKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InvoiceStatusKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[invoiceStatusKey]"));
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
        public void Can_Map_BillToName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToName]"));
        }

        [Test]
        public void Can_Map_BillToAddress1_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToAddress1");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToAddress1]"));
        }

        [Test]
        public void Can_Map_BillToAddress2_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToAddress2");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToAddress2]"));
        }

        [Test]
        public void Can_Map_BillToLocality_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToLocality");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToLocality]"));
        }

        [Test]
        public void Can_Map_BillToRegion_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToRegion");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToRegion]"));
        }

        [Test]
        public void Can_Map_BillToPostalCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToPostalCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToPostalCode]"));
        }

        [Test]
        public void Can_Map_BillToCountryCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToCountryCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToCountryCode]"));
        }

        [Test]
        public void Can_Map_BillToEmail_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToEmail");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToEmail]"));
        }

        [Test]
        public void Can_Map_BillToPhone_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BillToPhone");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[billToPhone]"));
        }

        [Test]
        public void Can_Map_CurrencyCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CurrencyCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[currencyCode]"));
        }

        [Test]
        public void Can_Map_Exported_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Exported");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[exported]"));
        }

        [Test]
        public void Can_Map_Archived_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Archived");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[archived]"));
        }

        [Test]
        public void Can_Map_Total_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Total");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[total]"));
        }
    }
}