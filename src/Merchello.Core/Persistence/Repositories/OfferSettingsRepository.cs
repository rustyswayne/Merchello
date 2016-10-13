namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class OfferSettingsRepository : IOfferSettingsRepository
    {
        /// <summary>
        /// The <see cref="IOfferRedeemedRepository"/>.
        /// </summary>
        private readonly IOfferRedeemedRepository _offerRedeemedRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferSettingsRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        /// <param name="offerRedeemedRepository">
        /// The <see cref="IOfferRedeemedRepository"/>
        /// </param>
        public OfferSettingsRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IOfferRedeemedRepository offerRedeemedRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(offerRedeemedRepository, nameof(offerRedeemedRepository));
            _offerRedeemedRepository = offerRedeemedRepository;
        }

        /// <inheritdoc/>
        protected override OfferSettingsFactory GetFactoryInstance()
        {
            return new OfferSettingsFactory();
        }
    }
}