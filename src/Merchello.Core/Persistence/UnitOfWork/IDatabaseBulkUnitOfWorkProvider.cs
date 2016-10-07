namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <summary>
    /// Represents a provider that can create units of work to work on databases and can perform bulk operations.
    /// </summary>
    public interface IDatabaseBulkUnitOfWorkProvider
    {
        /// <summary>
        /// Creates a unit of work.
        /// </summary>
        /// <returns>A new unit of work.</returns>
        IDatabaseBulkUnitOfWork CreateUnitOfWork();
    }
}