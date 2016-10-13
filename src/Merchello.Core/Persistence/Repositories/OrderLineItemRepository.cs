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
    internal partial class OrderLineItemRepository : IOrderLineItemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLineItemRepository"/> class.
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
        public OrderLineItemRepository(IDatabaseUnitOfWork work, [Inject(Constants.Repository.DisabledCache)] ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc />
        public void UpdateForShipmentDelete(Guid shipmentKey)
        {
            Database.Execute(
                "UPDATE merchOrderItem SET shipmentKey = NULL WHERE shipmentKey = @Key",
                new { @Key = shipmentKey });

            //// Not really needed since this is a NullCacheRepository - but added in case we change it to
            //// an isolated cache.
            CachePolicy.ClearAll();
        }

        /// <inheritdoc/>
        protected override OrderLineItemFactory GetFactoryInstance()
        {
            return new OrderLineItemFactory();
        }
    }
}