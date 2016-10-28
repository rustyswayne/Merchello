namespace Merchello.Core.Persistence.Migrations
{
    using Semver;

    /// <summary>
    /// Represents a migration instruction.
    /// </summary>
    internal class MigrationInstruction
    {
        /// <summary>
        /// Gets or sets the current configuration status.
        /// </summary>
        public SemVersion ConfigurationStatus { get; set; }

        /// <summary>
        /// Gets or sets the target configuration status.
        /// </summary>
        public SemVersion TargetConfigurationStatus { get; set; }

        /// <summary>
        /// Gets a value indicating whether to update the merchello settings configuration file.
        /// </summary>
        public bool UpdateConfigFile => ConfigurationStatus != TargetConfigurationStatus;

        /// <summary>
        /// Gets or sets a value indicating whether auto update the database schema.
        /// </summary>
        public bool AutoUpdateDbSchema { get; set; }

        /// <summary>
        /// Gets or sets the plugin install status.
        /// </summary>
        public PluginInstallStatus PluginInstallStatus { get; set; }
    }
}