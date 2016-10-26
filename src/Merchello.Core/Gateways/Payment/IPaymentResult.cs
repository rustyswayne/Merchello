namespace Merchello.Core.Gateways.Payment
{
    using System;

    using Models;

    /// <summary>
    /// Represents the result of a payment transaction attempt
    /// </summary>
    public interface IPaymentResult
    {
        /// <summary>
        /// Gets a value indicating whether success.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the <see cref="IPayment"/>
        /// </summary>
        IPayment Payment { get; }

        /// <summary>
        /// Gets the invoice
        /// </summary>
        IInvoice Invoice { get; }
        
        /// <summary>
        /// Gets or sets a value indicating whether or not the sales preparation should generate the <see cref="IOrder"/> and <see cref="IShipment"/>(s)
        /// </summary>
        bool ApproveOrderCreation { get; set; }

        /// <summary>
        /// Gets the redirect url.
        /// </summary>
        string RedirectUrl { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        Exception Exception { get; }
    }
}