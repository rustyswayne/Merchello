namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired;
    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class GatewayProviderSettingsRepository : IGatewayProviderSettingsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayProviderSettingsRepository"/> class.
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
        public GatewayProviderSettingsRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IGatewayProviderSettings> GetByShipCountryKey(Guid shipCountryKey)
        {
            var sql = Sql().SelectAll()
                        .From<GatewayProviderSettingsDto>()
                        .InnerJoin<ShipMethodDto>()
                        .On<GatewayProviderSettingsDto, ShipMethodDto>(left => left.Key, right => right.ProviderKey)
                        .Where<ShipMethodDto>(x => x.ShipCountryKey == shipCountryKey);


            var dtos = Database.Fetch<GatewayProviderSettingsDto>(sql);
            var factory = new GatewayProviderSettingsFactory();

            return dtos.DistinctBy(x => x.Key).Select(dto => factory.BuildEntity(dto));
        }

        /// <inheritdoc/>
        protected override GatewayProviderSettingsFactory GetFactoryInstance()
        {
            return new GatewayProviderSettingsFactory();
        }
    }
}
