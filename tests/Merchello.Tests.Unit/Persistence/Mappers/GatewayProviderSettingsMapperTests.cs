namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class GatewayProviderSettingsMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchGatewayProviderSettings";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new GatewayProviderSettingsMapper();
        }

        [Test]
        public void Can_Map_ProviderTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProviderTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[providerTfKey]"));
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
        public void Can_Map_EncryptExtendedData_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "EncryptExtendedData");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[encryptExtendedData]"));
        }
    }
}