namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class EntityCollectionRepository : NPocoEntityRepositoryBase<IEntityCollection, EntityCollectionDto, EntityCollectionFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<EntityCollectionDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchEntityCollection.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchProduct2EntityCollection WHERE merchProduct2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchInvoice2EntityCollection WHERE merchInvoice2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchCustomer2EntityCollection WHERE merchCustomer2EntityCollection.entityCollectionKey = @Key",
                    "DELETE FROM merchEntityCollection WHERE merchEntityCollection.pk = @Key"
                };

            return list;
        }
    }
}
