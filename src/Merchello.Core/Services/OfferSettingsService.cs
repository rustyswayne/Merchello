namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
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
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferSettings> GetAll(params Guid[] keys)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOfferSettings GetByOfferCode(string offerCode)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IOfferSettings> GetByProviderKey(Guid offerProviderKey, bool activeOnly = true)
        {
            throw new NotImplementedException();
        }
    }
}