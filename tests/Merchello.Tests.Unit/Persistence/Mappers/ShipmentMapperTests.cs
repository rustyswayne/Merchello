namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ShipmentMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchShipment";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ShipmentMapper();
        }

        [Test]
        public void Can_Map_ShipmentNumberPrefix_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipmentNumberPrefix");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipmentNumberPrefix]"));
        }

        [Test]
        public void Can_Map_ShipmentNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipmentNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipmentNumber]"));
        }

        [Test]
        public void Can_Map_ShipmentStatusKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipmentStatusKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipmentStatusKey]"));
        }

        [Test]
        public void Can_Map_ShippedDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShippedDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shippedDate]"));
        }

        [Test]
        public void Can_Map_FromOrganization_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromOrganization");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromOrganization]"));
        }

        [Test]
        public void Can_Map_FromName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromName]"));
        }

        [Test]
        public void Can_Map_FromAddress1_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromAddress1");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromAddress1]"));
        }

        [Test]
        public void Can_Map_FromAddress2_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromAddress2");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromAddress2]"));
        }

        [Test]
        public void Can_Map_FromLocality_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromLocality");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromLocality]"));
        }

        [Test]
        public void Can_Map_FromRegion_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromRegion");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromRegion]"));
        }

        [Test]
        public void Can_Map_FromPostalCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromPostalCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromPostalCode]"));
        }

        [Test]
        public void Can_Map_FromCountryCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromCountryCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromCountryCode]"));
        }

        [Test]
        public void Can_Map_FromIsCommercial_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromIsCommercial");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromIsCommercial]"));
        }

        [Test]
        public void Can_Map_ToOrganization_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToOrganization");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toOrganization]"));
        }

        [Test]
        public void Can_Map_ToName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toName]"));
        }

        [Test]
        public void Can_Map_ToAddress1_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToAddress1");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toAddress1]"));
        }

        [Test]
        public void Can_Map_ToAddress2_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToAddress2");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toAddress2]"));
        }

        [Test]
        public void Can_Map_ToLocality_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToLocality");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toLocality]"));
        }

        [Test]
        public void Can_Map_ToRegion_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToRegion");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toRegion]"));
        }

        [Test]
        public void Can_Map_ToCountryCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToCountryCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toCountryCode]"));
        }

        [Test]
        public void Can_Map_ToIscommercial_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ToIsCommercial");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[toIsCommercial]"));
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
        public void Can_Map_ShipMethodKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ShipMethodKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shipMethodKey]"));
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
        public void Can_Map_Carrier_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Carrier");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[carrier]"));
        }

        [Test]
        public void Can_Map_TrackingCode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TrackingCode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[trackingCode]"));
        }
    }
}