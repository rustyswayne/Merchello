namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class PaymentService : IAppliedPaymentService
    {
        /// <inheritdoc/>
        public IAppliedPayment GetAppliedPaymentByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                var ap = repo.Get(key);
                uow.Complete();
                return ap;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IAppliedPayment> GetAppliedPaymentsByPaymentKey(Guid paymentKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                var aps = repo.GetByQuery(repo.Query.Where(x => x.PaymentKey == paymentKey));
                uow.Complete();
                return aps;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IAppliedPayment> GetAppliedPaymentsByInvoiceKey(Guid invoiceKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                var aps = repo.GetByQuery(repo.Query.Where(x => x.InvoiceKey == invoiceKey));
                uow.Complete();
                return aps;
            }
        }

        /// <inheritdoc/>
        public IAppliedPayment ApplyPaymentToInvoice(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount)
        {
            var ap = CreateAppliedPayment(paymentKey, invoiceKey, appliedPaymentType, description, amount);
            Save(ap);
            return ap;
        }

        /// <inheritdoc/>
        public IAppliedPayment CreateAppliedPayment(Guid paymentKey, Guid invoiceKey, AppliedPaymentType appliedPaymentType, string description, Money amount)
        {
            var tfKey = EnumTypeFieldConverter.AppliedPayment.GetTypeField(appliedPaymentType).TypeKey;
            return CreateAppliedPayment(paymentKey, invoiceKey, tfKey, description, amount);
        }

        /// <inheritdoc/>
        public IAppliedPayment CreateAppliedPayment(Guid paymentKey, Guid invoiceKey, Guid appliedPaymentTfKey, string description, Money amount)
        {
            Ensure.ParameterCondition(!Guid.Empty.Equals(paymentKey), "paymentKey");
            Ensure.ParameterCondition(!Guid.Empty.Equals(invoiceKey), "invoiceKey");
            Ensure.ParameterCondition(!Guid.Empty.Equals(appliedPaymentTfKey), "appliedPaymentTfKey");

            var appliedPayment = new AppliedPayment(paymentKey, invoiceKey, appliedPaymentTfKey)
            {
                Description = description,
                Amount = amount,
                Exported = false
            };

            return appliedPayment;
        }

        /// <inheritdoc/>
        public void Save(IAppliedPayment appliedPayment)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                repo.AddOrUpdate(appliedPayment);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IAppliedPayment> appliedPayments)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                foreach (var ap in appliedPayments)
                {
                    repo.AddOrUpdate(ap);
                }
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IAppliedPayment appliedPayment)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                repo.Delete(appliedPayment);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IAppliedPayment> appliedPayments)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                foreach (var ap in appliedPayments)
                {
                    repo.Delete(ap);
                }
                uow.Complete();
            }
        }
    }
}
