namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class GatewayProviderService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IGatewayProviderService
    {
        /// <summary>
        /// The <see cref="IInvoiceService"/>.
        /// </summary>
        private readonly Lazy<IInvoiceService> _invoiceService;

        /// <summary>
        /// The <see cref="IPaymentService"/>.
        /// </summary>
        private readonly Lazy<IPaymentService> _paymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderService"/> class.
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
        /// <param name="invoiceService">
        /// The <see cref="IInvoiceService"/>
        /// </param>
        /// <param name="paymentService">
        /// The <see cref="IPaymentService"/>
        /// </param>
        public GatewayProviderService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory, Lazy<IInvoiceService> invoiceService, Lazy<IPaymentService> paymentService)
            : base(provider, logger, eventMessagesFactory)
        {
            Ensure.ParameterNotNull(invoiceService, nameof(invoiceService));
            Ensure.ParameterNotNull(paymentService, nameof(paymentService));

            _invoiceService = invoiceService;
            _paymentService = paymentService;
        }


        /// <inheritdoc/>
        public IEnumerable<IInvoiceStatus> GetAllInvoiceStatuses()
        {
            return _invoiceService.Value.GetAllInvoiceStatuses();
        }

        /// <inheritdoc/>
        public IEnumerable<IOrderStatus> GetAllOrderStatuses()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IOrderStatusRepository>();
                var statuses = repo.GetAll();
                uow.Complete();
                return statuses;
            }
        }

        /// <inheritdoc/>
        public IWarehouse GetDefaultWarehouse()
        {
            //// TODO this will need to change with multi-store.

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IWarehouseRepository>();
                var warehouse = repo.Get(Constants.Warehouse.DefaultWarehouseKey);
                uow.Complete();
                return warehouse;
            }
        }

        /// <inheritdoc/>
        public void Save(IInvoice invoice)
        {
            _invoiceService.Value.Save(invoice);
        }

        /// <inheritdoc/>
        public IPayment CreatePayment(PaymentMethodType paymentMethodType, decimal amount, Guid? paymentMethodKey)
        {
            return _paymentService.Value.Create(paymentMethodType, amount, paymentMethodKey);
        }

        /// <inheritdoc/>
        public void Save(IPayment payment)
        {
            _paymentService.Value.Save(payment);
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