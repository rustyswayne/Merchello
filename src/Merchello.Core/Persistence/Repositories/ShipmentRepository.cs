namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class ShipmentRepository : IShipmentRepository
    {
        /// <summary>
        /// The <see cref="IOrderLineItemRepository"/>.
        /// </summary>
        private readonly IOrderLineItemRepository _orderLineItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentRepository"/> class.
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
        /// <param name="orderLineItemRepository">
        /// The <see cref="IOrderLineItemRepository"/>
        /// </param>
        public ShipmentRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IOrderLineItemRepository orderLineItemRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(orderLineItemRepository, nameof(orderLineItemRepository));
            _orderLineItemRepository = orderLineItemRepository;
        }

        /// <inheritdoc/>
        public void UpdateForShipMethodDelete(Guid shipMethodKey)
        {
            var sql = Sql().Append(
                        "UPDATE merchShipment SET shipMethodKey = NULL WHERE shipMethodKey IN (SELECT pk FROM merchShipMethod WHERE shipCountryKey = @Key)",
                        new { @Key = shipMethodKey });

            Database.Execute(sql);

            CachePolicy.ClearAll();
        }

        /// <inheritdoc/>
        protected override ShipmentFactory GetFactoryInstance()
        {
            return new ShipmentFactory();
        }
    }
}