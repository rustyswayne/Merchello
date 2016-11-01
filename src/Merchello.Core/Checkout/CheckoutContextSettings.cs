namespace Merchello.Core.Checkout
{
    /// <summary>
    /// The checkout context version change settings.
    /// </summary>
    public class CheckoutContextSettings : ICheckoutContextSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutContextSettings"/> class.
        /// </summary>
        public CheckoutContextSettings()
        {
            this.ResetCustomerManagerDataOnVersionChange = true;
            this.ResetPaymentManagerDataOnVersionChange = true;
            this.ResetExtendedManagerDataOnVersionChange = true;
            this.ResetShippingManagerDataOnVersionChange = true;
            this.ResetOfferManagerDataOnVersionChange = true;
            this.EmptyBasketOnPaymentSuccess = true;
            this.ApplyTaxesToInvoice = true;
        }

        /// <summary>
        /// Gets or sets the invoice number prefix to be added to the generated invoice in the invoice builder.
        /// </summary>
        public string InvoiceNumberPrefix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to apply taxes to generated invoice.
        /// </summary>
        public bool ApplyTaxesToInvoice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reset the customer manager data on version change.
        /// </summary>
        public bool ResetCustomerManagerDataOnVersionChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reset the payment manager data on version change.
        /// </summary>
        public bool ResetPaymentManagerDataOnVersionChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reset the extended manager data on version change.
        /// </summary>
        public bool ResetExtendedManagerDataOnVersionChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reset the shipping manager data on version change.
        /// </summary>
        public bool ResetShippingManagerDataOnVersionChange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether reset the offer manager data on version change.
        /// </summary>
        public bool ResetOfferManagerDataOnVersionChange { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to empty the basket on payment success.
        /// </summary>
        public bool EmptyBasketOnPaymentSuccess { get; set; }
    }
}