namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class ProductOptionService : IProductOptionService
    {
        /// <inheritdoc/>
        public PagedCollection<IProductOption> GetPagedCollection(long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending, bool sharedOnly = true)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var results = repo.SearchForTerm(string.Empty, page, itemsPerPage, sortBy, sharedOnly, direction);
                uow.Complete();
                return results;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<IProductOption> Search(string term, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending, bool sharedOnly = true)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.ProductTree);
                var repo = uow.CreateRepository<IProductOptionRepository>();
                var results = repo.SearchForTerm(term, page, itemsPerPage, sortBy, sharedOnly, direction);
                uow.Complete();
                return results;
            }
        }
    }
}
