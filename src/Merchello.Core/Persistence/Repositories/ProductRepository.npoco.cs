namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductRepository : NPocoRepositoryBase<IProduct>
    {
        /// <inheritdoc/>
        protected override IProduct PerformGet(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProduct> PerformGetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IProduct> PerformGetByQuery(IQuery<IProduct> query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IProduct entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IProduct entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            throw new NotImplementedException();
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
