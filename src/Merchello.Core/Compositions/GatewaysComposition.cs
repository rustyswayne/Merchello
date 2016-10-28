namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;

    /// <summary>
    /// Sets the IoC container for the Merchello <see cref="IGatewayProvider"/>.
    /// </summary>
    public class GatewaysComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {

            container.RegisterSingleton<INotificationContext, NotificationContext>();
            container.RegisterSingleton<IPaymentContext, PaymentContext>();
            container.RegisterSingleton<IShippingContext, ShippingContext>();
            container.RegisterSingleton<ITaxationContext, TaxationContext>();

            container.RegisterSingleton<IGatewayContext, GatewayContext>();
        }
    }
}