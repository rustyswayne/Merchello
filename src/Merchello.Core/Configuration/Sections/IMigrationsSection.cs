namespace Merchello.Core.Configuration.Sections
{
    /// <summary>
    /// Represents a configuration section for configurations related to Merchello "migrations" (upgrade process). 
    /// </summary>
    public interface IMigrationsSection : IMerchelloSection
    {

        /// <summary>
        /// Gets a value indicating whether or not to automatically run database schema changes when an install or upgrade migration
        /// has occurred.
        /// </summary>
        bool AutoUpdateDbSchema { get; }
    }
}