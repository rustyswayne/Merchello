namespace Merchello.Providers.Shipping.FixedRate
{
    using Merchello.Core.Gateways.Shipping;

    /// <summary>
    /// Defines the rate table ship method
    /// </summary>
    public interface IFixedRateShippingGatewayMethod : IShippingGatewayMethod
    {
        /// <summary>
        /// Gets the <see cref="IShippingFixedRateTable"/> for this ship method
        /// </summary>
        IShippingFixedRateTable RateTable { get; }

        /// <summary>
        /// Gets the <see cref="FixedRateShippingGatewayMethod.QuoteType"/> for this ship method
        /// </summary>
        FixedRateShippingGatewayMethod.QuoteType RateTableType { get; }
    }
}