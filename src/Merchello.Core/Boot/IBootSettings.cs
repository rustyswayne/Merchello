namespace Merchello.Core.Boot
{
    /// <summary>
    /// Represents settings for booting Merchello.
    /// </summary>
    public interface IBootSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this boot is for testing.
        /// </summary>
        bool IsTest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether auto install.
        /// </summary>
        bool AutoInstall { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Merchello configuration files are required for this instance.
        /// </summary>
        bool RequiresConfig { get; set; }
    }
}