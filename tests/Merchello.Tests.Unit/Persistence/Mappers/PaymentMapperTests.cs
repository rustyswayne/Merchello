namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class PaymentMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchPayment";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new PaymentMapper();
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
        public void Can_Map_PaymentMethodKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PaymentMethodKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[paymentMethodKey]"));
        }

        [Test]
        public void Can_Map_PaymentMethodName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PaymentMethodName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[paymentMethodName]"));
        }

        [Test]
        public void Can_Map_ReferenceNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ReferenceNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[referenceNumber]"));
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
        public void Can_Map_Authoized_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Authorized");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[authorized]"));
        }

        [Test]
        public void Can_Map_Collected_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Collected");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[collected]"));
        }

        [Test]
        public void Can_Map_Voided_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Voided");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[voided]"));
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