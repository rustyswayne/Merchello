namespace Merchello.Core.Gateways.Payment
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Configuration;
    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Services;

    using NodaMoney;

    /// <summary>
    /// Represents a base GatewayPaymentMethod 
    /// </summary>
    public abstract class PaymentGatewayMethodBase : IPaymentGatewayMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayMethodBase"/> class.
        /// </summary>
        /// <param name="gatewayProviderService">
        /// The gateway provider service.
        /// </param>
        /// <param name="paymentMethod">
        /// The payment method.
        /// </param>
        protected PaymentGatewayMethodBase(IGatewayProviderService gatewayProviderService, IPaymentMethod paymentMethod)
        {
            Ensure.ParameterNotNull(gatewayProviderService, "gatewayProviderService");
            Ensure.ParameterNotNull(paymentMethod, "paymentMethod");

            this.GatewayProviderService = gatewayProviderService;
            this.PaymentMethod = paymentMethod;
        }

        #region Events       

        /// <summary>
        /// The authorizing event handler.  Fires before an authorization attempt.
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, SaveEventArgs<AuthorizeOperationData>> Authorizing;

        /// <summary>
        /// The capturing event handler.  Fires before an authorize capture or capture attempt.
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, SaveEventArgs<PaymentOperationData>> Capturing;

        /// <summary>
        /// The authorize capturing event handler.  Fires after an authorize / capture attempt
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, SaveEventArgs<AuthorizeOperationData>> AuthorizeCapturing;

        /// <summary>
        /// The voiding event handler.  Fires before an void attempt.
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, SaveEventArgs<PaymentOperationData>> Voiding;

        /// <summary>
        /// The refunding event handler.  Fires before an refund attempt.
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, SaveEventArgs<PaymentOperationData>> Refunding;

        /// <summary>
        /// The authorize attempted event handler. Fires after an authorize attempt.
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, PaymentAttemptEventArgs<IPaymentResult>> AuthorizeAttempted;

        /// <summary>
        /// The capture attempted event handler.  Fires after a capture attempt
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, PaymentAttemptEventArgs<IPaymentResult>> CaptureAttempted;

        /// <summary>
        /// The authorize capture attempted event handler.  Fires after an authorize / capture attempt
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, PaymentAttemptEventArgs<IPaymentResult>> AuthorizeCaptureAttempted;

        /// <summary>
        /// The void attempted event handler. Fires after a void attempt
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, PaymentAttemptEventArgs<IPaymentResult>> VoidAttempted;

        /// <summary>
        /// The refund attempted event handler.  Fires after a refund attempt
        /// </summary>
        public static event TypedEventHandler<PaymentGatewayMethodBase, PaymentAttemptEventArgs<IPaymentResult>> RefundAttempted;

        #endregion

        /// <summary>
        /// Gets the <see cref="IPaymentMethod"/>
        /// </summary>
        public IPaymentMethod PaymentMethod { get; }

        /// <summary>
        /// Gets the <see cref="IGatewayProviderService"/>
        /// </summary>
        protected IGatewayProviderService GatewayProviderService { get; }

        /// <summary>
        /// Processes a payment for the <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        public virtual IPaymentResult AuthorizePayment(IInvoice invoice, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(invoice, "invoice");

            var operationData = new AuthorizeOperationData()
            {
                Invoice = invoice,
                PaymentMethod = this.PaymentMethod,
                ProcessorArgumentCollection = args
            };

            Authorizing.RaiseEvent(new SaveEventArgs<AuthorizeOperationData>(operationData), this);

            // persist the invoice
            if (!invoice.HasIdentity)
                GatewayProviderService.Save(invoice);

            // collect the payment authorization
            var response = PerformAuthorizePayment(invoice, args);

            AuthorizeAttempted.RaiseEvent(new PaymentAttemptEventArgs<IPaymentResult>(response), this);

            if (!response.Success) return response;

            AssertPaymentApplied(response, invoice);

            // Check configuration for override on ApproveOrderCreation
            if (!response.ApproveOrderCreation)
                response.ApproveOrderCreation = MerchelloConfig.For.MerchelloSettings().Sales.AlwaysApproveOrderCreation;

            // give response
            return response;
        }

        /// <summary>
        /// Authorizes and Captures a Payment
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="amount">The amount of the payment to the invoice</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        public virtual IPaymentResult AuthorizeCapturePayment(IInvoice invoice, Money amount, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(invoice, "invoice");

            var operationData = new AuthorizeOperationData()
                {
                    Invoice = invoice, 
                    Amount = amount,
                    PaymentMethod = this.PaymentMethod,
                    ProcessorArgumentCollection = args
                };

            AuthorizeCapturing.RaiseEvent(new SaveEventArgs<AuthorizeOperationData>(operationData), this);

            // persist the invoice
            if (!invoice.HasIdentity)
                GatewayProviderService.Save(invoice);

            // authorize and capture the payment
            var response = PerformAuthorizeCapturePayment(invoice, amount, args);

            AuthorizeCaptureAttempted.RaiseEvent(new PaymentAttemptEventArgs<IPaymentResult>(response), this);

            if (!response.Success) return response;

            AssertPaymentApplied(response, invoice);

            invoice.EnsureInvoiceStatus();

            // Check configuration for override on ApproveOrderCreation
            if (!response.ApproveOrderCreation)
                response.ApproveOrderCreation = MerchelloConfig.For.MerchelloSettings().Sales.AlwaysApproveOrderCreation;

            // give response
            return response;
        }

        /// <summary>
        /// Captures a payment for the <see cref="IInvoice"/>
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="payment">The payment to capture</param>
        /// <param name="amount">The amount of the payment to be captured</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        public virtual IPaymentResult CapturePayment(IInvoice invoice, IPayment payment, Money amount, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(invoice, "invoice");

            var operationData = new PaymentOperationData()
                                    {
                                        Invoice = invoice,
                                        Payment = payment,
                                        Amount = amount,
                                        PaymentMethod = this.PaymentMethod,
                                        ProcessorArgumentCollection = args
                                    };

            Capturing.RaiseEvent(new SaveEventArgs<PaymentOperationData>(operationData), this);

            // persist the invoice
            if (!invoice.HasIdentity)
                GatewayProviderService.Save(invoice);

            var response = PerformCapturePayment(invoice, payment, amount, args);

            // Special case where the order has already been created due to configuration override.
            if (invoice.Orders.Any() && response.ApproveOrderCreation)
                ((PaymentResult)response).ApproveOrderCreation = false;

            CaptureAttempted.RaiseEvent(new PaymentAttemptEventArgs<IPaymentResult>(response), this);

            if (!response.Success) return response;

            AssertPaymentApplied(response, invoice);

            invoice.EnsureInvoiceStatus();


            // give response
            return response;
        }

        /// <summary>
        /// Refunds a payment
        /// </summary>
        /// <param name="invoice">The invoice to be the payment was applied</param>
        /// <param name="payment">The payment to be refunded</param>
        /// <param name="amount">The amount to be refunded</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        public virtual IPaymentResult RefundPayment(IInvoice invoice, IPayment payment, Money amount, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(invoice, "invoice");
            if (!invoice.HasIdentity) return new PaymentResult(Attempt<IPayment>.Fail(new InvalidOperationException("Cannot refund a payment on an invoice that cannot have payments")), invoice, false);

            var operationData = new PaymentOperationData()
            {
                Invoice = invoice,
                Payment = payment,
                Amount = amount,
                PaymentMethod = this.PaymentMethod,
                ProcessorArgumentCollection = args
            };

            Refunding.RaiseEvent(new SaveEventArgs<PaymentOperationData>(operationData), this);

            var response = PerformRefundPayment(invoice, payment, amount, args);

            RefundAttempted.RaiseEvent(new PaymentAttemptEventArgs<IPaymentResult>(response), this);

            if (!response.Success) return response;

            invoice.EnsureInvoiceStatus();

            // Force the ApproveOrderCreation to false
            if (response.ApproveOrderCreation) response.ApproveOrderCreation = false;

            // give response
            return response;
        }

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="invoice">The invoice associated with the payment to be voided</param>
        /// <param name="payment">The payment to be voided</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        public virtual IPaymentResult VoidPayment(IInvoice invoice, IPayment payment, ProcessorArgumentCollection args)
        {
            Ensure.ParameterNotNull(invoice, "invoice");
            if (!invoice.HasIdentity) return new PaymentResult(Attempt<IPayment>.Fail(new InvalidOperationException("Cannot void a payment on an invoice that cannot have payments")), invoice, false);

            var operationData = new PaymentOperationData()
            {
                Invoice = invoice,
                Payment = payment,
                PaymentMethod = this.PaymentMethod,
                ProcessorArgumentCollection = args
            };

            Voiding.RaiseEvent(new SaveEventArgs<PaymentOperationData>(operationData), this);

            var response = PerformVoidPayment(invoice, payment, args);

            VoidAttempted.RaiseEvent(new PaymentAttemptEventArgs<IPaymentResult>(response), this);

            if (!response.Success) return response;

            var appliedPayments = payment.AppliedPayments().Where(x => x.TransactionType != AppliedPaymentType.Void);
            foreach (var appliedPayment in appliedPayments)
            {
                appliedPayment.TransactionType = AppliedPaymentType.Void;
                appliedPayment.Amount = 0;

                GatewayProviderService.Save(appliedPayment);
            }

            // Assert the payment has been voided
            if (!payment.Voided)
            {
                payment.Voided = true;
                GatewayProviderService.Save(payment);
            }

            invoice.EnsureInvoiceStatus();

            // Force the ApproveOrderCreation to false
            if (response.ApproveOrderCreation)
                ((PaymentResult)response).ApproveOrderCreation = false;

            // give response
            return response;
        }
        

        /// <summary>
        /// Does the actual work of creating and authorizing the payment
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <param name="args">Any arguments required to process the payment. (Maybe a username, password or some API Key)</param>
        /// <returns>The <see cref="IPaymentResult"/></returns>
        protected abstract IPaymentResult PerformAuthorizePayment(IInvoice invoice, ProcessorArgumentCollection args);

        /// <summary>
        /// Doe the actual work of Authorizes and Captures a Payment
        /// </summary>
        /// <param name="invoice">The invoice to be paid</param>
        /// <param name="amount">The amount of the payment to the invoice</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        protected abstract IPaymentResult PerformAuthorizeCapturePayment(IInvoice invoice, Money amount, ProcessorArgumentCollection args);

        /// <summary>
        /// Does the actual work of capturing a payment
        /// </summary>
        /// <param name="invoice">The <see cref="IInvoice"/></param>
        /// <param name="payment">The payment to be captured</param>
        /// <param name="amount">The amount of the payment to be captured</param>
        /// <param name="args">Any arguments required to process the payment. (Maybe a username, password or some API Key)</param>
        /// <returns>The <see cref="IPaymentResult"/></returns>
        protected abstract IPaymentResult PerformCapturePayment(IInvoice invoice, IPayment payment, Money amount, ProcessorArgumentCollection args);


        /// <summary>
        /// Does the actual work of refunding the payment
        /// </summary>
        /// <param name="invoice">The invoice to be the payment was applied</param>
        /// <param name="payment">The payment to be refunded</param>
        /// <param name="amount">The amount of the payment to be refunded</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        protected abstract IPaymentResult PerformRefundPayment(IInvoice invoice, IPayment payment, Money amount, ProcessorArgumentCollection args);

        /// <summary>
        /// Does the actual work of voiding a payment
        /// </summary>
        /// <param name="invoice">The invoice to which the payment is associated</param>
        /// <param name="payment">The payment to be voided</param>
        /// <param name="args">Additional arguments required by the payment processor</param>
        /// <returns>A <see cref="IPaymentResult"/></returns>
        protected virtual IPaymentResult PerformVoidPayment(IInvoice invoice, IPayment payment, ProcessorArgumentCollection args)
        {
            foreach (var applied in payment.AppliedPayments())
            {
                applied.TransactionType = AppliedPaymentType.Void;
                applied.Amount = 0;
                applied.Description += " - **Void**";
                GatewayProviderService.Save(applied);
            }

            payment.Voided = true;
            GatewayProviderService.Save(payment);

            return new PaymentResult(Attempt<IPayment>.Succeed(payment), invoice, false);
        }

        /// <summary>
        /// The calculate total owed.
        /// </summary>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        /// <returns>
        /// The <see cref="decimal"/>.
        /// </returns>
        protected Money CalculateTotalOwed(IInvoice invoice)
        {
            var applied = invoice.AppliedPayments().ToArray();

            var credits =
                applied.Where(
                    x =>
                    x.AppliedPaymentTfKey.Equals(
                        EnumTypeFieldConverter.AppliedPayment.GetTypeField(AppliedPaymentType.Debit).TypeKey))
                    .Select(y => y.Amount);

            var totalCredits = new Money(0, invoice.CurrencyCode);
            totalCredits = credits.Aggregate(totalCredits, (current, c) => current + c);

            var debits = applied.Where(
                    x =>
                    x.AppliedPaymentTfKey.Equals(
                        EnumTypeFieldConverter.AppliedPayment.GetTypeField(AppliedPaymentType.Credit).TypeKey))
                      .Select(y => y.Amount);

            var totalDebits = new Money(0, invoice.CurrencyCode);
            totalDebits = debits.Aggregate(totalDebits, (current, d) => current + d);
            

            return totalCredits - totalDebits;
        }

        /// <summary>
        /// Provides the assertion that the payment is applied to the invoice
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="invoice">
        /// The invoice.
        /// </param>
        private void AssertPaymentApplied(IPaymentResult response, IInvoice invoice)
        {
            // Apply the payment to the invoice if it was not done in the sub class           
            var payment = response.Payment;
            if (payment.AppliedPayments().FirstOrDefault(x => x.InvoiceKey == invoice.Key) == null)
            {
                GatewayProviderService.ApplyPaymentToInvoice(payment.Key, invoice.Key, AppliedPaymentType.Debit, PaymentMethod.Name, payment.Amount);
            }
        }
    }
}