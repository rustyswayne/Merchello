namespace Merchello.Core.Compositions
{
    using System;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Chains;
    using Merchello.Core.Chains.InvoiceCreation;
    using Merchello.Core.Checkout;
    using Merchello.Core.Configuration;
    using Merchello.Core.Models;

    /// <summary>
    /// Sets the IoC container for the Merchello configuration task chains.
    /// </summary>
    internal sealed class TaskChainComposition : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            var taskChains = MerchelloConfig.For.MerchelloExtensibility().TaskChains;

            var invoiceBuilder = taskChains[Constants.TaskChainAlias.CheckoutManagerInvoiceCreate].ToArray();
            if (invoiceBuilder.Length > 0)
            {
                // Add the attempt chain task register.
                container.Register<ICheckoutManagerBase, IAttemptChainTaskRegister<IInvoice>>(
                    (factory, manager) => new InvoiceBuilderTaskRegister(
                        factory.GetInstance<IServiceContainer>(),
                        manager,
                        Constants.TaskChainAlias.CheckoutManagerInvoiceCreate),
                    Constants.TaskChainAlias.CheckoutManagerInvoiceCreate);

                // Register the instantiator for each of the tasks
                container.Register<Type, ICheckoutManagerBase, IAttemptChainTask<IInvoice>>(
                    (factory, type, manager) =>
                        {
                            var activator = factory.GetInstance<IActivatorServiceProvider>();

                            var service = activator.GetService<IAttemptChainTask<IInvoice>>(type, new object[] { manager });

                            return service;
                        });
            }
        }
    }
}