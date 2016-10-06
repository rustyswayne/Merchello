namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IProductVariantRepository
    {
        /// <summary>
        /// The <see cref="IProductOptionRepository"/>.
        /// </summary>
        private readonly IProductOptionRepository _productOptionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVariantRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        /// <param name="productOptionRepository">
        /// The <see cref="IProductOptionRepository"/>
        /// </param>
        public ProductVariantRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IProductOptionRepository productOptionRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(productOptionRepository, nameof(productOptionRepository));
            _productOptionRepository = productOptionRepository;
        }

        /// <inheritdoc/>
        public IProductVariant GetProductVariantWithAttributes(IProduct product, Guid[] attributeKeys)
        {
            var variants = GetByProductKey(product.Key);
            return variants.FirstOrDefault(x => x.Attributes.Count() == attributeKeys.Count() && attributeKeys.All(key => x.Attributes.FirstOrDefault(att => att.Key == key) != null));
        }

        /// <inheritdoc/>
        public bool ProductVariantWithAttributesExists(IProduct product, ProductAttributeCollection attributes)
        {
            var variants = GetByProductKey(product.Key).ToArray();

            var keys = attributes.Select(x => x.Key);
            return variants.Any(x => x.Attributes.All(z => keys.Contains(z.Key)));
        }

        /// <inheritdoc/>
        public IEnumerable<IProductVariant> GetByProductKey(Guid productKey)
        {
            var query = Query.Where(x => x.ProductKey == productKey && x.Master == false);
            return GetByQuery(query);
        }

        /// <inheritdoc/>
        public ProductVariantCollection GetProductVariantCollection(Guid productKey)
        {
            var collection = new ProductVariantCollection();
            var variants = GetByProductKey(productKey);

            foreach (var variant in variants.Where(variant => variant != null))
            {
                collection.Add(variant);
            }

            return collection;
        }

        /// <inheritdoc/>
        public bool SkuExists(string sku)
        {
            var sql = Sql()
                        .SelectCount()
                        .From<ProductVariantDto>()
                        .Where<ProductVariantDto>(x => x.Sku.Equals(sku));

            return Database.ExecuteScalar<int>(sql) > 0;
        }


        /// <summary>
        /// Ensures product variant rules.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool MandateProductVariantRules(IEnumerable<IProductVariant> entities)
        {
            var success = true;
            foreach (var entity in entities)
            {
                success = MandateProductVariantRules(entity);
                if (!success) break;
            }

            return success;
        }

        /// <summary>
        /// Ensures product variant rules.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// Returns true or throws exception.
        /// </returns>
        private static bool MandateProductVariantRules(IProductVariant entity)
        {
            if (entity.ProductKey.Equals(Guid.Empty)) throw new InvalidOperationException("ProductKey must be set.");
            if (!entity.Master && !entity.Attributes.Any()) throw new InvalidOperationException("Product variants must have attributes.");
            return true;
        }

        /// <summary>
        /// True/false indicating whether or not a SKU exists on a record other than the record with the id passed
        /// </summary>
        /// <param name="sku">The SKU to be tested</param>
        /// <param name="productVariantKey">The key of the <see cref="IProductVariant"/> to be excluded</param>
        /// <returns>A value indicating whether or not a SKU exists on a record other than the record with the id passed</returns>
        private bool SkuExists(string sku, Guid productVariantKey)
        {
            var sql = Sql().SelectCount()
                        .From<ProductVariantDto>()
                        .Where<ProductVariantDto>(x => x.Sku == sku && x.Key != productVariantKey);

            return Database.ExecuteScalar<int>(sql) > 0;
        }

        /// <summary>
        /// True/false indicating whether or not a SKU exists on a record other than the record with the id passed
        /// </summary>
        /// <param name="entities">The collection of the <see cref="IProductVariant"/> to be excluded</param>
        /// <returns>A value indicating whether or not a SKU exists on a record other than the record with the id passed</returns>
        private bool SkuExists(IEnumerable<IProductVariant> entities)
        {
            var sql = Sql().SelectAll()
                .From<ProductVariantDto>();

            var whereClauses = entities.Select(entity => $"(Sku = '{entity.Sku}' and pk != '{entity.Key}')").ToList();

            sql = sql.Where(string.Join(" OR ", whereClauses), null);

            return Database.Fetch<ProductAttributeDto>(sql).Any();
        }
    }
}