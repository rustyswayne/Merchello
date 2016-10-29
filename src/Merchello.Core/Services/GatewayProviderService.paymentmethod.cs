namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IPaymentMethodService
    {
        /// <inheritdoc/>
        public IPaymentMethod GetPaymentMethodByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                var method = repo.Get(key);
                uow.Complete();
                return method;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPaymentMethod> GetPaymentMethodsByProviderKey(Guid providerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                var method = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey));
                uow.Complete();
                return method;
            }
        }

        /// <inheritdoc/>
        public IPaymentMethod GetPaymentMethodByPaymentCode(Guid providerKey, string paymentCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                var methods = repo.GetByQuery(repo.Query.Where(x => x.ProviderKey == providerKey && x.PaymentCode == paymentCode));
                uow.Complete();
                return methods.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public Attempt<IPaymentMethod> CreatePaymentMethodWithKey(Guid providerKey, string name, string description, string paymentCode)
        {
            if (name.IsNullOrWhiteSpace() || paymentCode.IsNullOrWhiteSpace())
            {
                var empty = new Exception("Cannot create payment method without a name and payment code");
                return Attempt<IPaymentMethod>.Fail(empty);
            }

            var method = new PaymentMethod(providerKey)
            {
                Name = name,
                PaymentCode = paymentCode
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                if (repo.Exists(providerKey, paymentCode))
                {
                    uow.Complete();
                    var invalid = new ConstraintException("Payment method already exists");
                    return Attempt<IPaymentMethod>.Fail(invalid);
                }
                repo.AddOrUpdate(method);
                uow.Complete();
            }

            return Attempt<IPaymentMethod>.Succeed(method);
        }

        /// <inheritdoc/>
        public void Save(IPaymentMethod paymentMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                repo.AddOrUpdate(paymentMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IPaymentMethod> paymentMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();

                foreach (var method in paymentMethods)
                {
                    repo.AddOrUpdate(method);
                }
                
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IPaymentMethod paymentMethod)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();
                repo.Delete(paymentMethod);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IPaymentMethod> paymentMethods)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IPaymentMethodRepository>();

                foreach (var method in paymentMethods)
                {
                    repo.Delete(method);
                }

                uow.Complete();
            }
        }
    }
}
