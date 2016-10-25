namespace Merchello.Core.Persistence.Migrations
{
    /// <summary>
    /// Represents an adapted Migration Expression.
    /// </summary>
    internal interface IMigrationExpressionAdapter
    {
        string Process();
    }
}