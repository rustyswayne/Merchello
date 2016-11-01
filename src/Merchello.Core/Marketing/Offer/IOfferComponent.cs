namespace Merchello.Core.Marketing.Offer
{
    using System;

    /// <summary>
    /// Represents a <see cref="IOfferComponent"/>.
    /// </summary>
    public interface IOfferComponent
    {
        /// <summary>
        /// Gets the component type.
        /// </summary>
        OfferComponentType ComponentType { get; }

        /// <summary>
        /// Gets the display configuration format.
        /// </summary>
        string DisplayConfigurationFormat { get; }

        /// <summary>
        /// Gets the offer code.
        /// </summary>
        string OfferCode { get; }

        /// <summary>
        /// Gets the offer settings key.
        /// </summary>
        Guid OfferSettingsKey { get; }

        /// <summary>
        /// Gets a value indicating whether the compoent requires configuration before the offer can be activated.
        /// </summary>
        bool RequiresConfiguration { get; }

        /// <summary>
        /// The get configuration value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetConfigurationValue(string key);
    }
}