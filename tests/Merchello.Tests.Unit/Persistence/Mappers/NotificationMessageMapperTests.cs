namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    public class NotificationMessageMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchNotificationMessage";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new NotificationMessageMapper();
        }

        [Test]
        public void Can_Map_MethodKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "MethodKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[methodKey]"));
        }

        [Test]
        public void Can_Map_MonitorKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "MonitorKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[monitorKey]"));
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
        public void Can_Map_FromAddress_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FromAddress");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[fromAddress]"));
        }

        [Test]
        public void Can_Map_ReplyTo_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ReplyTo");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[replyTo]"));
        }

        [Test]
        public void Can_Map_BodyText_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BodyText");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[bodyText]"));
        }

        [Test]
        public void Can_Map_MaxLength_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "MaxLength");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[maxLength]"));
        }

        [Test]
        public void Can_Map_BodyTextIsFilePath_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "BodyTextIsFilePath");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[bodyTextIsFilePath]"));
        }

        [Test]
        public void Can_Map_Recipients_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Recipients");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[recipients]"));
        }

        [Test]
        public void Can_Map_SendToCustomer_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "SendToCustomer");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[sendToCustomer]"));
        }

        [Test]
        public void Can_Map_Disabled_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Disabled");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[disabled]"));
        }
    }
}