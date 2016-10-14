namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class CustomerAddressRepository : NPocoEntityRepositoryBase<ICustomerAddress, CustomerAddressDto, CustomerAddressFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<CustomerAddressDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchCustomerAddress.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchCustomerAddress WHERE pk = @Key",
                };

            return list;
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(ICustomerAddress entity)
        {
            base.PersistNewItem(entity);

            // TODO ensure default
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(ICustomerAddress entity)
        {
            base.PersistUpdatedItem(entity);

            // TODO ensure default
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(ICustomerAddress entity)
        {
            base.PersistDeletedItem(entity);

            // TODO ensure default
        }
    }
}
