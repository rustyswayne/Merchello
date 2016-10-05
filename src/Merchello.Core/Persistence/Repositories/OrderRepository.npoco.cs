namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Cache;
    using Merchello.Core.Cache.Policies;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class OrderRepository : NPocoRespositoryBase<IOrder>
    {
        /// <summary>
        /// The caching policy.
        /// </summary>
        private IRepositoryCachePolicy<IOrder> _cachePolicy;

        /// <inheritdoc/>
        protected override IRepositoryCachePolicy<IOrder> CachePolicy
        {
            get
            {
                var options = new RepositoryCachePolicyOptions(() =>
                {
                    // Get count of all entities of current type (TEntity) to ensure cached result is correct
                    var query = Query.Where(x => x.Key != null && x.Key != Guid.Empty);
                    return PerformCount(query);
                });

                _cachePolicy = new OrderRepositoryCachePolicy(RuntimeCache, options);

                return _cachePolicy;
            }
        }

        /// <inheritdoc/>
        protected override IOrder PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false);
            sql.Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.First<OrderDto>(sql);

            if (dto == null)
                return null;

            var factory = new OrderFactory(new OrderStatusFactory(), _orderLineItemRepository.GetLineItemCollection);
            var entity = factory.BuildEntity(dto);

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Cached via CachingPolicy 
        /// </remarks>
        protected override IEnumerable<IOrder> PerformGetAll(params Guid[] keys)
        {
            if (keys.Any())
            {
                return Database.Fetch<OrderDto>("WHERE pk in (@keys)", new { /*keys =*/ keys })
                    .Select(x => Get(x.Key));
            }

            return Database.Fetch<OrderDto>().Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        /// REVIEW - This is NOT a cached query so we need to take
        /// precautions to assert it is not used in a service that is called over and over.
        protected override IEnumerable<IOrder> PerformGetByQuery(IQuery<IOrder> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<IOrder>(sqlClause, query);
            var sql = translator.Translate();

            var dtos = Database.Fetch<OrderDto>(sql);

            if (!dtos.Any()) return Enumerable.Empty<IOrder>();

            var factory = new OrderFactory(new OrderStatusFactory(), _orderLineItemRepository.GetLineItemCollection);
            return dtos.Select(dto => factory.BuildEntity(dto));
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IOrder entity)
        {
            ((Entity)entity).AddingEntity();

            var factory = new OrderFactory(new OrderStatusFactory(), _orderLineItemRepository.GetLineItemCollection);
            var dto = factory.BuildDto(entity);

            Database.Insert(dto);
            entity.Key = dto.Key;

            _orderLineItemRepository.SaveLineItem(entity.Items, entity.Key);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IOrder entity)
        {
            ((Entity)entity).UpdatingEntity();

            var factory = new OrderFactory(new OrderStatusFactory(), _orderLineItemRepository.GetLineItemCollection);
            var dto = factory.BuildDto(entity);

            Database.Update(dto);

            _orderLineItemRepository.SaveLineItem(entity.Items, entity.Key);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
               .From<OrderDto>()
               .InnerJoin<OrderStatusDto>()
               .On<OrderDto, OrderStatusDto>(left => left.OrderStatusKey, right => right.Key);
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchOrder.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchOrderItem WHERE orderKey = @Key",
                "DELETE FROM merchOrder WHERE pk = @Key"
            };

            return list;
        }
    }
}
