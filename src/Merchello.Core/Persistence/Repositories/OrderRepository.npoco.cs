namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class OrderRepository : NPocoRespositoryBase<IOrder>
    {

        /// <inheritdoc/>
        protected override IOrder PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false);
            sql.Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.First<OrderDto>(sql);
           
            // TODO need the OrderStatusDto

            if (dto == null)
                return null;

            var factory = new OrderFactory();
            var entity = factory.BuildEntity(dto);

            // line items
            ((Order)entity).Items = _orderLineItemRepository.GetLineItemCollection(entity.Key);

            entity.ResetDirtyProperties();

            return entity;
        }

        /// <inheritdoc/>
        /// REVIEW
        protected override IEnumerable<IOrder> PerformGetAll(params Guid[] keys)
        {
            if (keys.Any())
            {
                return Database.Fetch<OrderDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => Get(x.Key));
            }

            return Database.Fetch<OrderDto>().Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        protected override IEnumerable<IOrder> PerformGetByQuery(IQuery<IOrder> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<IOrder>(sqlClause, query);
            var sql = translator.Translate();

            var dtos = Database.Fetch<OrderDto>(sql);
            return dtos.Select(dto => Get(dto.Key));
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IOrder entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IOrder entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            throw new NotImplementedException();
        }
    }
}
