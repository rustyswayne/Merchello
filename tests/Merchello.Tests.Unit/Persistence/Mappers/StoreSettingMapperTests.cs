namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class StoreSettingMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchStoreSetting";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new StoreSettingMapper();
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
        public void Can_Map_Value_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Value");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[value]"));
        }


        [Test]
        public void Can_Map_TypeName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TypeName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[typeName]"));
        }
    }
}