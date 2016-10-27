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

    public partial class InvoiceService : EntityCollectionEntityServiceBase<IInvoice, IDatabaseUnitOfWorkProvider, IInvoiceRepository>, IInvoiceService
    {
        public InvoiceService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory, Constants.Locks.SalesTree)
        {
        }

        public IInvoice GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInvoice> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInvoice> GetByPaymentKey(Guid paymentKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInvoice> GetByCustomerKey(Guid customeryKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IInvoice> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IInvoice GetByInvoiceNumber(int invoiceNumber)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public int Count(DateTime startDate, DateTime endDate, CustomerType customerType)
        {
            throw new NotImplementedException();
        }

        public Money SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode)
        {
            throw new NotImplementedException();
        }

        public Money SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku)
        {
            throw new NotImplementedException();
        }
    }
}