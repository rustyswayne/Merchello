namespace Merchello.Providers.Payment.Braintree.Provider
{
    using Merchello.Core.Gateways.Payment;

    /// <summary>
    /// Marker interface for making one time PayPal transactions through Braintree.
    /// </summary>
    public interface IPayPalOneTimeTransactionPaymentGatewayMethod : IPaymentGatewayMethod
    { 
    }
}