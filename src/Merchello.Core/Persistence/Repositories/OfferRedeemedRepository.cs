namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class OfferRedeemedRepository : IOfferRedeemedRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferRedeemedRepository"/> class.
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        public OfferRedeemedRepository(IDatabaseUnitOfWork work, [Inject(Constants.Repository.DisabledCache)] ICacheHelper cache, ILogger logger, IMapperRegister mappers)
            : base(work, cache, logger, mappers)
        {
        }

        /// <inheritdoc/>
        public void UpdateForOfferSettingDelete(Guid offerSettingsKey)
        {
            Database.Execute(
                "UPDATE merchOfferRedeemed SET offerSettingsKey = NULL WHERE offerSettingsKey = @Key",
                new { @Key = offerSettingsKey });
        }

        /// <inheritdoc/>
        protected override OfferRedeemedFactory GetFactoryInstance()
        {
            return new OfferRedeemedFactory();
        }
    }
}