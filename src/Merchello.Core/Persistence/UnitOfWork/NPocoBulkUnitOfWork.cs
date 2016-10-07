namespace Merchello.Core.Persistence.UnitOfWork
{
    using System;
    using System.Linq;

    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    internal class NPocoBulkUnitOfWork : NPocoUnitOfWork, IDatabaseBulkUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPocoBulkUnitOfWork"/> class. 
        /// </summary>
        /// <param name="dbAdapter">
        /// A database.
        /// </param>
        /// <param name="factory">
        /// A repository factory.
        /// </param>
        /// <remarks>
        /// This should be used by the NPocoUnitOfWorkProvider exclusively.
        /// </remarks>
        public NPocoBulkUnitOfWork(IDatabaseAdapter dbAdapter, RepositoryFactory factory)
            : base(dbAdapter, factory)
        {
        }

        /// <summary>
        /// Performs all operations in the queue.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of entity
        /// </typeparam>
        public void FlushBulk<TEntity>() where TEntity : IEntity
        {
            if (Operations.Any(o => !(o.Repository is IBulkOperationRepository<TEntity>)))
            {
                Complete();
                return;
            }

            this.Begin();

            var operations = Operations.ToList();
            foreach (var opGroup in operations.GroupBy(o => new { o.Repository, o.Type }))
            {
                switch (opGroup.Key.Type)
                {
                    case OperationType.Insert:
                        ((IBulkOperationRepository<TEntity>)opGroup.Key.Repository).PersistNewItems(opGroup.Select(o => (TEntity)o.Entity));
                        break;
                    case OperationType.Update:
                        ((IBulkOperationRepository<TEntity>)opGroup.Key.Repository).PersistUpdatedItems(opGroup.Select(o => (TEntity)o.Entity));
                        break;
                    case OperationType.Delete:
                    default:
                        throw new InvalidOperationException("Only Inserts and Updates are supported");
                }
            }

        }

        /// <inheritdoc/>
        public void CompleteBulk<TEntity>() where TEntity : IEntity
        {
            this.FlushBulk<TEntity>();
            this.Completed = true;
        }
    }
}