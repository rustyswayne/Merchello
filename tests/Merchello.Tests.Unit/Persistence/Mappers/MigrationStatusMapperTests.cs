namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class MigrationStatusMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchMigrationStatus";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new MigrationStatusMapper();
        }

        [Test]
        public void Can_Map_MigrationName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "MigrationName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[name]"));
        }

        [Test]
        public void Can_Map_Version_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Version");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[version]"));
        }
    }
}