namespace Merchello.Core.Persistence.UnitOfWork
{
    using Merchello.Core.Models.EntityBase;

    /// <summary>
	/// Represents a persistence unit of work for working with a database that can also perform bulk operations.
	/// </summary>
    public interface IDatabaseBulkUnitOfWork : IDatabaseUnitOfWork
    {
        /// <summary>
        /// Completes the bulk unit of work.
        /// </summary>
        /// <typeparam name="TEntity">
        /// The type of entity
        /// </typeparam>
        /// <remarks>
        /// When a unit of work is completed, a local transaction scope is created at database level,
        /// all queued operations are executed, and the scope is committed. If the unit of work is not completed
        /// before it is disposed, all queued operations are cleared and the scope is rolled back (and also
        /// higher level transactions if any).
        /// Whether this actually commits or rolls back the transaction depends on whether the transaction scope
        /// is part of a higher level transactions. The  database transaction is committed or rolled back only
        /// when the upper level scope is disposed.
        /// If any operation is added to the unit of work after it has been completed, then its completion
        /// status is reset. So in a way it could be possible to always complete and never flush, but flush
        /// is preferred when appropriate to indicate that you understand what you are doing.
        /// Every units of work should be completed, unless a rollback is required. That is, even if the unit of
        /// work contains only read operations, that do not need to be "committed", the unit of work should be
        /// properly completed, else it may force an unexpected rollback of a higher-level transaction.
        /// </remarks>
        void CompleteBulk<TEntity>() where TEntity : IEntity;
    }
}