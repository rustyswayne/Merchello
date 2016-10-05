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
    internal partial class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// The <see cref="IOrderLineItemRepository"/>.
        /// </summary>
        private IOrderLineItemRepository _orderLineItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
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
        public OrderRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver, IOrderLineItemRepository orderLineItemRepository)
            : base(work, cache, logger, mappingResolver)
        {
            Ensure.ParameterNotNull(orderLineItemRepository, nameof(orderLineItemRepository));
            _orderLineItemRepository = orderLineItemRepository;
        }

        /// <summary>
        /// Maps a collection of <see cref="OrderDto"/> to <see cref="IOrder"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IOrder}"/>.
        /// </returns>
        protected IEnumerable<IOrder> MapDtoCollection(IEnumerable<OrderDto> dtos)
        {
            return dtos.Select(dto => Get(dto.Key));
        }
    }
}