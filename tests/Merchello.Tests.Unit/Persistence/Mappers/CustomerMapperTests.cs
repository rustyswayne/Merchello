namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class CustomerMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchCustomer";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new CustomerMapper();
        }

        [Test]
        public void Can_Map_LoginName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "LoginName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[loginName]"));
        }

        [Test]
        public void Can_Map_FirstName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "FirstName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[firstName]"));
        }

        [Test]
        public void Can_Map_LastName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "LastName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[lastName]"));
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
        public void Can_Map_TaxExempt_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TaxExempt");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[taxExempt]"));
        }

        [Test]
        public void Can_Map_LastActivityDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "LastActivityDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[lastActivityDate]"));
        }
    }
}