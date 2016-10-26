namespace Merchello.Core.Gateways.Payment
{
    using System;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a Result
    /// </summary>
    public class PaymentResult : IPaymentResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentResult"/> class.
        /// </summary>
        /// <param name="attempt">
        /// The payment attempt.
        /// </param>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <param name="approveOrderCreation">
        /// The approve order creation.
        /// </param>
        public PaymentResult(Attempt<IPayment> attempt, IInvoice invoice, bool approveOrderCreation)
            : this(attempt, invoice, approveOrderCreation, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentResult"/> class.
        /// </summary>
        /// <param name="attempt">
        /// The payment attempt.
        /// </param>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <param name="approveOrderCreation">
        /// The approve order creation.
        /// </param>
        /// <param name="redirectUrl">
        /// The redirect URL.
        /// </param>
        public PaymentResult(Attempt<IPayment> attempt, IInvoice invoice, bool approveOrderCreation, string redirectUrl)
        {
            Payment = attempt.Result;
            Success = attempt.Success;
            Invoice = invoice;
            ApproveOrderCreation = approveOrderCreation;
            RedirectUrl = redirectUrl;
            Exception = attempt.Exception;
        }

        /// <inheritdoc/>
        public bool Success { get; }

        /// <summary>
        /// Gets the Result
        /// </summary>
        public IPayment Payment { get; }

        /// <summary>
        /// Gets the invoice.
        /// </summary>
        public IInvoice Invoice { get; }

        /// <summary>
        /// Gets a value indicating whether or not an order should be generated
        /// as a result of this payment
        /// </summary>
        public bool ApproveOrderCreation { get; set; }

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        public string RedirectUrl { get; internal set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception { get; }
    }
}