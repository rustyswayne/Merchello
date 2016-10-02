namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.SqlSyntax;
    using Merchello.Umbraco.Adapters.Persistence;

    using NUnit.Framework;

    public abstract class EntityMapperTestBase
    {

        protected EntityMapperTestBase()
        {
            this.SqlSyntax = new SqlSyntaxProviderAdapter(new SqlCeSyntaxProvider());
        }

        protected ISqlSyntaxProviderAdapter SqlSyntax;

        protected abstract string TableName { get; }

        protected abstract BaseMapper GetMapper();

        [Test]
        public void Can_Map_CreateDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CreateDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[createDate]"));
        }

        [Test]
        public void Can_Map_UpdateDate_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "UpdateDate");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[updateDate]"));
        }
    }
}