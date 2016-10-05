namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
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
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public OfferRedeemedRepository(IDatabaseUnitOfWork work, [Inject(Constants.Repository.DisabledCache)] ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public void UpdateForOfferSettingDelete(Guid offerSettingsKey)
        {
            Database.Execute(
                "UPDATE merchOfferRedeemed SET offerSettingsKey = NULL WHERE offerSettingsKey = @Key",
                new { @Key = offerSettingsKey });
        }
    }
}