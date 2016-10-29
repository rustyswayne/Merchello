namespace Merchello.Providers.Taxation.FixedRate
{
    using Merchello.Core.DI;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Models;

    /// <summary>
    /// The fix rate taxation gateway method.
    /// </summary>
    public class FixRateTaxationGatewayMethod : TaxationGatewayMethodBase, IFixedRateTaxationGatewayMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FixRateTaxationGatewayMethod"/> class.
        /// </summary>
        /// <param name="taxMethod">
        /// The tax method.
        /// </param>
        public FixRateTaxationGatewayMethod(ITaxMethod taxMethod)
            : base(taxMethod)
        {
        }

        /// <summary>
        /// The calculate tax for invoice.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <param name="taxAddress">
        /// The tax address.
        /// </param>
        /// <returns>
        /// The <see cref="ITaxCalculationResult"/>.
        /// </returns>
        public override ITaxCalculationResult CalculateTaxForInvoice(IInvoice invoice, IAddress taxAddress)
        {            
            var strategy = MC.Container.GetInstance<IInvoice, IAddress, ITaxMethod, ITaxCalculationStrategy>(invoice, taxAddress, this.TaxMethod);
            return this.CalculateTaxForInvoice(strategy);
        }


        /// <summary>
        /// Calculates taxes for a product.
        /// </summary>
        /// <param name="product">
        /// The <see cref="IProductVariantDataModifierData"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ITaxCalculationResult"/>.
        /// </returns>
        public virtual IProductTaxCalculationResult CalculateTaxForProduct(IProductVariantDataModifierData product)
        {            
            var baseTaxRate = this.TaxMethod.PercentageTaxRate;

            var taxRate = baseTaxRate > 1 ? baseTaxRate / 100M : baseTaxRate;

            var priceCalc = product.Price * taxRate;

            var salePriceCalc = product.SalePrice * taxRate;

            return new ProductTaxCalculationResult(this.TaxMethod.Name, product.Price, priceCalc, product.SalePrice, salePriceCalc, baseTaxRate);
        }
    }
}