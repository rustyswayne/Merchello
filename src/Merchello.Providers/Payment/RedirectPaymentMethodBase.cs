namespace Merchello.Providers.Payment
{
    using System;

    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Services;

    using NodaMoney;

    /// <summary>
    /// A base payment method for operations that require redirecting to the provider's site to accept the payment.
    /// </summary>
    public abstract class RedirectPaymentMethodBase : PaymentGatewayMethodBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectPaymentMethodBase"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="paymentMethod">
        /// The payment method.
        /// </param>
        protected RedirectPaymentMethodBase(IGatewayProviderService gatewayProviderService, IPaymentMethod paymentMethod)
            : base(gatewayProviderService, paymentMethod)
        {
        }

        /// <summary>
        /// Payment methods derived from <see cref="RedirectPaymentMethodBase"/> cannot implement PerformAuthorizeCapturePayment.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="IPaymentResult"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if this method is invoked.
        /// </exception>
        protected override IPaymentResult PerformAuthorizeCapturePayment(IInvoice invoice, Money amount, ProcessorArgumentCollection args)
        {
            var logData = MultiLogger.GetBaseLoggingData();

            logData.AddCategory("Payment");
            logData.AddCategory("Redirect");

            var invalidOp = new InvalidOperationException("Payment Providers that require redirection cannot perform Authorize & Capture operations.");

            MultiLogHelper.Error<RedirectPaymentMethodBase>("Cannot perform authorize and capture operation", invalidOp, logData);

            throw invalidOp;
        }
    }
}