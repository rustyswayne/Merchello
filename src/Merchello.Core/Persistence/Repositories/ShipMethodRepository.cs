namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal class ShipMethodRepository : NPocoEntityRepositoryBase<IShipMethod, ShipMethodDto, ShipMethodFactory>, IShipMethodRepository
    {
        /// <summary>
        /// The <see cref="IShipmentRepository"/>.
        /// </summary>
        private readonly IShipmentRepository _shipmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipMethodRepository"/> class.
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
        /// <param name="shipmentRepository">
        /// The <see cref="IShipmentRepository"/>
        /// </param>
        public ShipMethodRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers, IShipmentRepository shipmentRepository)
            : base(work, cache, logger, mappers)
        {
            Ensure.ParameterNotNull(shipmentRepository, nameof(shipmentRepository));

            _shipmentRepository = shipmentRepository;
        }

        /// <inheritdoc/>
        public bool Exists(Guid providerKey, Guid shipCountryKey, string serviceCode)
        {
            var query = Query.Where(x => x.ProviderKey == providerKey && x.ShipCountryKey == shipCountryKey && x.ServiceCode == serviceCode);
            return Count(query) > 0;
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<ShipMethodDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchShipMethod.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchShipRateTier WHERE shipMethodKey = @Key",
                "DELETE FROM merchShipMethod WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        /// <param name="entity"></param>
        protected override void PersistDeletedItem(IShipMethod entity)
        {
            _shipmentRepository.UpdateForShipMethodDelete(entity.Key);
            base.PersistDeletedItem(entity);
        }

        /// <inheritdoc/>
        protected override ShipMethodFactory GetFactoryInstance()
        {
            return new ShipMethodFactory();
        }
    }
}