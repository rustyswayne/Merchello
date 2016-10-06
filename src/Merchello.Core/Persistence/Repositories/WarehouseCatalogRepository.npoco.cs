namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class WarehouseCatalogRepository : NPocoEntityRepositoryBase<IWarehouseCatalog, WarehouseCatalogDto, WarehouseCatalogFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<WarehouseCatalogDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchWarehouseCatalog.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchCatalogInventory WHERE catalogKey = @Key",
                    "DELETE FROM merchShipMethod WHERE shipCountryKey IN (SELECT pk FROM merchShipCountry WHERE catalogKey = @Key)",
                    "DELETE FROM merchShipCountry WHERE catalogKey = @Key",
                    "DELETE FROM merchWarehouseCatalog WHERE pk = @Key"
                };

            return list;
        }
    }
}
