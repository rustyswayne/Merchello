namespace Merchello.Core.Persistence.UnitOfWork
{
    /// <summary>
    /// Common marker interface for database unit of work providers.
    /// </summary>
    /// <typeparam name="TUow">
    /// The type of database Unit of work
    /// </typeparam>
    public interface IDatabaseUnitOfWorkProvider<out TUow>
        where TUow : IDatabaseUnitOfWork
    {
        /// <summary>
        /// Creates a unit of work.
        /// </summary>
        /// <returns>A new unit of work.</returns>
        TUow CreateUnitOfWork();
    }
}
