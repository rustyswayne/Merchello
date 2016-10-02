namespace Merchello.Core.Persistence
{
    using NPoco;

    /// <summary>
    /// Represents the a database adapter.
    /// </summary>
    public interface IDatabaseAdapter : IExposeSqlSyntax
    {
        /// <summary>
        /// Gets the NPoco database.
        /// </summary>
        Database Database { get; }
    }
}