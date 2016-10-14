namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <summary>
    /// Represents a provider that can create units of work to work on databases.
    /// </summary>
    public interface IDatabaseUnitOfWorkProvider : IDatabaseUnitOfWorkProvider<IDatabaseUnitOfWork>
	{
	}
}