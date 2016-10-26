namespace Merchello.Core.Gateways.Payment
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a payment gateway method
    /// </summary>
    public interface IPaymentGatewayMethod : IGatewayMethod
    {
        /// <summary>
        /// Gets the <see cref="IPaymentMethod"/>
        /// </summary>
        IPaymentMethod PaymentMethod { get; }

        /// <summary>
        /// Authorizes a payment for the <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        IPaymentResult AuthorizePayment(IInvoice invoice, ProcessorArgumentCollection args);

        /// <summary>
        /// Authorizes and Captures a Payment
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="amount">The amount of the payment to the invoice</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        IPaymentResult AuthorizeCapturePayment(IInvoice invoice, decimal amount, ProcessorArgumentCollection args);

        /// <summary>
        /// Captures a payment for the <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="payment">The payment to be captured</param>
        /// <param name="amount">The amount to the payment to be captured</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        IPaymentResult CapturePayment(IInvoice invoice, IPayment payment, decimal amount, ProcessorArgumentCollection args);

        /// <summary>
        /// Refunds a payment
        /// </summary>
        /// <param name="invoice">The invoice to be the payment was applied</param>
        /// <param name="payment">The payment to be refunded</param>
        /// <param name="amount">The amount of the payment to be refunded</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        IPaymentResult RefundPayment(IInvoice invoice, IPayment payment, decimal amount, ProcessorArgumentCollection args);

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="invoice">The invoice associated with the payment to be voided</param>
        /// <param name="payment">The payment to be voided</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        IPaymentResult VoidPayment(IInvoice invoice, IPayment payment, ProcessorArgumentCollection args);
    }
}