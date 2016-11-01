namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class PaymentService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IPaymentService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseBulkUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public PaymentService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IPayment GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                var payment = repo.Get(key);
                uow.Complete();
                return payment;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                var payments = repo.GetAll(keys);
                uow.Complete();
                return payments;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByPaymentMethodKey(Guid? paymentMethodKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                var payments = repo.GetByQuery(repo.Query.Where(x => x.PaymentMethodKey == paymentMethodKey));
                uow.Complete();
                return payments;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByInvoiceKey(Guid invoiceKey)
        {
            var applied = GetAppliedPaymentsByInvoiceKey(invoiceKey);

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                var payments = repo.GetAll(applied.Select(x => x.PaymentKey).ToArray());
                uow.Complete();
                return payments;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByCustomerKey(Guid customerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IPaymentRepository>();
                var payments = repo.GetByQuery(repo.Query.Where(x => x.CustomerKey == customerKey));
                uow.Complete();
                return payments;
            }
        }
    }
}