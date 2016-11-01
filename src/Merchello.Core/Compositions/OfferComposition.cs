namespace Merchello.Core.Compositions
{
    using System;

    using LightInject;

    using Merchello.Core.Marketing.Offer;

    /// <summary>
    /// Sets the IoC container for the Merchello offer components.
    /// </summary>
    internal sealed class OfferComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            // Register the component resolver
            container.Register<Type, OfferComponentDefinition, IOfferComponent>(
                (factory, type, definition) =>
                    {
                        var activator = factory.GetInstance<IActivatorServiceProvider>();

                        var component = activator.GetService<IOfferComponent>(type, new object[] { definition });

                        return component;
                    });
        }
    }
}