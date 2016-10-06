namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ShipCountryRepository : NPocoEntityRepositoryBase<IShipCountry, ShipCountryDto, ShipCountryFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ShipCountryDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchShipCountry.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchShipRateTier WHERE shipMethodKey IN (SELECT pk FROM merchShipMethod WHERE shipCountryKey = @Key)",
                "DELETE FROM merchShipMethod WHERE shipCountryKey = @Key",
                "DELETE FROM merchShipCountry WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        /// <param name="entity"></param>
        protected override void PersistDeletedItem(IShipCountry entity)
        {
            _shipmentRepository.UpdateForShipMethodDelete(entity.Key);
            base.PersistDeletedItem(entity);
        }
    }
}
