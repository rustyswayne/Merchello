namespace Merchello.Core.Chains.InvoiceCreation
{
    using System;

    using LightInject;

    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    /// <summary>
    /// The invoice builder task register.
    /// </summary>
    internal class InvoiceBuilderTaskRegister : ContainerConfigurationChainTaskRegister<IInvoice>
    {
        /// <summary>
        /// The <see cref="ICheckoutManagerBase"/>.
        /// </summary>
        private readonly ICheckoutManagerBase _checkoutManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceBuilderTaskRegister"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="manager">
        /// The <see cref="ICheckoutManagerBase"/>.
        /// </param>
        /// <param name="chainAlias">
        /// The chain alias.
        /// </param>
        public InvoiceBuilderTaskRegister(IServiceContainer container, ICheckoutManagerBase manager, string chainAlias)
            : base(container, chainAlias)
        {
            Core.Ensure.ParameterNotNull(manager, nameof(manager));
            _checkoutManager = manager;
        }


        /// <inheritdoc/>
        protected override IAttemptChainTask<IInvoice> CreateInstance(Type type)
        {
            return Container.GetInstance<Type, ICheckoutManagerBase, IAttemptChainTask<IInvoice>>(type, _checkoutManager);
        }
    }
}