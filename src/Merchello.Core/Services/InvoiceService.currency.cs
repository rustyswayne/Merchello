namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Persistence.Repositories;

    using NodaMoney;

    /// <inheritdoc/>
    public partial class InvoiceService : IInvoiceService
    {
        /// <inheritdoc/>
        public IEnumerable<string> GetDistinctCurrencyCodes()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var codes = repo.GetDistinctCurrencyCodes();
                uow.Complete();
                return codes;
            }
        }


        /// <inheritdoc/>
        public Money SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var amount = repo.SumInvoiceTotals(startDate, endDate, currencyCode);
                uow.Complete();
                return amount;
            }
        }

        /// <inheritdoc/>
        public Money SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.SalesTree);
                var repo = uow.CreateRepository<IInvoiceRepository>();
                var amount = repo.SumLineItemTotalsBySku(startDate, endDate, currencyCode, sku);
                uow.Complete();
                return amount;
            }
        }
    }
}
