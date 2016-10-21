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
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOptionRepository"/> class.
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        public ProductOptionRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers)
            : base(work, cache, logger, mappers)
        {
        }

        /// <inheritdoc/>
        public void UpdateForDetachedContentDelete(Guid detachedContentTypeKey)
        {
            Database.Execute(
                "UPDATE merchProductOption SET detachedContentTypeKey = NULL WHERE detachedContentTypeKey = @Key",
                new { @Key = detachedContentTypeKey });

            // FYI Even if we wind up using a NullCacheProvider we still need to clear the cache of all products that 
            // use shared options - so we need a custom cache policy here!!!
            CachePolicy.ClearAll();
        }

        /// <summary>
        /// Gets a collection of options for a specific <see cref="IProduct"/>.
        /// </summary>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProduct}"/>.
        /// </returns>
        private IEnumerable<IProductOption> GetByProductKey(Guid productKey)
        {
            var sql = Sql().SelectAll()
               .From<ProductOptionDto>()
               .InnerJoin<Product2ProductOptionDto>()
               .On<ProductOptionDto, Product2ProductOptionDto>(left => left.Key, right => right.OptionKey)
               .Where<Product2ProductOptionDto>(x => x.ProductKey == productKey);

            var dtos = Database.Fetch<ProductOptionDto>(sql);

            var factory = new ProductOptionFactory();

            if (!dtos.Any()) return Enumerable.Empty<IProductOption>();

            var options = new List<IProductOption>();

            foreach (var option in dtos.OrderBy(x => x.Product2ProductOptionDto.SortOrder).Select(dto => factory.BuildEntity(dto)))
            {
                option.Choices = this.GetProductAttributeCollection(option, productKey);
                options.Add(option);
            }

            return options;
        }

        /// <inheritdoc/>
        protected override ProductOptionFactory GetFactoryInstance()
        {
            return new ProductOptionFactory();
        }
    }
}