namespace Merchello.Providers.Shipping.FixedRate
{
    using Merchello.Core;
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// Visitor class that calculates shipping per line item
    /// </summary>
    public class FixedRateShipmentLineItemVisitor : ILineItemVisitor
    {
        /// <summary>
        /// The currency code.
        /// </summary>
        private string _currencyCode;


        /// <summary>
        /// Initializes a new instance of the <see cref="FixedRateShipmentLineItemVisitor"/> class.
        /// </summary>
        /// <param name="currencyCode">
        /// The currency Code.
        /// </param>
        public FixedRateShipmentLineItemVisitor(string currencyCode)
        {
            Ensure.ParameterNotNullOrEmpty(currencyCode, nameof(currencyCode));
            _currencyCode = currencyCode;

            this.TotalPrice = new Money(0M, _currencyCode);
            this.TotalWeight = 0M;
            this.UseOnSalePriceIfOnSale = false;
        }


        /// <summary>
        /// Gets the TotalWeight from ExtendedData
        /// </summary>
        public decimal TotalWeight { get; private set; }

        /// <summary>
        /// Gets the total price
        /// </summary>
        public Money TotalPrice { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to use the OnSale price in the total price calculation
        /// </summary>
        public bool UseOnSalePriceIfOnSale { get; set; }

        /// <inheritdoc/>
        public void Visit(ILineItem lineItem)
        {
            if (!lineItem.ExtendedData.DefinesProductVariant()) return;

            // adjust the total weight
            this.TotalWeight += lineItem.ExtendedData.GetWeightValue() * lineItem.Quantity;

            // adjust the total price
            if (this.UseOnSalePriceIfOnSale)
            {
                this.TotalPrice += lineItem.ExtendedData.GetOnSaleValue()
                    ? new Money(lineItem.ExtendedData.GetSalePriceValue(), _currencyCode) * lineItem.Quantity
                    : new Money(lineItem.ExtendedData.GetPriceValue(), _currencyCode) * lineItem.Quantity;
            }
            else
            {
                this.TotalPrice += lineItem.ExtendedData.GetPriceValue() * lineItem.Quantity;
            }
        }
    }
}
