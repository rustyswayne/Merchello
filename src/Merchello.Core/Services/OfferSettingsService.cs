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
    public partial class OfferSettingsService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IOfferSettingsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferSettingsService"/> class.
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
        public OfferSettingsService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IOfferSettings GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                var settings = repo.Get(key);
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferSettings> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                var settings = repo.GetAll();
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public IOfferSettings GetByOfferCode(string offerCode)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                var settings = repo.GetByQuery(repo.Query.Where(x => x.OfferCode.Equals(offerCode, StringComparison.InvariantCultureIgnoreCase)));
                uow.Complete();
                return settings.FirstOrDefault();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferSettings> GetByProviderKey(Guid offerProviderKey, bool activeOnly = true)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.MarketingTree);
                var repo = uow.CreateRepository<IOfferSettingsRepository>();
                var query = activeOnly
                                ? repo.Query.Where(x => x.OfferProviderKey == offerProviderKey && x.Active)
                                : repo.Query.Where(x => x.OfferProviderKey == offerProviderKey);
                var settings = repo.GetByQuery(query);
                uow.Complete();
                return settings;
            }
        }
    }
}