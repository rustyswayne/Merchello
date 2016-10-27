namespace Merchello.Core.Gateways.Payment
{
    using Merchello.Core.Models;

    using NodaMoney;

    /// <summary>
    /// The authorize operation data.
    /// </summary>
    public class AuthorizeOperationData
    {
        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        public IInvoice Invoice { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public Money Amount { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IPaymentMethod"/>.
        /// </summary>
        public IPaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the processor argument collection.
        /// </summary>
        public ProcessorArgumentCollection ProcessorArgumentCollection { get; set; }
    }
}