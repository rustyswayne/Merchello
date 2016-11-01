namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;

    /// <inheritdoc/>
    public partial class OfferSettingsService : IOfferRedeemedService
    {
        /// <inheritdoc/>
        public IOfferRedeemed GetOfferRedeemedByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.Key == key));
                uow.Complete();
                return redeemed.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferRedeemed> GetOfferRedeemedByInvoiceKey(Guid invoiceKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.InvoiceKey == invoiceKey));
                uow.Complete();
                return redeemed;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferRedeemed> GetOfferRedeemedByCustomerKey(Guid customerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.CustomerKey == customerKey));
                uow.Complete();
                return redeemed;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferSettingsKey(Guid offerSettingsKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.OfferSettingsKey == offerSettingsKey));
                uow.Complete();
                return redeemed;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferSettingsKeyAndCustomerKey(Guid offerSettingsKey, Guid customerKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.OfferSettingsKey == offerSettingsKey && x.CustomerKey == customerKey));
                uow.Complete();
                return redeemed;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferRedeemed> GetOfferRedeemedByOfferProviderKey(Guid offerProviderKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var redeemed = repo.GetByQuery(repo.Query.Where(x => x.OfferProviderKey == offerProviderKey));
                uow.Complete();
                return redeemed;
            }
        }

        /// <inheritdoc/>
        public int GetOfferRedeemedCount(Guid offerSettingsKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                var count = repo.Count(repo.Query.Where(x => x.OfferSettingsKey == offerSettingsKey));
                uow.Complete();
                return count;
            }
        }

        /// <inheritdoc/>
        public IOfferRedeemed CreateOfferRedeemedWithKey(IOfferSettings offerSettings, IInvoice invoice)
        {
            var redemption = new OfferRedeemed(
                        offerSettings.OfferCode,
                        offerSettings.OfferProviderKey,
                        invoice.Key,
                        offerSettings.Key);

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                repo.AddOrUpdate(redemption);
                uow.Complete();
            }

            return redemption;
        }

        /// <inheritdoc/>
        public void Save(IOfferRedeemed redeemed)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                repo.AddOrUpdate(redeemed);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IOfferRedeemed> redemptions)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                foreach (var redeemed in redemptions)
                {
                    repo.AddOrUpdate(redeemed);
                }
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IOfferRedeemed redeemed)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                repo.Delete(redeemed);
                uow.Complete();
            }
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IOfferRedeemed> redemptions)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferRedeemedRepository>();
                foreach (var redeemed in redemptions)
                {
                    repo.Delete(redeemed);
                }
                uow.Complete();
            }
        }
    }
}
