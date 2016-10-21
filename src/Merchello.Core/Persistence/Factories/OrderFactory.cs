namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.DI;
    using Merchello.Core.Persistence.Repositories;

    using Models;
    using Models.Rdbms;

    /// <summary>
    /// The order factory.
    /// </summary>
    internal class OrderFactory : IEntityFactory<IOrder, OrderDto>
    {
        /// <summary>
        /// The <see cref="OrderStatusFactory"/>.
        /// </summary>
        private readonly OrderStatusFactory _orderStatusFactory;

        /// <summary>
        /// A function to query order line items.
        /// </summary>
        private readonly Func<Guid, string, LineItemCollection> _lineItemGetter;

        /// <summary>
        /// The currency code.
        /// </summary>
        private readonly string _currencyCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        /// <param name="orderStatusFactory">
        /// The <see cref="OrderStatusFactory"/>.
        /// </param>
        /// <param name="lineItemGetter">
        /// A function to query order line items.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code
        /// </param>
        public OrderFactory(OrderStatusFactory orderStatusFactory, Func<Guid, string, LineItemCollection> lineItemGetter, string currencyCode)
        {
            Ensure.ParameterNotNull(orderStatusFactory, nameof(orderStatusFactory));
            Ensure.ParameterNotNull(lineItemGetter, nameof(lineItemGetter));
            Ensure.ParameterNotNullOrEmpty(currencyCode, nameof(currencyCode));

            _currencyCode = currencyCode;
            _orderStatusFactory = orderStatusFactory;
            _lineItemGetter = lineItemGetter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        /// <remarks>
        /// Used for create and updates since we don't need the currency code
        /// </remarks>
        internal OrderFactory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        /// <param name="orderStatusFactory">
        /// The order status factory.
        /// </param>
        /// <param name="currencyCode">
        /// The currency code
        /// </param>
        internal OrderFactory(OrderStatusFactory orderStatusFactory, string currencyCode)
            : this(orderStatusFactory, MC.Container.GetInstance<IOrderLineItemRepository>().GetLineItemCollection, currencyCode)
        {
        }



        /// <summary>
        /// Builds an order entity.
        /// </summary>
        /// <param name="dto">
        /// The dto.
        /// </param>
        /// <returns>
        /// The <see cref="IOrder"/>.
        /// </returns>
        public IOrder BuildEntity(OrderDto dto)
        {
            var order = new Order(_orderStatusFactory.BuildEntity(dto.OrderStatusDto), dto.InvoiceKey)
                {
                    Key = dto.Key,
                    OrderNumberPrefix = dto.OrderNumberPrefix,
                    OrderNumber = dto.OrderNumber,
                    OrderDate = dto.OrderDate,
                    CurrencyCode = dto.CurrencyCode,
                    VersionKey = dto.VersionKey,
                    Exported = dto.Exported,
                    Items = _lineItemGetter.Invoke(dto.Key, _currencyCode),
                    UpdateDate = dto.UpdateDate,
                    CreateDate = dto.CreateDate
                };

            order.ResetDirtyProperties();

            return order;
        }

        /// <summary>
        /// Builds an order dto
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="OrderDto"/>.
        /// </returns>
        public OrderDto BuildDto(IOrder entity)
        {
            return new OrderDto()
                {
                    Key = entity.Key,
                    InvoiceKey = entity.InvoiceKey,
                    OrderNumberPrefix = entity.OrderNumberPrefix,
                    OrderNumber = entity.OrderNumber,
                    OrderStatusKey = entity.OrderStatusKey,
                    OrderDate = entity.OrderDate,
                    CurrencyCode = entity.CurrencyCode,
                    VersionKey = entity.VersionKey,
                    Exported = entity.Exported,
                    UpdateDate = entity.UpdateDate,
                    CreateDate = entity.CreateDate
                };
        }
    }
}