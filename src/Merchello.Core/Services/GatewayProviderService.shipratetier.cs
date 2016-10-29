namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class GatewayProviderService : IShipRateTierService
    {
        /// <inheritdoc/>
        public IEnumerable<IShipRateTier> GetShipRateTiersByShipMethodKey(Guid shipMethodKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipRateTierRepository>();
                var tiers = repo.GetByQuery(repo.Query.Where(x => x.ShipMethodKey == shipMethodKey));
                uow.Complete();
                return tiers;
            }
        }

        /// <inheritdoc/>
        public void Save(IShipRateTier shipRateTier)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipRateTierRepository>();
                repo.AddOrUpdate(shipRateTier);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IShipRateTier> shipRateTierList)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipRateTierRepository>();

                foreach (var tier in shipRateTierList)
                {
                    repo.AddOrUpdate(tier);
                }
                
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IShipRateTier shipRateTier)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.WriteLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IShipRateTierRepository>();
                repo.Delete(shipRateTier);
                uow.Complete();
            }
        }
    }
}
