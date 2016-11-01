namespace Merchello.Core.Configuration.Elements
{
    using System.Configuration;

    using Merchello.Core.Acquired.Configuration;
    using Merchello.Core.Configuration.Sections;

    /// <inheritdoc/>
    internal class CheckoutContextElement : ConfigurationElement, ICheckoutContextSection
    {
        /// <inheritdoc/>
        string ICheckoutContextSection.InvoiceNumberPrefix => this.InvoiceNumberPrefix;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ApplyTaxesToInvoice => this.ApplyTaxesToInvoice;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ResetCustomerManagerDataOnVersionChange => this.ResetCustomerManagerDataOnVersionChange;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ResetPaymentManagerDataOnVersionChange => this.ResetPaymentManagerDataOnVersionChange;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ResetExtendedManagerDataOnVersionChange => this.ResetExtendedManagerDataOnVersionChange;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ResetShippingManagerDataOnVersionChange => this.ResetShippingManagerDataOnVersionChange;

        /// <inheritdoc/>
        bool ICheckoutContextSection.ResetOfferManagerDataOnVersionChange => this.ResetOfferManagerDataOnVersionChange;

        /// <inheritdoc/>
        bool ICheckoutContextSection.EmptyBasketOnPaymentSuccess => this.EmptyBasketOnPaymentSuccess;

        /// <inheritdoc/>
        [ConfigurationProperty("invoiceNumberPrefix")]
        internal InnerTextConfigurationElement<string> InvoiceNumberPrefix => (InnerTextConfigurationElement<string>)this["invoiceNumberPrefix"];

        /// <inheritdoc/>
        [ConfigurationProperty("applyTaxesToInvoice")]
        internal InnerTextConfigurationElement<bool> ApplyTaxesToInvoice => (InnerTextConfigurationElement<bool>)this["applyTaxesToInvoice"];

        /// <inheritdoc/>
        [ConfigurationProperty("raiseCustomerEvents")]
        internal InnerTextConfigurationElement<bool> RaiseCustomerEvents => (InnerTextConfigurationElement<bool>)this["raiseCustomerEvents"];

        /// <inheritdoc/>
        [ConfigurationProperty("resetCustomerManagerDataOnVersionChange")]
        internal InnerTextConfigurationElement<bool> ResetCustomerManagerDataOnVersionChange => (InnerTextConfigurationElement<bool>)this["resetCustomerManagerDataOnVersionChange"];

        /// <inheritdoc/>
        [ConfigurationProperty("resetPaymentManagerDataOnVersionChange")]
        internal InnerTextConfigurationElement<bool> ResetPaymentManagerDataOnVersionChange => (InnerTextConfigurationElement<bool>)this["resetPaymentManagerDataOnVersionChange"];

        /// <inheritdoc/>
        [ConfigurationProperty("resetExtendedManagerDataOnVersionChange")]
        internal InnerTextConfigurationElement<bool> ResetExtendedManagerDataOnVersionChange => (InnerTextConfigurationElement<bool>)this["resetExtendedManagerDataOnVersionChange"];

        /// <inheritdoc/>
        [ConfigurationProperty("resetShippingManagerDataOnVersionChange")]
        internal InnerTextConfigurationElement<bool> ResetShippingManagerDataOnVersionChange => (InnerTextConfigurationElement<bool>)this["resetShippingManagerDataOnVersionChange"];

        /// <inheritdoc/>
        [ConfigurationProperty("resetOfferManagerDataOnVersionChange")]
        internal InnerTextConfigurationElement<bool> ResetOfferManagerDataOnVersionChange => (InnerTextConfigurationElement<bool>)this["resetOfferManagerDataOnVersionChange"];

        /// <inheritdoc/>
        [ConfigurationProperty("emptyBasketOnPaymentSuccess")]
        internal InnerTextConfigurationElement<bool> EmptyBasketOnPaymentSuccess => (InnerTextConfigurationElement<bool>)this["emptyBasketOnPaymentSuccess"];
    }
}