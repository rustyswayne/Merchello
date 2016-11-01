namespace Merchello.Core.Chains.InvoiceCreation
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    /// <summary>
    /// Adds the invoice number prefix to the invoice.
    /// </summary>
    internal class AddInvoiceNumberPrefixTask : CheckoutManagerInvoiceCreationAttemptChainTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddInvoiceNumberPrefixTask"/> class.
        /// </summary>
        /// <param name="checkoutManager">
        /// The checkout manager.
        /// </param>
        public AddInvoiceNumberPrefixTask(ICheckoutManagerBase checkoutManager)
            : base(checkoutManager)
        {
        }

        /// <summary>
        /// Adds the invoice number prefix to the invoice if it has been set.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        public override Attempt<IInvoice> PerformTask(IInvoice value)
        {
            if (!string.IsNullOrWhiteSpace(this.CheckoutManager.Context.Settings.InvoiceNumberPrefix))
            {
                value.InvoiceNumberPrefix = this.CheckoutManager.Context.Settings.InvoiceNumberPrefix;
            }

            return Attempt<IInvoice>.Succeed(value);
        }
    }
}