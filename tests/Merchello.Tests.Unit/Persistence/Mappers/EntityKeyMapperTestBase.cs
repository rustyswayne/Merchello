namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.SqlSyntax;
    using Merchello.Umbraco.Adapters.Persistence;

    using NUnit.Framework;

    public abstract class EntityKeyMapperTestBase : EntityMapperTestBase
    {
      
        [Test]
        public void Can_Map_Key_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Key");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[pk]"));
        }
    }
}