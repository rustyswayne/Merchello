namespace Merchello.Providers.Models
{
    using System;

    /// <summary>
    /// An attribute for mapping a provider to its serializable settings.
    /// </summary>
    internal class ProviderSettingsMapperAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderSettingsMapperAttribute"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="settingsType">
        /// The settings type.
        /// </param>
        internal ProviderSettingsMapperAttribute(string key, Type settingsType)
        {
            this.Key = key;
            this.SettingsType = settingsType;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the settings type.
        /// </summary>
        public Type SettingsType { get; private set; }
    }
}