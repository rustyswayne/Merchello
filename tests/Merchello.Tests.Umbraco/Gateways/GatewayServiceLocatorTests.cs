namespace Merchello.Tests.Umbraco.Gateways
{
    using Merchello.Core.DI;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class GatewayServiceLocatorTests : UmbracoRuntimeTestBase
    {
        [Test]
        public void GatewayContext()
        {
            Assert.NotNull(MC.Container.GetInstance<IGatewayContext>());
        }

        [Test]
        public void NotificationContext()
        {
            Assert.NotNull(MC.Container.GetInstance<INotificationContext>());
        }

        [Test]
        public void PaymentContext()
        {
            Assert.NotNull(MC.Container.GetInstance<IPaymentContext>());
        }

        [Test]
        public void ShippingContext()
        {
            Assert.NotNull(MC.Container.GetInstance<IShippingContext>());
        }
    }
}