namespace Merchello.Core.Services
{

    using Merchello.Core.Events;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class PaymentService : IPaymentService
    {
        /// <summary>
        /// Occurs before applying payment.
        /// </summary>
        public static event TypedEventHandler<IPaymentService, SaveEventArgs<IAppliedPayment>> ApplyingPayment;

        /// <summary>
        /// Occurs after payment has been removed.
        /// </summary>
        public static event TypedEventHandler<IPaymentService, SaveEventArgs<IAppliedPayment>> AppliedPayment;

        /// <summary>
        /// Occurs before Create
        /// </summary>
        public static event TypedEventHandler<IPaymentService, NewEventArgs<IPayment>> Creating;

        /// <summary>
        /// Occurs after Create
        /// </summary>
        public static event TypedEventHandler<IPaymentService, NewEventArgs<IPayment>> Created;

        /// <summary>
        /// Occurs before removing payment.
        /// </summary>
        public static event TypedEventHandler<IPaymentService, DeleteEventArgs<IAppliedPayment>> RemovingPayment;

        /// <summary>
        /// Occurs after removing payment.
        /// </summary>
        public static event TypedEventHandler<IPaymentService, DeleteEventArgs<IAppliedPayment>> RemovedPayment;

        /// <summary>
        /// Occurs before Save
        /// </summary>
        public static event TypedEventHandler<IPaymentService, SaveEventArgs<IPayment>> Saving;

        /// <summary>
        /// Occurs after Save
        /// </summary>
        public static event TypedEventHandler<IPaymentService, SaveEventArgs<IPayment>> Saved;

        /// <summary>
        /// Occurs before Delete
        /// </summary>		
        public static event TypedEventHandler<IPaymentService, DeleteEventArgs<IPayment>> Deleting;

        /// <summary>
        /// Occurs after Delete
        /// </summary>
        public static event TypedEventHandler<IPaymentService, DeleteEventArgs<IPayment>> Deleted;
    }
}
