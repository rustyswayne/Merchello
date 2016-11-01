namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class ProductOptionService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IProductOptionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOptionService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public ProductOptionService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IProductOption GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var po = repo.Get(key);
                uow.Complete();
                return po;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IProductOption> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var options = repo.GetAll(keys);
                uow.Complete();
                return options;
            }
        }

        /// <inheritdoc/>
        public IProductAttribute GetProductAttributeByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var pa = repo.GetProductAttributeByKey(key);
                uow.Complete();
                return pa;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IProductAttribute> GetProductAttributes(IEnumerable<Guid> keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var atts = repo.GetProductAttributes(keys.ToArray());
                uow.Complete();
                return atts;
            }
        }

        /// <inheritdoc/>
        public IProductOptionUseCount GetProductOptionUseCount(IProductOption option)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var counts = repo.GetProductOptionUseCount(option);
                uow.Complete();
                return counts;
            }
        }

        /// <inheritdoc/>
        public int GetProductOptionShareCount(IProductOption option)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var count = repo.GetSharedProductOptionCount(option.Key);
                uow.Complete();
                return count;
            }
        }

        /// <summary>
        /// Ensures the option is safe to delete.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the option can be deleted.
        /// </returns>
        private bool EnsureSafeOptionDelete(IProductOption option)
        {
            var count = GetProductOptionShareCount(option);
            return option.Shared ? count == 0 : count == 1;
        }
    }
}