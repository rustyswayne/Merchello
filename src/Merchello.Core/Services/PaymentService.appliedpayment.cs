namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class PaymentService : IPaymentService
    {
        /// <inheritdoc/>
        public IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IAppliedPayment appliedPayment)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IAppliedPayment appliedPayment)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IAppliedPayment> appliedPayments)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IAppliedPayment> GetAppliedPaymentsByInvoiceKey(Guid invoiceKey)
        {
            throw new NotImplementedException();
        }
    }
}
