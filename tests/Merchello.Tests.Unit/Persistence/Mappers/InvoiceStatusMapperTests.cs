namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceStatusMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchInvoiceStatus";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new InvoiceStatusMapper();
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

        [Test]
        public void Can_Map_Reportable_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Reportable");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[reportable]"));
        }

        [Test]
        public void Can_Map_Active_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Active");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[active]"));
        }

        [Test]
        public void Can_Map_SortOrder_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "SortOrder");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[sortOrder]"));
        }
    }
}