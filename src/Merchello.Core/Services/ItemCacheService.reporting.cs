namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class ItemCacheService : IItemCacheService
    {
        /// <inheritdoc/>
        public PagedCollection<IItemCache> GetCustomerItemCachePage(ItemCacheType itemCacheType, DateTime startDate, DateTime endDate, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {

            var tfKey = EnumTypeFieldConverter.ItemItemCache.GetTypeField(itemCacheType).TypeKey;

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var result = repo.GetItemCachePage(tfKey, startDate, endDate, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return result;
            }
        }

        /// <inheritdoc/>
        public int Count(ItemCacheType itemCacheType, CustomerType customerType)
        {
            var dtMin = DateTime.MinValue.SqlDateTimeMinValueAsDateTimeMinValue();
            var dtMax = DateTime.MaxValue.SqlDateTimeMaxValueAsSqlDateTimeMaxValue();
            return Count(itemCacheType, customerType, dtMin, dtMax);
        }

        /// <inheritdoc/>
        public int Count(ItemCacheType itemCacheType, CustomerType customerType, DateTime startDate, DateTime endDate)
        {
            var tfKey = EnumTypeFieldConverter.ItemItemCache.GetTypeField(itemCacheType).TypeKey;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IItemCacheRepository>();
                var count = repo.Count(tfKey, customerType, startDate, endDate);
                uow.Complete();
                return count;
            }
        }
    }
}
