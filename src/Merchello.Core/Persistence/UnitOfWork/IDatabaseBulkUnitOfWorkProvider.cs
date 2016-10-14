namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <summary>
    /// Represents a provider that can create units of work to work on databases and can perform bulk operations.
    /// </summary>
    public interface IDatabaseBulkUnitOfWorkProvider : IDatabaseUnitOfWorkProvider<IDatabaseBulkUnitOfWork>
    {
    }
}