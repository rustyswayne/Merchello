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
    internal partial class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The <see cref="IProductVariantRepository"/>.
        /// </summary>
        private readonly IProductVariantRepository _productVariantRepository;

        /// <summary>
        /// The <see cref="IProductOptionRepository"/>.
        /// </summary>
        private readonly IProductOptionRepository _productOptionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
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
        /// <param name="productVariantRepository">
        /// The <see cref="IProductVariantRepository"/>.
        /// </param>
        /// <param name="productOptionRepository">
        /// The <see cref="IProductOptionRepository"/>
        /// </param>
        public ProductRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers, IProductVariantRepository productVariantRepository, IProductOptionRepository productOptionRepository)
            : base(work, cache, logger, mappers)
        {
            Ensure.ParameterNotNull(productVariantRepository, nameof(productVariantRepository));
            Ensure.ParameterNotNull(productOptionRepository, nameof(productOptionRepository));

            _productVariantRepository = productVariantRepository;
            _productOptionRepository = productOptionRepository;
        }

        /// <inheritdoc/>
        /// TEST - this query is different from V2!!!
        public IEnumerable<IProduct> GetByDetachedContentType(Guid detachedContentTypeKey)
        {
            var innerSql = Sql().SelectDistinct<ProductVariantDetachedContentDto>(x => x.ProductVariantKey)
                .From<ProductVariantDetachedContentDto>()
                .Where<ProductVariantDetachedContentDto>(x => x.DetachedContentTypeKey == detachedContentTypeKey);


            var sql = GetBaseQuery(false).SingleWhereIn<ProductVariantDto>(x => x.Key, innerSql);

            var dtos = Database.Fetch<ProductDto>(sql);


            return GetAll(dtos.Select(x => x.Key).ToArray());
        }

        /// <inheritdoc/>
        public IProduct GetKeyForSlug(string slug)
        {
            var sql = Sql().SelectAll()
                .From<ProductVariantDto>()
                .InnerJoin<ProductVariantDetachedContentDto>()
                .On<ProductVariantDto, ProductVariantDetachedContentDto>(left => left.Key, right => right.ProductVariantKey)
                .Where<ProductVariantDetachedContentDto>(x => x.Slug == slug);

            var dto = Database.First<ProductVariantDto>(sql);

            return Get(dto.ProductKey);
        }

        /// <inheritdoc/>
        public bool SkuExists(string sku)
        {
            return _productVariantRepository.SkuExists(sku);
        }

        /// <summary>
        /// Maps a collection of <see cref="ProductDto"/> to <see cref="IProduct"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IProduct}"/>.
        /// </returns>
        protected virtual IEnumerable<IProduct> MapDtoCollection(IEnumerable<ProductDto> dtos)
        {
            return GetAll(dtos.Select(dto => dto.Key).ToArray());
        }
    }
}