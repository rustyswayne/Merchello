namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache.Policies;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public void CreateAttributeAssociationForProductVariant(IProductVariant variant)
        {
            // insert associations for every attribute
            foreach (var association in variant.Attributes.Select(att => new ProductVariant2ProductAttributeDto()
            {
                ProductVariantKey = variant.Key,
                OptionKey = att.OptionKey,
                ProductAttributeKey = att.Key,
                UpdateDate = DateTime.Now,
                CreateDate = DateTime.Now
            }))
            {
                Database.Insert(association);
            }

            var sharedOptions = GetProductOptions(variant.Attributes.Select(x => x.OptionKey).ToArray(), true);
            var clearKeys = GetProductKeysForCacheRefresh(sharedOptions.Select(x => x.Key).ToArray());
            ((ProductOptionRepositoryCachePolicy)CachePolicy).ClearProductWithSharedOption(clearKeys);
        }

        /// <inheritdoc/>
        public IProductAttribute GetProductAttributeByKey(Guid key)
        {
            return GetProductAttributes(new[] { key }).FirstOrDefault();
        }

        /// <inheritdoc/>
        public IEnumerable<IProductAttribute> GetProductAttributes(Guid[] attributeKeys)
        {
            //// FYI return attributeKeys.Select(key => this.GetStashed(key, this.GetAttributeByKey));
            //// REFACTOR consider Attribute cache as Isolated cache
            return attributeKeys.Select(GetAttributeByKey);
        }

        /// <inheritdoc/>
        public ProductAttributeCollection GetProductAttributeCollection(Guid optionKey)
        {
            var sql = Sql().SelectAll()
                .From<ProductAttributeDto>()
                .Where<ProductAttributeDto>(x => x.OptionKey == optionKey)
                .OrderBy<ProductAttributeDto>(x => x.SortOrder);

            return GetProductAttributeCollection(sql);
        }

        /// <inheritdoc/>
        public ProductAttributeCollection GetProductAttributeCollectionForVariant(Guid productVariantKey)
        {
            var sql = Sql().SelectAll()
                .From<ProductVariant2ProductAttributeDto>()
                .InnerJoin<ProductAttributeDto>()
                .On<ProductVariant2ProductAttributeDto, ProductAttributeDto>(left => left.ProductAttributeKey, right => right.Key)
                .Where<ProductVariant2ProductAttributeDto>(x => x.ProductVariantKey == productVariantKey);

            var dtos = Database.Fetch<ProductVariant2ProductAttributeDto>(sql);

            var factory = new ProductAttributeFactory();
            var collection = new ProductAttributeCollection();
            foreach (var dto in dtos)
            {
                var attribute = factory.BuildEntity(dto.ProductAttributeDto);

                //// REFACTOR clear isolated cache
                //// RuntimeCache.GetCacheItem(Cache.CacheKeys.GetEntityCacheKey<IProductAttribute>(attribute.Key), () => attribute);
                collection.Add(attribute);
            }

            return collection;
        }

        /// <inheritdoc/>
        public void DeleteAllProductVariantAttributes(IProductVariant variant)
        {
            //// This needs to delete all attributes from the merchProductVariant2ProductAttribute table.

            var sharedOptions = GetProductOptions(variant.Attributes.Select(x => x.OptionKey).Distinct().ToArray(), true);

            Database.Execute("DELETE FROM merchProductVariant2ProductAttribute WHERE productVariantKey = @key", new { @key = variant.Key });

            ((ProductOptionRepositoryCachePolicy)CachePolicy).ClearProductWithSharedOption(GetProductKeysForCacheRefresh(sharedOptions.Select(x => x.Key).ToArray()));
        }

        /// <inheritdoc/>
        public void UpdateAttribute(IProductAttribute attribute)
        {
            //// REVIEW - for deploy
            if (!attribute.HasIdentity)
            {
                var invalid = new InvalidOperationException("Cannot update an attribute that does not have an identity");
                MultiLogHelper.Error<ProductOptionRepository>("Attempt to update a new attribute", invalid);
                throw invalid;
            }

            var factory = new ProductAttributeFactory();
            var dto = factory.BuildDto(attribute);
            Database.Update(dto);
            //// REFACTOR isolated cache - Stash(attribute);
            //// RuntimeCache.ClearCacheItem(Cache.CacheKeys.GetEntityCacheKey<IProductOption>(attribute.OptionKey));
        }

        /// <summary>
        /// Gets a <see cref="IProductAttribute"/> by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IProductAttribute"/>.
        /// </returns>
        private IProductAttribute GetAttributeByKey(Guid key)
        {
            var sql = Sql().SelectAll()
                .From<ProductAttributeDto>()
                .Where<ProductAttributeDto>(x => x.Key == key);

            var dto = Database.Fetch<ProductAttributeDto>(sql).FirstOrDefault();
            if (dto != null)
            {
                var factory = new ProductAttributeFactory();
                return factory.BuildEntity(dto);
            }

            return null;
        }

        /// <summary>
        /// Gets the <see cref="ProductAttributeCollection"/> for a specific product.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="ProductAttributeCollection"/>.
        /// </returns>
        private ProductAttributeCollection GetProductAttributeCollection(IProductOption option, Guid productKey)
        {
            var sql = Sql();

            if (option.Shared)
            {
                sql.SelectAll()
                    .From<ProductAttributeDto>()
                    .InnerJoin<ProductOptionAttributeShareDto>()
                    .On<ProductAttributeDto, ProductOptionAttributeShareDto>(
                        left => left.Key,
                        right => right.AttributeKey)
                    .Where<ProductOptionAttributeShareDto>(x => x.ProductKey == productKey && x.OptionKey == option.Key)
                    .OrderBy<ProductAttributeDto>(x => x.SortOrder);
            }
            else
            {
                sql.SelectAll()
                    .From<ProductAttributeDto>()
                    .Where<ProductAttributeDto>(x => x.OptionKey == option.Key)
                    .OrderBy<ProductAttributeDto>(x => x.SortOrder);
            }

            return GetProductAttributeCollection(sql);
        }

        /// <summary>
        /// Gets the the <see cref="ProductAttributeCollection"/> by SQL.
        /// </summary>
        /// <param name="sql">
        /// The SQL.
        /// </param>
        /// <returns>
        /// The <see cref="ProductAttributeCollection"/>.
        /// </returns>
        private ProductAttributeCollection GetProductAttributeCollection(Sql sql)
        {
            var dtos = Database.Fetch<ProductAttributeDto>(sql);

            var attributes = new ProductAttributeCollection();
            var factory = new ProductAttributeFactory();

            foreach (var dto in dtos.OrderBy(x => x.SortOrder))
            {
                // FYI var attribute = Stash(factory.BuildEntity(dto));
                var attribute = factory.BuildEntity(dto);
                attributes.Add(attribute);
            }

            return attributes;
        }


        /// <summary>
        /// Gets a collection of .
        /// </summary>
        /// <param name="sharedOptionKeys">
        /// The shared option keys.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Guid}"/>.
        /// </returns>
        private IEnumerable<Guid> GetProductKeysForCacheRefresh(Guid[] sharedOptionKeys)
        {
            if (!sharedOptionKeys.Any()) return Enumerable.Empty<Guid>();

            var sql = Sql().SelectAll()
                .From<Product2ProductOptionDto>()
                .Where("optionKey IN (@keys)", new { @keys = sharedOptionKeys });

            var dtos = Database.Fetch<Product2ProductOptionDto>(sql);

            return dtos.Select(x => x.ProductKey);
        }
    }
}
