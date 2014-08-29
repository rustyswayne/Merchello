﻿namespace Merchello.Web.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Core;
    using Core.Models;
    using Core.Services;
    using Models.ContentEditing;    
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using WebApi;

    /// <summary>
    /// The product variant api controller.
    /// </summary>
    [PluginController("Merchello")]
    public class ProductVariantApiController : MerchelloApiController
    {
        /// <summary>
        /// The product variant service.
        /// </summary>
        private readonly IProductVariantService _productVariantService;

        /// <summary>
        /// The _product service.
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// The warehouse service.
        /// </summary>
        private readonly IWarehouseService _warehouseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantApiController"/> class. 
        /// Constructor
        /// </summary>
        public ProductVariantApiController()
            : this(Core.MerchelloContext.Current)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="merchelloContext"></param>
        public ProductVariantApiController(MerchelloContext merchelloContext)
            : base(merchelloContext)
        {
            _productService = MerchelloContext.Services.ProductService;
            _productVariantService = MerchelloContext.Services.ProductVariantService;
            _warehouseService = MerchelloContext.Services.WarehouseService;
        }

        /// <summary>
        /// This is a helper contructor for unit testing
        /// </summary>
        internal ProductVariantApiController(MerchelloContext merchelloContext, UmbracoContext umbracoContext)
            : base(merchelloContext, umbracoContext)
        {
            _productService = MerchelloContext.Services.ProductService;
            _productVariantService = MerchelloContext.Services.ProductVariantService;
            _warehouseService = MerchelloContext.Services.WarehouseService;
        }

        /// <summary>
        /// Returns Product by id (key)
        /// 
        /// GET /umbraco/Merchello/ProductVariantApi/GetProductVariant?id={Guid}
        /// </summary>
        /// <param name="id">ProductVariant Key</param>
        public ProductVariantDisplay GetProductVariant(Guid id)
        {
            var productVariant = _productVariantService.GetByKey(id);
            if (productVariant == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return productVariant.ToProductVariantDisplay();
        }

        /// <summary>
        /// Returns ProductVariants by Product key
        /// 
        /// GET /umbraco/Merchello/ProductVariantApi/GetByProduct?key={guid}
        /// </summary>
        /// <param name="id">Product Key</param>
        public IEnumerable<ProductVariantDisplay> GetByProduct(Guid id)
        {
            if (id != Guid.Empty)
            {
                var productVariants = _productVariantService.GetByProductKey(id);
                if (productVariants == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                foreach (IProductVariant productVariant in productVariants)
                {
                    yield return productVariant.ToProductVariantDisplay();
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Parameter key is null")),
                    ReasonPhrase = "Invalid Parameter"
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Returns Product by keys separated by a comma
        /// 
        /// GET /umbraco/Merchello/ProductVariantApi/GetProductVariants?ids={int}&amp;ids={int}
        /// </summary>
        /// <param name="ids">Product Variant Keys</param>
        public IEnumerable<ProductVariantDisplay> GetProductVariants([FromUri]IEnumerable<Guid> ids)
        {
            if (ids != null)
            {
                var productVariants = _productVariantService.GetByKeys(ids);
                if (productVariants != null)
                {
                    foreach (var productVariant in productVariants)
                    {
                        yield return productVariant.ToProductVariantDisplay();
                    }
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Parameter ids is null")),
                    ReasonPhrase = "Invalid Parameter"
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Returns ProductVariant combinations by Product key that can be created
        /// 
        /// GET /umbraco/Merchello/ProductVariantApi/GetVariantsByProductThatCanBeCreated?id={guid}
        /// </summary>
        /// <param name="id">Product Key</param>
        public IEnumerable<ProductVariantDisplay> GetVariantsByProductThatCanBeCreated(Guid id)
        {
            if (id != Guid.Empty)
            {
                var merchProduct = _productService.GetByKey(id);
                var productVariants = _productVariantService.GetProductVariantsThatCanBeCreated(merchProduct);

                foreach (var productVariant in productVariants)
                {
                    yield return productVariant.ToProductVariantDisplay();
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Parameter key is null")),
                    ReasonPhrase = "Invalid Parameter"
                };
                throw new HttpResponseException(resp);
            }
        }


        ///  <summary>
        ///  Creates a product variant from Product & Attributes
        /// 
        ///  POST /umbraco/Merchello/ProductVariantApi/NewProductVariant
        ///  </summary>
        /// <param name="productVariant">Product variant object serialized from JSON</param>
        [AcceptVerbs("GET", "POST")]
        public ProductVariantDisplay NewProductVariant(ProductVariantDisplay productVariant)
        {
            IProductVariant newProductVariant;

            try
            {
                var product = _productService.GetByKey(productVariant.ProductKey) as Product;

                if (product != null)
                {
                    var productAttributes = new ProductAttributeCollection();
                    foreach (var attribute in productVariant.Attributes)
                    {
                        // TODO: This should be refactored into an extension method
                        var productOption = product.ProductOptions.FirstOrDefault(x => x.Key == attribute.OptionKey) as ProductOption;

                        if (productOption != null) productAttributes.Add(productOption.Choices[attribute.Key]);
                    }

                    newProductVariant = _productVariantService.CreateProductVariantWithKey(product, productAttributes);

                    if (productVariant.TrackInventory)
                    {
                        newProductVariant.AddToCatalogInventory(_warehouseService.GetDefaultWarehouse().DefaultCatalog());
                    }

                    newProductVariant = productVariant.ToProductVariant(newProductVariant);

                    _productVariantService.Save(newProductVariant);
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

            return newProductVariant.ToProductVariantDisplay();
        }

        /// <summary>
        /// Updates an existing product
        /// 
        /// PUT /umbraco/Merchello/ProductVariantApi/PutProductVariant
        /// </summary>
        /// <param name="productVariant">
        /// ProductVariantDisplay object serialized from WebApi
        /// </param>
        /// <returns>
        /// The <see cref="ProductVariantDisplay"/>.
        /// </returns>
        [HttpPost, HttpPut]
        public ProductVariantDisplay PutProductVariant(ProductVariantDisplay productVariant)
        {
            
                IProductVariant merchProductVariant = _productVariantService.GetByKey(productVariant.Key);

                if (productVariant.TrackInventory && !merchProductVariant.CatalogInventories.Any())
                {
                    merchProductVariant.AddToCatalogInventory(_warehouseService.GetDefaultWarehouse().DefaultCatalog());
                }

                merchProductVariant = productVariant.ToProductVariant(merchProductVariant);

                _productVariantService.Save(merchProductVariant);
            

            return merchProductVariant.ToProductVariantDisplay();
        }

        /// <summary>
        /// Deletes an existing product variant
        ///
        /// DELETE /umbraco/Merchello/ProductVariantApi/DeleteVariant?id={key}
        /// </summary>
        /// <param name="id">Product Variant key</param>
        [AcceptVerbs("GET", "DELETE")]
        public HttpResponseMessage DeleteVariant(Guid id)
        {
            var productVariantToDelete = _productVariantService.GetByKey(id);
            if (productVariantToDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _productVariantService.Delete(productVariantToDelete);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Deletes all product variants for a specific product
        ///
        /// GET /umbraco/Merchello/ProductVariantApi/DeleteAllVariants?productkey={key}
        /// </summary>
        /// <param name="productkey">Product Variant key</param>
        [AcceptVerbs("GET","DELETE")]
        public HttpResponseMessage DeleteAllVariants(Guid productkey)
        {
            var productWithVariantsToDelete = _productService.GetByKey(productkey);
            if (productWithVariantsToDelete == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _productVariantService.Delete(productWithVariantsToDelete.ProductVariants);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
