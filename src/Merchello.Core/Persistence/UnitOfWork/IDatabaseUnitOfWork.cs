namespace Merchello.Core.Persistence.UnitOfWork
{
    using NPoco;

    /// <summary>
	/// Represents a persistence unit of work for working with a database.
	/// </summary>
	public interface IDatabaseUnitOfWork : IUnitOfWork
	{
        /// <summary>
        /// Gets the database.
        /// </summary>
        IDatabaseAdapter DatabaseAdapter { get; }

        /// <summary>
        /// Read lock to prevent dirty read.
        /// </summary>
        /// <param name="lockIds">
        /// The lock ids.
        /// </param>
        void ReadLock(params int[] lockIds);

        /// <summary>
        /// Database write lock.
        /// </summary>
        /// <param name="lockIds">
        /// The lock ids.
        /// </param>
        void WriteLock(params int[] lockIds);
    }
}