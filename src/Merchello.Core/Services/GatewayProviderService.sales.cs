namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IGatewayProviderSalesService
    {
        /// <inheritdoc/>
        public void Save(IInvoice invoice)
        {
            _invoiceService.Value.Save(invoice);
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
    }
}
