namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ProductOptionService : IProductOptionService
    {
        /// <inheritdoc/>
        public PagedCollection<IProductOption> GetPagedCollection(long page, long itemsPerPage, string sortBy = "", Direction sortDirection = Direction.Descending, bool sharedOnly = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IProductOption> GetPagedCollection(string term, long page, long itemsPerPage, string sortBy = "", Direction sortDirection = Direction.Descending, bool sharedOnly = true)
        {
            throw new NotImplementedException();
        }
    }
}
