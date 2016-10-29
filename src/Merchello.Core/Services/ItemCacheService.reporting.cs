namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ItemCacheService : IItemCacheService
    {
        /// <inheritdoc/>
        public PagedCollection<IItemCache> GetCustomerItemCachePage(ItemCacheType itemCacheType, DateTime startDate, DateTime endDate, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Count(ItemCacheType itemCacheType, CustomerType customerType)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Count(ItemCacheType itemCacheType, CustomerType customerType, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
