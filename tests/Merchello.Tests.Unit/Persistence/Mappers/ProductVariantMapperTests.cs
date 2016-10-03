namespace Merchello.Tests.Unit.Persistence.Mappers
{
    using Merchello.Core.Persistence.Mappers;

    using NUnit.Framework;

    [TestFixture]
    public class ProductVariantMapperTests : EntityKeyMapperTestBase
    {
        protected override string TableName
        {
            get
            {
                return "merchProductVariant";
            }
        }

        protected override BaseMapper GetMapper()
        {
            return new ProductVariantMapper();
        }

        [Test]
        public void Can_Map_ProductKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ProductKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[productKey]"));
        }

        [Test]
        public void Can_Map_Sku_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Sku");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[sku]"));
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
        public void Can_Map_Price_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Price");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[price]"));
        }

        [Test]
        public void Can_Map_CostOfGoods_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "CostOfGoods");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[costOfGoods]"));
        }

        [Test]
        public void Can_Map_SalePrice_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "SalePrice");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[salePrice]"));
        }

        [Test]
        public void Can_Map_OnSale_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OnSale");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[onSale]"));
        }

        [Test]
        public void Can_Map_Manufacturer_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Manufacturer");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[manufacturer]"));
        }

        [Test]
        public void Can_Map_ModelNumber_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "ManufacturerModelNumber");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[modelNumber]"));
        }

        [Test]
        public void Can_Map_Weight_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Weight");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[weight]"));
        }

        [Test]
        public void Can_Map_Height_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Height");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[height]"));
        }

        [Test]
        public void Can_Map_Width_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Width");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[width]"));
        }

        [Test]
        public void Can_Map_Barcode_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Barcode");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[barcode]"));
        }

        [Test]
        public void Can_Map_Available_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Available");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[available]"));
        }

        [Test]
        public void Can_Map_TrackInventory_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "TrackInventory");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[trackInventory]"));
        }

        [Test]
        public void Can_Map_OutOfStockPurchase_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "OutOfStockPurchase");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[outOfStockPurchase]"));
        }

        [Test]
        public void Can_Map_Taxable_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Taxable");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[taxable]"));
        }

        [Test]
        public void Can_Map_Shippable_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Shippable");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[shippable]"));
        }

        [Test]
        public void Can_Map_Download_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Download");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[download]"));
        }

        [Test]
        public void Can_Map_DownloadMediaId_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "DownloadMediaId");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[downloadMediaId]"));
        }

        [Test]
        public void Can_Map_Master_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "Master");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[master]"));
        }

        [Test]
        public void Can_Map_VersionKey_Property()
        {
            //// Act
            var column = GetMapper().Map(SqlSyntax, "VersionKey");

            //// Assert
            Assert.That(column, Is.EqualTo($"[{TableName}].[versionKey]"));
        }
    }
}