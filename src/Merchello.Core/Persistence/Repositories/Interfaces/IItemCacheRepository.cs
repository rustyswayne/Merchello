namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IItemCache"/> entities.
    /// </summary>
    public interface IItemCacheRepository : INPocoEntityRepository<IItemCache>
    {
        /// <summary>
        /// Gets a paged collection of <see cref="IItemCache"/>.
        /// </summary>
        /// <param name="itemCacheTfKey">
        /// The item cache type field key.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        /// <remarks>
        /// Used in maintenance operations such as deleting anonymous customer baskets after a time.
        /// </remarks>
        PagedCollection<IItemCache> GetItemCachePage(Guid itemCacheTfKey, DateTime startDate, DateTime endDate, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets the count of <see cref="IItemCache"/> created within a date range.
        /// </summary>
        /// <param name="itemCacheTfKey">
        /// The item cache type field key.
        /// </param>
        /// <param name="customerType">
        /// The customer type.
        /// </param>
        /// <param name="startDate">
        /// The start date.
        /// </param>
        /// <param name="endDate">
        /// The end date.
        /// </param>
        /// <returns>
        /// The count of the <see cref="IItemCache"/>.
        /// </returns>
        int Count(Guid itemCacheTfKey, CustomerType customerType, DateTime startDate, DateTime endDate);
    }
}