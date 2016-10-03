namespace Merchello.Core.Services
{
    /// <summary>
    /// Represents Merchello service context.
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        /// Gets the <see cref="IMigrationStatusService"/>.
        /// </summary>
        IMigrationStatusService MigrationStatusService { get; }
    }
}
