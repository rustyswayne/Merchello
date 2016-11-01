namespace Merchello.Core.Checkout
{
    using System;

    using LightInject;

    using Merchello.Core.Builders;
    using Merchello.Core.Chains;
    using Merchello.Core.Models;

    /// <summary>
    /// The checkout manager base.
    /// </summary>
    public abstract class CheckoutManagerBase : CheckoutContextManagerBase, ICheckoutManagerBase
    {
        /// <summary>
        /// The <see cref="IServiceContainer"/>.
        /// </summary>
        private readonly IServiceContainer _container;

        /// <summary>
        /// The invoice builder.
        /// </summary>
        private Lazy<IBuilderChain<IInvoice>> _invoiceBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutManagerBase"/> class.
        /// </summary>
        /// <param name="container">
        /// The <see cref="IServiceContainer"/>.
        /// </param>
        /// <param name="checkoutContext">
        /// The checkout Context.
        /// </param>
        protected CheckoutManagerBase(IServiceContainer container, ICheckoutContext checkoutContext)
            : base(checkoutContext)
        {
            Core.Ensure.ParameterNotNull(container, nameof(container));
            _container = container;
            this.Initialize();
        }

        /// <summary>
        /// Gets the checkout manager for customer information.
        /// </summary>
        public abstract ICheckoutCustomerManager Customer { get; }

        /// <summary>
        /// Gets the checkout extended manager for custom invoicing.
        /// </summary>
        public abstract ICheckoutExtendedManager Extended { get; }

        /// <summary>
        /// Gets the checkout manager for marketing offers.
        /// </summary>
        public abstract ICheckoutOfferManager Offer { get; }

        /// <summary>
        /// Gets the checkout manager for shipping.
        /// </summary>
        public abstract ICheckoutShippingManager Shipping { get; }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        public abstract ICheckoutPaymentManager Payment { get; }

        /// <summary>
        /// Gets the invoice builder.
        /// </summary>
        /// <returns>
        /// The <see cref="BuildChainBase{IInvoice}"/>.
        /// </returns>
        protected virtual IBuilderChain<IInvoice> InvoiceBuilder => this._invoiceBuilder.Value;

        /// <summary>
        /// Resets the checkout manager by removing persisted information.
        /// </summary>
        public override void Reset()
        {
            Customer.Reset();
            Offer.Reset();
            Extended.Reset();
            Payment.Reset();
            Shipping.Reset();
        }


        /// <summary>
        /// Initializes the manager.
        /// </summary>
        private void Initialize()
        {
            this._invoiceBuilder = new Lazy<IBuilderChain<IInvoice>>(() =>
                _container
                    .GetInstance<IAttemptChainTaskRegister<IInvoice>, ICheckoutManagerBase, IBuilderChain<IInvoice>>(
                     _container.GetInstance<ICheckoutManagerBase, IAttemptChainTaskRegister<IInvoice>>(this),
                        this));
        }
    }
}