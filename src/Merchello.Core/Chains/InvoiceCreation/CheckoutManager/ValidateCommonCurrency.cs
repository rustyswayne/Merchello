namespace Merchello.Core.Chains.InvoiceCreation.CheckoutManager
{
    using System.IO;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    using Constants = Merchello.Core.Constants;

    /// <summary>
    /// The validate common currency.
    /// </summary>
    internal class ValidateCommonCurrency : CheckoutManagerInvoiceCreationAttemptChainTaskBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCommonCurrency"/> class.
        /// </summary>
        /// <param name="checkoutManager">
        /// The <see cref="ICheckoutManagerBase"/>.
        /// </param>
        public ValidateCommonCurrency(ICheckoutManagerBase checkoutManager)
            : base(checkoutManager)
        {
        }

        /// <summary>
        /// Performs the task of asserting everything is billed in a common currency.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="Attempt"/>.
        /// </returns>
        public override Attempt<IInvoice> PerformTask(IInvoice value)
        {
            var allCurrencyCodes =
                value.Items.Select(x => x.Price.Currency.Code).Distinct().ToArray();

            return 1 == allCurrencyCodes.Length && value.CurrencyCode == allCurrencyCodes.First()
                       ? Attempt<IInvoice>.Succeed(value)
                       : Attempt<IInvoice>.Fail(new InvalidDataException("Invoice is being created with line items costed in different currencies."));
        }
    }
}