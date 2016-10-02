namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    public class NoteMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchNote";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new NoteMapper();
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
        public void Can_Map_Author_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Author");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[author]"));
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
        public void Can_Map_InternalOnly_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "InternalOnly");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[internalOnly]"));
        }
    }
}