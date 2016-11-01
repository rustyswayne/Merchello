namespace Merchello.Core.Marketing.Offer
{
    using System;

    /// <summary>
    /// The DiscountOfferProvider interface.
    /// </summary>
    public interface IOfferProvider
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        Guid Key { get; }

        /// <summary>
        /// Gets the name of the type this provider manages.
        /// </summary>
        string ManagesTypeName { get; }
    }
}