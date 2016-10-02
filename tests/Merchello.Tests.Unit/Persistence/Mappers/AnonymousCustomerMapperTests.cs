namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class AnonymousCustomerMapperTests : EntityKeyMapperTestBase
    {

        protected override string TableName
        {
            get
            {
                return "merchAnonymousCustomer";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new AnonymousCustomerMapper();
        }

        [Test]
        public void Can_Map_LastActivityDate_Property()
        {
            //// Act
            var column = new AnonymousCustomerMapper().Map(SqlSyntax, "LastActivityDate");

            //// Assert
            Assert.That(column, Is.EqualTo("[merchAnonymousCustomer].[lastActivityDate]"));
        }
    }
}