namespace Merchello.Core.Compositions
{
    using System.Linq;

    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.Configuration;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Models;

    /// <summary>
    /// Sets the IoC container with configured strategies
    /// </summary>
    internal sealed class StrategiesComposition : ICompositionRoot
    {
        /// <summary>
        /// Composes strategy services by adding strategy classes to the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void Compose(IServiceRegistry container)
        {
            var strategies = MerchelloConfig.For.MerchelloExtensibility().Strategies;

            // shipment rate quote
            var shipRateQuote = strategies.FirstOrDefault(x => x.Key == Constants.StrategyTypeAlias.DefaultShipmentRateQuote);
            if (shipRateQuote.Value != null)
            {
                container.Register<IShipment, IShippingGatewayMethod[], IShipmentRateQuoteStrategy>(
                    (factory, shipment, methods) =>
                        {
                            var activator = factory.GetInstance<IActivatorServiceProvider>();

                            var strategy = activator.GetService<ShipmentRateQuoteStrategyBase>(
                                shipRateQuote.Value,
                                new object[] { factory.GetInstance<IRuntimeCacheProviderAdapter>(), shipment, methods });
                            return strategy;
                        });
            }

            // tax rate quote
            var taxRateQuote = strategies.FirstOrDefault(x => x.Key == Constants.StrategyTypeAlias.DefaultInvoiceTaxRateQuote);
            if (taxRateQuote.Value != null)
            {
                container.Register<IInvoice, IAddress, ITaxMethod, ITaxCalculationStrategy>(
                    (factory, invoice, address, method) =>
                        {
                            var activator = factory.GetInstance<IActivatorServiceProvider>();

                            var strategy = activator.GetService<TaxCalculationStrategyBase>(
                                taxRateQuote.Value,
                                new object[] { invoice, address, method });

                            return strategy;
                        });
            } 
        }
    }
}