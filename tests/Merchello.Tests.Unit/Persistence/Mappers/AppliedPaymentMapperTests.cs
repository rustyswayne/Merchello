namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class AppliedPaymentMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchAppliedPayment";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new AppliedPaymentMapper();
        }

        [Test]
        public void Can_Map_PaymentKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PaymentKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[paymentKey]"));
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
        public void Can_Map_AppliedPaymentTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "AppliedPaymentTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[appliedPaymentTfKey]"));
        }

        [Test]
        public void Can_Map_Description_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Description");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[description]"));
        }

        [Test]
        public void Can_Map_Amount_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Amount");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[amount]"));
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