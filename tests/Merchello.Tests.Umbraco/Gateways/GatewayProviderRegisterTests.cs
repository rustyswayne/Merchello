namespace Merchello.Tests.Umbraco.Gateways
{
    using System;
    using System.Linq;
    using System.Threading;

    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Gateways;
    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Providers.Notification.Smtp;
    using Merchello.Providers.Payment.Cash;
    using Merchello.Providers.Shipping.FixedRate;
    using Merchello.Providers.Taxation.FixedRate;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class GatewayProviderRegisterTests : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;
        

        protected IGatewayProviderRegister Register;

        public override void Initialize()
        {
            base.Initialize();

            this.Register = MC.Container.GetInstance<IGatewayProviderRegister>();
            Assert.NotNull(this.Register);

        }

        [Test]
        public void GetAllGatewayProviders()
        {
            //// Arrange
            // expect to resolve cash payment, smtp notification, fixed tax, flat rate ship
            var expected = 4;

            //// Act
            var providers = this.Register.GetAllProviders().ToArray();

            foreach (var p in providers) Console.WriteLine(p.GetType().Name);

            //// Assert
            Assert.AreEqual(expected, providers.Count());
        }


        [Test]
        public void GetActivatedProviders()
        {
            //// Arrange
            // expect to resolve cash payment, fixed tax, flat rate ship
            var expected = 3;

            //// Act
            var providers = this.Register.GetActivatedProviders();

            //// Assert
            Assert.AreEqual(expected, providers.Count());
        }

        [Test]
        public void GetActivatedProvidersOfT( )
        {
            var cash = Register.GetActivatedProviders<ICashPaymentGatewayProvider>().First();
            Assert.That(cash != null);
            Assert.That(cash.GetType(), Is.EqualTo(typeof(CashPaymentGatewayProvider)));
            var typedCash = Register.GetActivatedProviders<CashPaymentGatewayProvider>().First();
            Assert.That(cash != null);
        }

        [Test]
        public void GetAllProvidersOfT()
        {
            var smtp = Register.GetAllProviders<ISmtpNotificationGatewayProvider>().First();

            Assert.NotNull(smtp);
            Assert.That(smtp.Activated, Is.False);
        }

        [TestCase(typeof(IGatewayProvider), 3)]
        [TestCase(typeof(IPaymentGatewayProvider), 1)]
        [TestCase(typeof(ICashPaymentGatewayProvider), 1)]
        [TestCase(typeof(CashPaymentGatewayProvider), 1)]
        [TestCase(typeof(IShippingGatewayProvider), 1)]
        [TestCase(typeof(IFixedRateShippingGatewayProvider), 1)]
        [TestCase(typeof(FixedRateShippingGatewayProvider), 1)]
        [TestCase(typeof(ITaxationGatewayProvider), 1)]
        [TestCase(typeof(IFixedRateTaxationGatewayProvider), 1)]
        [TestCase(typeof(FixedRateTaxationGatewayProvider), 1)]
        [Test]
        public void GetActivatedProviders(Type type, int expected)
        {
            //// Act
            var providers = Register.GetActivatedProviders(type).ToArray();
            foreach (var p in providers)
            {
                Console.WriteLine(p.GetType().Name);
            }

            //// Assert
            Assert.AreEqual(expected, providers.Length);
        }

        [TestCase(typeof(IPaymentGatewayProvider), GatewayProviderType.Payment)]
        [TestCase(typeof(INotificationGatewayProvider), GatewayProviderType.Notification)]
        [TestCase(typeof(ITaxationGatewayProvider), GatewayProviderType.Taxation)]
        [TestCase(typeof(IShippingGatewayProvider), GatewayProviderType.Shipping)]
        [TestCase(typeof(CashPaymentGatewayProvider), GatewayProviderType.Payment)]
        [TestCase(typeof(ICashPaymentGatewayProvider), GatewayProviderType.Payment)]
        [TestCase(typeof(FixedRateShippingGatewayProvider), GatewayProviderType.Shipping)]
        [TestCase(typeof(IFixedRateShippingGatewayProvider), GatewayProviderType.Shipping)]
        [TestCase(typeof(FixedRateTaxationGatewayProvider), GatewayProviderType.Taxation)]
        [TestCase(typeof(IFixedRateTaxationGatewayProvider), GatewayProviderType.Taxation)]
        [Test]
        public void GetGatewayProviderType(Type t, GatewayProviderType expected)
        {
            //// Arrange
            // handled

            //// Act
            var type = GatewayProviderRegister.GetGatewayProviderType(t);

            //// Assert
            Assert.AreEqual(expected, type);
        }
    }
}