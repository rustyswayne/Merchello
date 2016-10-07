namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : ISearchTermRepository<IInvoice>
    {
        /// <inheritdoc/>
        public PagedCollection<IInvoice> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
