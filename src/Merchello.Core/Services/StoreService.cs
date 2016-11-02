namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public partial class StoreService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, IStoreService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
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
        public StoreService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <inheritdoc/>
        public IStore GetByKey(Guid key)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IStoreRepository>();
                var store = repo.Get(key);
                uow.Complete();
                return store;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IStore> GetAll(params Guid[] keys)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IStoreRepository>();
                var stores = repo.GetAll(keys);
                uow.Complete();
                return stores;
            }
        }

        /// <inheritdoc/>
        public IStore GetByAlias(string alias)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<IStoreRepository>();
                var store = repo.GetByQuery(repo.Query.Where(x => x.Alias == alias.ToLowerInvariant()));
                uow.Complete();
                return store.FirstOrDefault();
            }
        }

        /// <summary>
        /// Ensures that an alias is unique.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <param name="existing">
        /// The existing.
        /// </param>
        /// <returns>
        /// A unique store alias.
        /// </returns>
        /// <remarks>
        /// Allows for testing.
        /// </remarks>
        internal static string EnsureUniqueStoreAlias(string alias, string[] existing)
        {
            alias = alias.Replace("-", string.Empty);
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            alias = rgx.Replace(alias, string.Empty).ToLowerInvariant();

            var modIndex = 0;
            var temp = alias;
            while (existing.Contains(temp))
            {
                temp = alias;
                modIndex++;
                temp += modIndex;
            }

            return modIndex == 0 ? alias : alias + modIndex;
        }

        /// <summary>
        /// Ensures an alias is unique.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private string EnsureUniqueStoreAlias(string alias)
        {
            var stores = GetAll().ToArray();
            return EnsureUniqueStoreAlias(alias, stores.Select(x => x.Alias).ToArray());
        }

        /// <summary>
        /// Ensures a store can be deleted before a delete is executed.
        /// </summary>
        /// <returns>
        /// A value indicating whether or not the store should be deleted.
        /// </returns>
        /// <remarks>
        /// Not implemented.
        /// </remarks>
        private bool EnsureStoreCanBeDeleted()
        {
            MultiLogHelper.Warn<IStoreService>("Returning a false for the time being.  TODO fixme.  Store will not be deleted.");
            return false;
        }
    }
}