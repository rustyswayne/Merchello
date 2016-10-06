﻿namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a repository that performs bulk operations.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity for bulk operations
    /// </typeparam>
    public interface IBulkOperationRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Bulk save new entities.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void PersistNewItems(IEnumerable<TEntity> entities);

        /// <summary>
        /// Bulk save updated entities.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void PersistUpdatedItems(IEnumerable<TEntity> entities);
    }
}