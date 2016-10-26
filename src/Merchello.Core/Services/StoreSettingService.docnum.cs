namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class StoreSettingService : IStoreSettingService
    {
        /// <inheritdoc/>
        public int GetNextInvoiceNumber(int invoicesCount = 1)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var settingRepo = uow.CreateRepository<IStoreSettingRepository>();
                var invRepo = uow.CreateRepository<IInvoiceRepository>();
                var invoiceNumber = settingRepo.GetNextInvoiceNumber(Constants.StoreSetting.NextInvoiceNumberKey, invRepo.GetMaxDocumentNumber, invoicesCount);
                uow.Complete();
                return invoiceNumber;
            }
        }

        /// <inheritdoc/>
        public int GetNextOrderNumber(int ordersCount = 1)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var settingRepo = uow.CreateRepository<IStoreSettingRepository>();
                var orderRepo = uow.CreateRepository<IOrderRepository>();
                var orderNumber = settingRepo.GetNextOrderNumber(Constants.StoreSetting.NextOrderNumberKey, orderRepo.GetMaxDocumentNumber, ordersCount);
                uow.Complete();
                return orderNumber;
            }
        }

        /// <inheritdoc/>
        public int GetNextShipmentNumber(int shipmentsCount = 1)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var settingRepo = uow.CreateRepository<IStoreSettingRepository>();
                var shipRepo = uow.CreateRepository<IShipmentRepository>();
                var shipmentNumber = settingRepo.GetNextShipmentNumber(Constants.StoreSetting.NextOrderNumberKey, shipRepo.GetMaxDocumentNumber, shipmentsCount);
                uow.Complete();
                return shipmentNumber;
            }
        }
    }
}
