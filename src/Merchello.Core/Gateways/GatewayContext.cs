namespace Merchello.Core.Gateways
{
    using System;

    using Merchello.Core.Gateways.Notification;
    using Merchello.Core.Gateways.Payment;
    using Merchello.Core.Gateways.Shipping;
    using Merchello.Core.Gateways.Taxation;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents the GatewayContext.  Provides access to <see cref="IGatewayProviderSettings"/>s
    /// </summary>
    internal class GatewayContext : IGatewayContext
    {
        /// <summary>
        /// The notification context.
        /// </summary>
        private readonly Lazy<INotificationContext> _notification;

        /// <summary>
        /// The payment context.
        /// </summary>
        private readonly Lazy<IPaymentContext> _payment;

        /// <summary>
        /// The shipping context.
        /// </summary>
        private readonly Lazy<IShippingContext> _shipping;

        /// <summary>
        /// The taxation context.
        /// </summary>
        private readonly Lazy<ITaxationContext> _taxation;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayContext"/> class.
        /// </summary>
        /// <param name="notificationContext">
        /// The <see cref="INotificationContext"/>.
        /// </param>
        /// <param name="paymentContext">
        /// The <see cref="IPaymentContext"/>.
        /// </param>
        /// <param name="shippingContext">
        /// The <see cref="IShippingContext"/>.
        /// </param>
        /// <param name="taxationContext">
        /// The <see cref="ITaxationContext"/>.
        /// </param>
        public GatewayContext(
            Lazy<INotificationContext> notificationContext,
            Lazy<IPaymentContext> paymentContext,
            Lazy<IShippingContext> shippingContext,
            Lazy<ITaxationContext> taxationContext)
        {
            _notification = notificationContext;
            _payment = paymentContext;
            _shipping = shippingContext;
            _taxation = taxationContext;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayContext"/> class.
        /// </summary>
        /// <param name="notificationContext">
        /// The <see cref="INotificationContext"/>.
        /// </param>
        /// <param name="paymentContext">
        /// The <see cref="IPaymentContext"/>.
        /// </param>
        /// <param name="shippingContext">
        /// The <see cref="IShippingContext"/>.
        /// </param>
        /// <param name="taxationContext">
        /// The <see cref="ITaxationContext"/>.
        /// </param>
        public GatewayContext(
            INotificationContext notificationContext = null,
            IPaymentContext paymentContext = null,
            IShippingContext shippingContext = null,
            ITaxationContext taxationContext = null)
        {
            if (notificationContext != null) _notification = new Lazy<INotificationContext>(() => notificationContext);
            if (paymentContext != null) _payment = new Lazy<IPaymentContext>(() => paymentContext);
            if (shippingContext != null) _shipping = new Lazy<IShippingContext>(() => shippingContext);
            if (taxationContext != null) _taxation = new Lazy<ITaxationContext>(() => taxationContext);
        }

        /// <summary>
        /// Gets the <see cref="IPaymentContext"/>
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if the <see cref="PaymentContext"/> is null
        /// </exception>
        public IPaymentContext Payment
        {
            get
            {
                if (_payment == null) throw new InvalidOperationException("The PaymentContext is not set in the GatewayContext");
                return _payment.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="INotificationContext"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if the <see cref="NotificationContext"/> is null
        /// </exception>
        public INotificationContext Notification
        {
            get
            {
                if (_notification == null) throw new InvalidOperationException("The NotificationContext is not set in the GatewayContext");
                return _notification.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="IShippingContext"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if the <see cref="ShippingContext"/> is null
        /// </exception>
        public IShippingContext Shipping
        {
            get
            {
                if (_shipping == null) throw new InvalidOperationException("The ShippingContext is not set in the GatewayContext");
                return _shipping.Value;
            }
        }

        /// <summary>
        /// Gets the <see cref="ITaxationContext"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throws an exception if the <see cref="TaxationContext"/> is null
        /// </exception>
        public ITaxationContext Taxation
        {
            get
            {
                if (_taxation == null) throw new InvalidOperationException("The TaxationContext is not set in the GatewayContext");
                return _taxation.Value;
            } 
        }
    }
}