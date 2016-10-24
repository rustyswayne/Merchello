namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class EntityCollectionService : IEntityCollectionService
    {
        /// <inheritdoc/>
        public IEntityFilterGroup GetEntityFilterGroupByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var filterGroup = repo.GetEntityFilterGroup(key);
                uow.Complete();
                return filterGroup;
            }
        }
    }
}
