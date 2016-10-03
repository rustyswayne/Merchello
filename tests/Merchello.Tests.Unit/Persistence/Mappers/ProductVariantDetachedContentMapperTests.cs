namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ProductVariantDetachedContentMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchProductVariantDetachedContent";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ProductVariantDetachedContentMapper();
        }


        [Test]
        public void Can_Map_ProductVariantKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProductVariantKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[productVariantKey]"));
        }


        [Test]
        public void Can_Map_CultureName_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CultureName");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[cultureName]"));
        }

        [Test]
        public void Can_Map_TemplateId_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TemplateId");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[templateId]"));
        }

        [Test]
        public void Can_Map_Slug_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Slug");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[slug]"));
        }

        [Test]
        public void Can_Map_CanBeRendered_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CanBeRendered");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[canBeRendered]"));
        }
    }
}