namespace Merchello.Core.Builders
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Chains;
    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    /// <summary>
    /// A builder chain used by the checkout manager to create invoices.
    /// </summary>
    internal sealed class InvoiceBuilderChain : BuildChainBase<IInvoice>
    {
        /// <summary>
        /// Gets the <see cref="ICheckoutManagerBase"/>.
        /// </summary>
        private readonly ICheckoutManagerBase _checkoutManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceBuilderChain"/> class.
        /// </summary>
        /// <param name="register">
        /// The <see cref="IAttemptChainTaskRegister{IInvoice}"/>
        /// </param>
        /// <param name="checkoutManager">
        /// The checkout manager.
        /// </param>
        internal InvoiceBuilderChain(IAttemptChainTaskRegister<IInvoice> register, ICheckoutManagerBase checkoutManager)
            : base(register)
        {
            Ensure.ParameterNotNull(checkoutManager, nameof(checkoutManager));

            _checkoutManager = checkoutManager;
        }

        /// <summary>
        /// Gets the count of tasks - Used for testing
        /// </summary>
        internal int TaskCount => this.TaskHandlers.Count();

        /// <summary>
        /// Builds the invoice
        /// </summary>
        /// <returns>The Attempt{IInvoice} representing the successful creation of an invoice</returns>
        public override Attempt<IInvoice> Build()
        {
            var unpaid =
                _checkoutManager.Context.Services.InvoiceService.GetInvoiceStatusByKey(Core.Constants.InvoiceStatus.Unpaid);

            if (unpaid == null)
                return Attempt<IInvoice>.Fail(new NullReferenceException("Unpaid invoice status query returned null"));

            var invoice = new Invoice(unpaid) { VersionKey = _checkoutManager.Context.VersionKey };

            // Associate a customer with the invoice if it is a known customer.
            if (!_checkoutManager.Context.Customer.IsAnonymous) invoice.CustomerKey = _checkoutManager.Context.Customer.Key;

            var attempt = TaskHandlers.Any()
                       ? TaskHandlers.First().Execute(invoice)
                       : Attempt<IInvoice>.Fail(new InvalidOperationException("The configuration Chain Task List could not be instantiated"));

            if (!attempt.Success) return attempt;


            var charges = attempt.Result.Items.Where(x => x.LineItemType != LineItemType.Discount).Select(x => x.TotalPrice).Sum();
            var discounts = attempt.Result.Items.Where(x => x.LineItemType == LineItemType.Discount).Select(x => x.TotalPrice).Sum();

            // total the invoice
            attempt.Result.Total = charges - discounts;

            return attempt;
        }
    }
}