namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.SqlSyntax;
    using Merchello.Umbraco.Adapters.Persistence;

    using NUnit.Framework;

    [TestFixture]
    public class TypeFieldMapperTests
    {
         protected ISqlSyntaxProviderAdapter SqlSyntax = new SqlSyntaxProviderAdapter(new SqlCeSyntaxProvider());

        protected string TableName
        {
            get
            {
                return "merchTypeField";
            }
        }

        protected BaseMapper GetMapper()
        {
            return new TypeFieldMapper();
        }

        [Test]
        public void Can_Map_TypeKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TypeKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[pk]"));
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