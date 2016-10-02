namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class AuditLogMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchAuditLog";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new AuditLogMapper();
        }

        [Test]
        public void Can_Map_EntityKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "EntityKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[entityKey]"));
        }

        [Test]
        public void Can_Map_EntityTfKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "EntityTfKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[entityTfKey]"));
        }

        [Test]
        public void Can_Map_Message_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Message");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[message]"));
        }

        [Test]
        public void Can_Map_Verbosity_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Verbosity");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[verbosity]"));
        }

        [Test]
        public void Can_Map_IsError_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "IsError");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[isError]"));
        }
    }
}