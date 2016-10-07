namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : NPocoRepositoryBase<IInvoice>
    {
        /// <inheritdoc/>
        protected override IInvoice PerformGet(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IInvoice> PerformGetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<IInvoice> PerformGetByQuery(IQuery<IInvoice> query)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IInvoice entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IInvoice entity)
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
