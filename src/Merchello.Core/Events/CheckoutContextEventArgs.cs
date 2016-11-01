namespace Merchello.Core.Events
{
    using Merchello.Core.Checkout;
    using Merchello.Core.Models;

    /// <summary>
    /// The <see cref="ICheckoutContext"/> event args.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the second argument
    /// </typeparam>
    public class CheckoutEventArgs<T> : CancellableObjectEventArgs<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutEventArgs{T}"/> class.
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        public CheckoutEventArgs(ICustomerBase customer, T item)
            : base(item, true)
        {
            Ensure.ParameterNotNull(customer, "customer");
            Ensure.ParameterNotNull(item, "item");

            this.Customer = customer;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        public ICustomerBase Customer { get; private set; }

        /// <summary>
        /// Gets the item.
        /// </summary>
        public T Item => this.EventObject;
    }
}