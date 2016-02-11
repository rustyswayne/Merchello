namespace Merchello.Tests.UnitTests.Attributes
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Tests.Base.DataMakers;

    using NUnit.Framework;

    [TestFixture]
    public class DtoAttributeTests
    {
        [Test]
        public void Can_Get_ProductVariantDtos_ColumnValues()
        {
            // Arrange
            var factory = new ProductVariantFactory();
            var product = MockProductDataMaker.MockProductForInserting();
            var dto = factory.BuildDto(((Product)product).MasterVariant);
            
            // Act
            var columns = dto.ColumnValues();

            // Assert

            foreach (var c in columns)
            {
                var value = c.Item2 == null ? "null" : c.Item2.ToString();
                
                Console.WriteLine(c.Item1 + " " + value);
            }
        }
    }
}