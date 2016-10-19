namespace Merchello.Core.Services
{
    using System;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
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
            throw new NotImplementedException();
        }
    }
}