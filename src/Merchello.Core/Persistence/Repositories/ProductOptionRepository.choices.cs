namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Guid> CreateAttributeAssociationForProductVariant(IProductVariant variant)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductAttribute GetProductAttributeByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IProductAttribute> GetProductAttributes(Guid[] attributeKeys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ProductAttributeCollection GetProductAttributeCollectionForVariant(Guid productVariantKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Guid> DeleteAllProductVariantAttributes(IProductVariant variant)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void UpdateAttribute(IProductAttribute attribute)
        {
            throw new NotImplementedException();
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
    }
}
