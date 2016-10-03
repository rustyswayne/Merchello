namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class WarehouseMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchWarehouse";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new WarehouseMapper();
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
        public void Can_Map_Address1_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Address1");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[address1]"));
        }

        [Test]
        public void Can_Map_Address2_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Address2");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[address2]"));
        }

        [Test]
        public void Can_Map_Locality_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Locality");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[locality]"));
        }

        [Test]
        public void Can_Map_Region_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Region");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[region]"));
        }

        [Test]
        public void Can_Map_PostalCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "PostalCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[postalCode]"));
        }

        [Test]
        public void Can_Map_CountryCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CountryCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[countryCode]"));
        }

        [Test]
        public void Can_Map_Phone_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Phone");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[phone]"));
        }

        [Test]
        public void Can_Map_Email_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Email");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[email]"));
        }

        [Test]
        public void Can_Map_IsDefault_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "IsDefault");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[isDefault]"));
        }
    }
}