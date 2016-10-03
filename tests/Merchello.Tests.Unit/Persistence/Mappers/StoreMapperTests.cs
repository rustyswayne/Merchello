namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class StoreMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchStore";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new StoreMapper();
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
        public void Can_Map_Alias_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Alias");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[alias]"));
        }
    }
}