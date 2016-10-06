namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ShipCountryRepository : IShipCountryRepository
    {
        /// <summary>
        /// The <see cref="IShipmentRepository"/>.
        /// </summary>
        private readonly IShipmentRepository _shipmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipCountryRepository"/> class.
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
        /// <param name="shipmentRepository">
        /// The <see cref="IShipmentRepository"/>
        /// </param>
        public ShipCountryRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IShipmentRepository shipmentRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(shipmentRepository, nameof(shipmentRepository));
            _shipmentRepository = shipmentRepository;
        }

        /// <inheritdoc/>
        public bool Exists(Guid catalogKey, string countryCode)
        {
            var sql = Sql().SelectCount()
                .From<ShipCountryDto>()
                .Where<ShipCountryDto>(x => x.CatalogKey == catalogKey && x.CountryCode == countryCode);

            return Database.ExecuteScalar<int>(sql) > 0;
        }
    }
}