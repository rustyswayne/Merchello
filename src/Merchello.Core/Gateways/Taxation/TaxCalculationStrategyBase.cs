namespace Merchello.Core.Gateways.Taxation
{
    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    /// <summary>
    /// Defines an invoice taxation strategy base class
    /// </summary>
    public abstract class TaxCalculationStrategyBase : ITaxCalculationStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxCalculationStrategyBase"/> class.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <param name="taxAddress">
        /// The tax address.
        /// </param>
        protected TaxCalculationStrategyBase(IInvoice invoice, IAddress taxAddress)
        {
            Ensure.ParameterNotNull(invoice, "invoice");
            Ensure.ParameterNotNull(taxAddress, "taxAddress");

            this.Invoice = invoice;
            this.TaxAddress = taxAddress;
        }

        /// <summary>
        /// Gets the <see cref="IInvoice"/>
        /// </summary>
        protected IInvoice Invoice { get; }

        /// <summary>
        /// Gets the tax address
        /// </summary>
        protected IAddress TaxAddress { get; }

        /// <summary>
        /// Computes the invoice tax result
        /// </summary>
        /// <returns>
        /// The <see cref="ITaxCalculationResult"/>
        /// </returns>
        public abstract Attempt<ITaxCalculationResult> CalculateTaxesForInvoice();
    }
}