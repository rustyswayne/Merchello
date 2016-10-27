namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class PaymentService : IAppliedPaymentService
    {
        /// <inheritdoc/>
        public IAppliedPayment GetAppliedPaymentByKey(Guid key)
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

        /// <inheritdoc/>
        public IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc/>
        public void Save(IAppliedPayment appliedPayment)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IAppliedPayment> appliedPayments)
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
    }
}
