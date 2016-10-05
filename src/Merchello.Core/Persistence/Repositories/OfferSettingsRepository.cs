namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
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

        /// <summary>
        /// Maps a collection of <see cref="OfferSettingsDto"/> to <see cref="IOfferSettings"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOfferSettings}"/>.
        /// </returns>
        protected IEnumerable<IOfferSettings> MapDtoCollection(IEnumerable<OfferSettingsDto> dtos)
        {
            var factory = new OfferSettingsFactory();
            return dtos.Select(factory.BuildEntity);
        }
    }
}