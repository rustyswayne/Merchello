namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Models.EntityBase;

    internal interface IBulkOperationRepository<in TEntity>
        where TEntity : IEntity
    {
        void PersistNewItems(IEnumerable<TEntity> entities);

        void PersistUpdatedItems(IEnumerable<TEntity> entities);
    }
}