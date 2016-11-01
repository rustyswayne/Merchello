namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Merchello.Core.Configuration;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;

    using NodaMoney;

    public partial class ProductService : IProductVariantService
    {
        public IProductVariant GetProductVariantByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IProductVariant GetProductVariantBySku(string sku)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProductVariant> GetAllProductVariants(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProductVariant> GetProductVariantsByProductKey(Guid productKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProductVariant> GetProductVariantsByWarehouseKey(Guid warehouseKey)
        {
            throw new NotImplementedException();
        }

        public bool SkuExists(string sku)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a <see cref="IProductVariant"/> of the <see cref="IProduct"/> passed defined by the collection of <see cref="IProductAttribute"/>
        /// without saving it to the database
        /// </summary>
        /// <param name="product">The <see cref="IProduct"/></param>
        /// <param name="attributes">The <see cref="IProductVariant"/></param>
        /// <returns>Either a new <see cref="IProductVariant"/> or, if one already exists with associated attributes, the existing <see cref="IProductVariant"/></returns>
        internal IProductVariant CreateProductVariant(IProduct product, ProductAttributeCollection attributes)
        {
            var skuSeparator = MerchelloConfig.For.MerchelloSettings().Products.DefaultSkuSeparator;

            // verify the order of the attributes so that a sku can be constructed in the same order as the UI
            var optionIds = product.ProductOptionsForAttributes(attributes).OrderBy(x => x.SortOrder).Select(x => x.Key).Distinct();

            // the base sku
            var sku = product.Sku;
            var name = $"{product.Name} - ";

            foreach (var att in optionIds.Select(key => attributes.FirstOrDefault(x => x.OptionKey == key)).Where(att => att != null))
            {
                name += att.Name + " ";

                sku += skuSeparator + (string.IsNullOrEmpty(att.Sku) ? Regex.Replace(att.Name, "[^0-9a-zA-Z]+", string.Empty) : att.Sku);
            }

            return CreateProductVariant(product, name, sku, product.Price, attributes);
        }


        /// <summary>
        /// Creates a <see cref="IProductVariant"/> of the <see cref="IProduct"/> passed defined by the collection of <see cref="IProductAttribute"/>
        /// without saving it to the database
        /// </summary>
        /// <param name="product">The <see cref="IProduct"/></param>
        /// <param name="name">The name of the product variant</param>
        /// <param name="sku">The unique SKU of the product variant</param>
        /// <param name="price">The price of the product variant</param>
        /// <param name="attributes">The <see cref="ProductAttributeCollection"/></param>        
        /// <returns>Either a new <see cref="IProductVariant"/> or, if one already exists with associated attributes, the existing <see cref="IProductVariant"/></returns>
        internal IProductVariant CreateProductVariant(IProduct product, string name, string sku, Money price, ProductAttributeCollection attributes)
        {
            Ensure.ParameterNotNull(product, "product");
            Ensure.ParameterNotNull(attributes, "attributes");
            Ensure.ParameterCondition(attributes.Count >= product.ProductOptions.Count(x => x.Required), "An attribute must be assigned for every required option");

            //// http://issues.merchello.com/youtrack/issue/M-740
            // verify there is not already a variant with these attributes
            ////Mandate.ParameterCondition(false == ProductVariantWithAttributesExists(product, attributes), "A ProductVariant already exists for the ProductAttributeCollection");
            if (this.ProductVariantWithAttributesExists(product, attributes))
            {
                Logger.Debug<ProductService>("Attempt to create a new variant that already exists.  Returning existing variant.");
                return this.GetProductVariantWithAttributes(product, attributes.Select(x => x.Key).ToArray());
            }

            return new ProductVariant(product.Key, attributes, name, sku, price)
            {
                CostOfGoods = product.CostOfGoods,
                SalePrice = product.SalePrice,
                OnSale = product.OnSale,
                Weight = product.Weight,
                Length = product.Length,
                Width = product.Width,
                Height = product.Height,
                Barcode = product.Barcode,
                Available = product.Available,
                Manufacturer = product.Manufacturer,
                ManufacturerModelNumber = product.ManufacturerModelNumber,
                TrackInventory = product.TrackInventory,
                OutOfStockPurchase = product.OutOfStockPurchase,
                Taxable = product.Taxable,
                Shippable = product.Shippable,
                Download = product.Download,
                VersionKey = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Returns <see cref="IProductVariant"/> given the product and the collection of attribute ids that defines the<see cref="IProductVariant"/>
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="attributeKeys">
        /// The attribute Keys.
        /// </param>
        /// <returns>
        /// The <see cref="IProductVariant"/>.
        /// </returns>
        private IProductVariant GetProductVariantWithAttributes(IProduct product, Guid[] attributeKeys)
        {
            throw new NotImplementedException();
            //using (var repository = RepositoryFactory.CreateProductVariantRepository(UowProvider.GetUnitOfWork()))
            //{
            //    return repository.GetProductVariantWithAttributes(product, attributeKeys);
            //}
        }


        /// <summary>
        /// Compares the <see cref="ProductAttributeCollection"/> with other <see cref="IProductVariant"/>s of the <see cref="IProduct"/> pass
        /// to determine if the a variant already exists with the attributes passed
        /// </summary>
        /// <param name="product">The <see cref="IProduct"/> to reference</param>
        /// <param name="attributes"><see cref="ProductAttributeCollection"/> to compare</param>
        /// <returns>True/false indicating whether or not a <see cref="IProductVariant"/> already exists with the <see cref="ProductAttributeCollection"/> passed</returns>
        private bool ProductVariantWithAttributesExists(IProduct product, ProductAttributeCollection attributes)
        {
            throw new NotImplementedException();
            //using (var repository = RepositoryFactory.CreateProductVariantRepository(UowProvider.GetUnitOfWork()))
            //{
            //    return repository.ProductVariantWithAttributesExists(product, attributes);
            //}
        }
    }
}
