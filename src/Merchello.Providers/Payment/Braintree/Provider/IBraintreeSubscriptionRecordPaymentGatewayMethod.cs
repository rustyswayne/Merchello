namespace Merchello.Providers.Payment.Braintree.Provider
{
    using Merchello.Core.Gateways.Payment;

    /// <summary>
    /// Marker interface for the Braintree Web hook Record Payment Gateway Method.
    /// </summary>
    public interface IBraintreeSubscriptionRecordPaymentGatewayMethod : IPaymentGatewayMethod
    { 
    }
}