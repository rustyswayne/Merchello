namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ShipmentRepository : NPocoEntityRepositoryBase<IShipment, ShipmentDto, ShipmentFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
               .From<ShipmentDto>()
               .InnerJoin<ShipmentStatusDto>()
               .On<ShipmentDto, ShipmentStatusDto>(left => left.ShipmentStatusKey, right => right.Key);
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchShipment.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchShipment WHERE pk = @Key",
                };

            return list;
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(IShipment entity)
        {
            _orderLineItemRepository.UpdateForShipmentDelete(entity.Key);
            base.PersistDeletedItem(entity);
        }
    }
}
