namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class PaymentMethodMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchPaymentMethod";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new PaymentMethodMapper();
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
        public void Can_Map_Description_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Description");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[description]"));
        }

        [Test]
        public void Can_Map_PaymentCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PaymentCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[paymentCode]"));
        }
    }
}