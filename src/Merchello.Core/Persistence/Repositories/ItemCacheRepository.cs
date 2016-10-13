namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

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
            var icTbl = SqlSyntax.GetQuotedTableName("merchItemCacheItem");
            var ick = SqlSyntax.GetQuotedColumnName("itemCacheKey");
            var iciQ = SqlSyntax.GetQuotedName("itemCacheItemQ");

            var joinOn = string.Format(
                "{0}.{1} = {2}.{3}",
                SqlSyntax.GetQuotedName("itemCacheItemQ"),
                ick,
                SqlSyntax.GetQuotedTableName("merchItemCache"),
                SqlSyntax.GetQuotedColumnName("pk"));

            var innerSql = Sql()
                    .Select($"{icTbl}.{ick}, COUNT(*) AS itemCount")
                    .From<ItemCacheItemDto>()
                    .GroupBy<ItemCacheItemDto>(x => x.ContainerKey);

            var sql = Sql().SelectAll()
                    .FromQuery(innerSql, "itemCacheItemQ")
                    .InnerJoin<ItemCacheDto>()
                    .On(joinOn)
                    .InnerJoin<CustomerDto>()
                    .On<ItemCacheDto, CustomerDto>(left => left.EntityKey, right => right.Key)
                    .Where<ItemCacheDto>(x => x.ItemCacheTfKey == itemCacheTfKey)
                    .WhereBetween<CustomerDto>(x => x.LastActivityDate, startDate, endDate)
                    .Where($"{iciQ}.itemCount > 0")
                    .AppendOrderExpression<ItemCacheDto>(orderExpression, direction);

            return Database.Page<ItemCacheDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public int Count(Guid itemCacheTfKey, CustomerType customerType, DateTime startDate, DateTime endDate)
        {
            var countQuery = Sql().SelectField<ItemCacheItemDto>(x => x.ContainerKey)
                                .From<ItemCacheItemDto>()
                                .GroupBy<ItemCacheItemDto>(x => x.ContainerKey)
                                .Append("HAVING COUNT(*) > 0");

            Sql<SqlContext> sql;
            if (customerType == CustomerType.Anonymous)
            {
                sql = Sql().SelectCount()
                    .From<AnonymousCustomerDto>()
                    .InnerJoin<ItemCacheDto>()
                    .On<AnonymousCustomerDto, ItemCacheDto>(left => left.Key, right => right.EntityKey)
                    .WhereBetween<AnonymousCustomerDto>(x => x.LastActivityDate, startDate, endDate)
                    .Where<ItemCacheDto>(x => x.ItemCacheTfKey == itemCacheTfKey)
                    .AndIn<ItemCacheDto>(x => x.Key, countQuery);
            }
            else
            {
                sql = Sql().SelectCount()
                    .From<CustomerDto>()
                    .InnerJoin<ItemCacheDto>()
                    .On<CustomerDto, ItemCacheDto>(left => left.Key, right => right.EntityKey)
                    .WhereBetween<CustomerDto>(x => x.LastActivityDate, startDate, endDate)
                    .Where<ItemCacheDto>(x => x.ItemCacheTfKey == Constants.TypeFieldKeys.ItemCache.BasketKey)
                    .AndIn<ItemCacheDto>(x => x.Key, countQuery);
            }

            return Database.ExecuteScalar<int>(sql);
        }

        /// <inheritdoc/>
        protected override ItemCacheFactory GetFactoryInstance()
        {
            return new ItemCacheFactory();
        }
    }
}