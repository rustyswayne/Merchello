namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ItemCacheRepository : IItemCacheRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCacheRepository"/> class.
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
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public ItemCacheRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public PagedCollection<IItemCache> GetItemCachePage(Guid itemCacheTfKey, DateTime startDate, DateTime endDate, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            // REFACTOR - query should use HAVING COUNT and be simplified

            //sql.Append("SELECT T1.pk AS itemCacheKey");
            //sql.Append("FROM [merchItemCache] T1");
            //sql.Append("INNER JOIN [merchCustomer] T2 ON T1.entityKey = T2.pk");
            //sql.Append("INNER JOIN (");
            //sql.Append("SELECT	itemCacheKey,");
            //sql.Append("COUNT(*) AS itemCount");
            //sql.Append("FROM [merchItemCacheItem]");
            //sql.Append("GROUP BY itemCacheKey");
            //sql.Append(") Q1 ON T1.pk = Q1.itemCacheKey");
            //sql.Append("WHERE T1.itemCacheTfKey = @tfkey", new { @tfkey = itemCacheTfKey });
            //sql.Append("AND	T2.lastActivityDate BETWEEN @start AND @end", new { @start = startDate, @end = endDate });
            //sql.Append("AND Q1.itemCount > 0");
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Count(Guid itemCacheTfKey, CustomerType customerType, DateTime startDate, DateTime endDate)
        {
            // REFACTOR 
            //var table = customerType == CustomerType.Anonymous ? "[merchAnonymousCustomer]" : "[merchCustomer]";

            //var querySql = @"SELECT  COUNT(*) AS cacheCount  
            //    FROM	merchItemCache T1
            //    INNER JOIN (
            //     SELECT	pk
            //     FROM " + table + @"
            //     WHERE	lastActivityDate BETWEEN @start AND @end
            //    ) Q1 ON T1.entityKey = Q1.pk
            //    INNER JOIN (
            //     SELECT	COUNT(*) AS itemCount,
            //       itemCacheKey
            //     FROM	merchItemCacheItem
            //     GROUP BY itemCacheKey
            //    ) Q2 ON T1.pk = Q2.itemCacheKey
            //    WHERE Q2.itemCount > 0 AND
            //    T1.itemCacheTfKey = @tfKey";

            //var sql = new Sql(
            //    querySql,
            //    new { @table = table, @start = startDate, @end = endDate, @tfKey = itemCacheTfKey });

            //return Database.ExecuteScalar<int>(sql);

            throw new NotImplementedException();
        }
    }
}