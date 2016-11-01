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
    public partial class InvoiceService : EntityCollectionEntityServiceBase<IInvoice, IDatabaseUnitOfWorkProvider, IInvoiceRepository>, IInvoiceService
    {
        /// <summary>
        /// The <see cref="IStoreSettingService"/>.
        /// </summary>
        private readonly IStoreSettingService _storeSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        /// <param name="storeSettingService">
        /// The <see cref="IStoreSettingService"/>
        /// </param>
        public InvoiceService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory, IStoreSettingService storeSettingService)
            : base(provider, logger, eventMessagesFactory, Constants.Locks.SalesTree)
        {
            Ensure.ParameterNotNull(storeSettingService, nameof(storeSettingService));
            _storeSettingService = storeSettingService;
        }

        /// <inheritdoc/>
        public IInvoice GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoice = repo.Get(key);
                uow.Complete();
                return invoice;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoice> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetAll(keys);
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoice> GetByPaymentKey(Guid paymentKey)
        {
            IEnumerable<IAppliedPayment> appliedPayments;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IAppliedPaymentRepository>();
                appliedPayments = repo.GetByQuery(repo.Query.Where(x => x.PaymentKey == paymentKey));
                uow.Complete();
            }

            var keys = appliedPayments.Select(x => x.InvoiceKey).ToArray();
            return GetAll(keys);
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoice> GetByCustomerKey(Guid customeryKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetByQuery(repo.Query.Where(x => x.CustomerKey == customeryKey));
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IInvoice> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetByDateRange(startDate, endDate);
                uow.Complete();
                return invoices;
            }
        }

        /// <inheritdoc/>
        public IInvoice GetByInvoiceNumber(int invoiceNumber)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var invoices = repo.GetByQuery(repo.Query.Where(x => x.InvoiceNumber == invoiceNumber));
                uow.Complete();
                return invoices.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public int Count()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var count = repo.Count(repo.Query.Where(x => x.Key != Guid.Empty));
                uow.Complete();
                return count;
            }
        }

        /// <inheritdoc/>
        public int Count(DateTime startDate, DateTime endDate)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var count = repo.Count(repo.Query.Where(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate));
                uow.Complete();
                return count;
            }
        }

        /// <inheritdoc/>
        public int Count(DateTime startDate, DateTime endDate, CustomerType customerType)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var query = repo.Query.Where(x => x.InvoiceDate >= startDate && x.InvoiceDate <= endDate);
                if (customerType == CustomerType.Anonymous)
                {
                    query.Where(x => x.CustomerKey == null);
                }
                else
                {
                    query.Where(x => x.CustomerKey != null);
                }
                var count = repo.Count(query);
                uow.Complete();
                return count;
            }
        }
    }
}