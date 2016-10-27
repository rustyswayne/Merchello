namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IGatewayProviderPaymentService
    {
        /// <inheritdoc/>
        public IPayment CreatePayment(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey)
        {
            return _paymentService.Value.Create(paymentMethodType, amount, paymentMethodKey);
        }

        /// <inheritdoc/>
        public void Save(IPayment payment)
        {
            _paymentService.Value.Save(payment);
        }


        /// <summary>
        /// Gets a collection of <see cref="IAppliedPayment"/>s by the payment key
        /// </summary>
        /// <param name="paymentKey">The payment key</param>
        /// <returns>A collection of <see cref="IAppliedPayment"/></returns>
        public IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey)
        {
            return _paymentService.Value.GetAppliedPaymentsByPaymentKey(paymentKey);
        }

        /// <inheritdoc/>
        public IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount)
        {
            return _paymentService.Value.ApplyPaymentToInvoice(paymentKey, invoiceKey, appliedPaymentType, description, amount);
        }

        /// <inheritdoc/>
        public void Save(IAppliedPayment appliedPayment)
        {
            _paymentService.Value.Save(appliedPayment);
        }
    }
}
