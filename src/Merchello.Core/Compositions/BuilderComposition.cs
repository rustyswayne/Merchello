namespace Merchello.Core.Compositions
{
    using LightInject;

    using Merchello.Core.Builders;
    using Merchello.Core.Chains;
    using Merchello.Core.Checkout;
    using Merchello.Core.DI;
    using Merchello.Core.Models;

    /// <summary>
    /// Sets the IoC container for the Merchello Builders.
    /// </summary>
    internal sealed class BuilderComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            // Invoice builder
            container.Register<IAttemptChainTaskRegister<IInvoice>, ICheckoutManagerBase, IBuilderChain<IInvoice>>(
                (factory, register, manager) => new InvoiceBuilderChain(register, manager));
        }
    }
}