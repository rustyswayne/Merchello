namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class EntityCollectionService : IEntityCollectionService
    {
        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByProductKey(Guid productKey, bool isFilter = false)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityCollectionsByProductKey(productKey, isFilter);
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByInvoiceKey(Guid invoiceKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityCollectionsByInvoiceKey(invoiceKey);
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityCollection> GetByCustomerKey(Guid customerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityCollectionsByCustomerKey(customerKey);
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetFilterGroupsByProviderKeys(IEnumerable<Guid> keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityFilterGroupsByProviderKeys(keys.ToArray());
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetFilterGroupsContainingProduct(IEnumerable<Guid> keys, Guid productKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityFilterGroupsContainingProduct(keys.ToArray(), productKey);
                uow.Complete();
                return collections;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IEntityFilterGroup> GetFilterGroupsNotContainingProduct(IEnumerable<Guid> keys, Guid productKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IEntityCollectionRepository>();
                var collections = repo.GetEntityFilterGroupsNotContainingProduct(keys.ToArray(), productKey);
                uow.Complete();
                return collections;
            }
        }
    }
}
