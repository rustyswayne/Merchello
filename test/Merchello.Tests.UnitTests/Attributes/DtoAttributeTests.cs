namespace Merchello.Tests.UnitTests.Attributes
{
    using Merchello.Core.Models.Rdbms;

    using NUnit.Framework;

    [TestFixture]
    public class DtoAttributeTests
    {
        [Test]
        public void Can_Get_A_List_Of_All_Properties_With_Attributes()
        {
            // Arrange
            var dto = new ProductVariantDto();
            var dtoType = dto.GetType();
            // Act
            var tableAttribute = dtoType.GetCustomAttributes(TypeOf(), false)
        }
    }
}