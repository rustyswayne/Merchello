namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class StoreSettingService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IStoreSettingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreSettingService"/> class.
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
        public StoreSettingService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IStoreSetting GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                var setting = repo.Get(key);
                uow.Complete();
                return setting;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IStoreSetting> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                var settings = repo.GetAll(keys);
                uow.Complete();
                return settings;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IStoreSetting> GetByStoreKey(Guid storeKey, bool excludeGlobal = false)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                var settings = repo.GetByStoreKey(storeKey, excludeGlobal);
                uow.Complete();
                return settings;
            }
        }
    }
}