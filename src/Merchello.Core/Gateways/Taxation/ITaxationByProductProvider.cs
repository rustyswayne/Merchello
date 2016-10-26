namespace Merchello.Core.Gateways.Taxation
{
    using System;

    /// <summary>
    /// Represents a provider for product base taxation calculations.
    /// </summary>
    public interface ITaxationByProductProvider : ITaxationGatewayProvider
    {
        /// <summary>
        /// Gets the tax method configured to use when Merchello is configured to include tax in product pricing.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ITaxationByProductMethod"/>.
        /// </returns>
        ITaxationByProductMethod GetTaxationByProductMethod(Guid key);
    }
}