namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class ItemCacheService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IItemCacheService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCacheService"/> class.
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
        public ItemCacheService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCache)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCacheType, Guid versionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetItemCaches(Guid entityKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetEntityItemCaches(Guid entityKey, Guid itemCacheTfKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IItemCache> GetItemCacheByCustomer(ICustomerBase customer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheByCustomer(ICustomerBase customer, Guid itemCacheTfKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IItemCache GetItemCacheByCustomer(ICustomerBase customer, ItemCacheType itemCacheType)
        {
            throw new NotImplementedException();
        }
    }
}