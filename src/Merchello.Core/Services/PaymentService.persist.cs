namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class PaymentService : IPaymentService
    {
        /// <inheritdoc/>
        public IPayment Create(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey)
        {
            var tfKey = EnumTypeFieldConverter.PaymentMethod.GetTypeField(paymentMethodType).TypeKey;
            return Create(tfKey, amount, paymentMethodKey);
        }

        /// <inheritdoc/>
        public IPayment Create(Guid paymentMethodTfKey, Money amount, Guid? paymentMethodKey)
        {
            var payment = new Payment(paymentMethodTfKey, amount, paymentMethodKey, new ExtendedDataCollection());

            if (Creating.IsRaisedEventCancelled(new NewEventArgs<IPayment>(payment), this))
            {
                payment.WasCancelled = true;
                return payment;
            }

            Created.RaiseEvent(new NewEventArgs<IPayment>(payment, false), this);

            return payment;
        }

        /// <inheritdoc/>
        public IPayment CreateWithKey(PaymentMethodType paymentMethodType, Money amount, Guid? paymentMethodKey)
        {
            var payment = Create(paymentMethodType, amount, paymentMethodKey);
            Save(payment);
            return payment;
        }

        /// <inheritdoc/>
        public void Save(IPayment entity)
        {
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IPayment>(entity), this))
            {
                ((Payment)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IPayment>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IPayment> entities)
        {
            var paymentsArr = entities as IPayment[] ?? entities.ToArray();
            if (Saving.IsRaisedEventCancelled(new SaveEventArgs<IPayment>(paymentsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                foreach (var payment in paymentsArr)
                {
                    repo.AddOrUpdate(payment);
                }
               
                uow.Complete();
            }

            Saved.RaiseEvent(new SaveEventArgs<IPayment>(paymentsArr, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IPayment entity)
        {
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IPayment>(entity), this))
            {
                ((Payment)entity).WasCancelled = true;
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                repo.AddOrUpdate(entity);
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IPayment>(entity, false), this);
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IPayment> entities)
        {
            var paymentsArr = entities as IPayment[] ?? entities.ToArray();
            if (Deleting.IsRaisedEventCancelled(new DeleteEventArgs<IPayment>(paymentsArr), this))
            {
                return;
            }

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                foreach (var payment in paymentsArr)
                {
                    repo.AddOrUpdate(payment);
                }
                uow.Complete();
            }

            Deleted.RaiseEvent(new DeleteEventArgs<IPayment>(paymentsArr, false), this);
        }
    }
}
