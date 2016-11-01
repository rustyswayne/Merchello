namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class ProductService : EntityCollectionEntityServiceBase<IProduct, IDatabaseBulkUnitOfWorkProvider, IProductRepository>, IProductService
    {
        /// <summary>
        /// The <see cref="IProductOptionService"/>.
        /// </summary>
        private readonly IProductOptionService _productOptionService;

        /// <summary>
        /// The <see cref="IStoreSettingService"/>.
        /// </summary>
        private readonly IStoreSettingService _storeSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseBulkUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        /// <param name="storeSettingsService">
        /// The <see cref="IStoreSettingService"/>.
        /// </param>
        /// <param name="productOptionService">
        /// The <see cref="IProductOptionService"/>
        /// </param>
        public ProductService(IDatabaseBulkUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory, IStoreSettingService storeSettingsService, IProductOptionService productOptionService)
            : base(provider, logger, eventMessagesFactory, Constants.Locks.ProductTree)
        {
            Ensure.ParameterNotNull(productOptionService, nameof(productOptionService));
            Ensure.ParameterNotNull(storeSettingsService, nameof(StoreSettingService));
            _productOptionService = productOptionService;
        }

        /// <inheritdoc/>
        public IProduct GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductRepository>();
                var p = repo.Get(key);
                uow.Complete();
                return p;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IProduct> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the default currency code.
        /// </summary>
        /// <returns>
        /// The currency code.
        /// </returns>
        private string GetDefaultCurrencyCode()
        {
            return _storeSettingService.GetByKey(Constants.StoreSetting.CurrencyCodeKey).Value;
        }
    }
}
