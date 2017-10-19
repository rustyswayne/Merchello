namespace Merchello.Core.Boot
{
    /// <inheritdoc/>
    public class BootSettings : IBootSettings
    {
        /// <inheritdoc/>
        public bool IsTest { get; set; } = false;

        /// <inheritdoc/>
        public bool AutoInstall { get; set; } = true;

        /// <inheritdoc/>
        public bool RequiresConfig { get; set; } = true;
    }
}