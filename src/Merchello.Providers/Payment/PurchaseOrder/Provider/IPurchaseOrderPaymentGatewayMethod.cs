namespace Merchello.Providers.Payment.PurchaseOrder.Provider
{
    using Merchello.Core.Gateways.Payment;

    /// <summary>
    /// Marker interface for an PurchaseOrder payment method
    /// </summary>
    public interface IPurchaseOrderPaymentGatewayMethod : IPaymentGatewayMethod
    {
    }
}