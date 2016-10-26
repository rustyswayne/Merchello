namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
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
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByPaymentMethodKey(Guid? paymentMethodKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByInvoiceKey(Guid invoiceKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IPayment> GetByCustomerKey(Guid customerKey)
        {
            throw new NotImplementedException();
        }
    }
}