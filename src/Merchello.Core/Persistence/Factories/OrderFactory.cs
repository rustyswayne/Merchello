﻿namespace Merchello.Core.Persistence.Factories
{
    using System;

    using Merchello.Core.DependencyInjection;
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
        private readonly Func<Guid, LineItemCollection> _lineItemGetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        /// <param name="orderStatusFactory">
        /// The <see cref="OrderStatusFactory"/>.
        /// </param>
        /// <param name="lineItemGetter">
        /// A function to query order line items.
        /// </param>
        public OrderFactory(OrderStatusFactory orderStatusFactory, Func<Guid, LineItemCollection> lineItemGetter)
        {
            Ensure.ParameterNotNull(orderStatusFactory, nameof(orderStatusFactory));
            Ensure.ParameterNotNull(lineItemGetter, nameof(lineItemGetter));

            _orderStatusFactory = orderStatusFactory;
            _lineItemGetter = lineItemGetter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        internal OrderFactory()
            : this(new OrderStatusFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderFactory"/> class.
        /// </summary>
        /// <param name="orderStatusFactory">
        /// The order status factory.
        /// </param>
        internal OrderFactory(OrderStatusFactory orderStatusFactory)
            : this(orderStatusFactory, IoC.Container.GetInstance<IOrderLineItemRepository>().GetLineItemCollection)
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
                    VersionKey = dto.VersionKey,
                    Exported = dto.Exported,
                    Items = _lineItemGetter.Invoke(dto.Key),
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
                    VersionKey = entity.VersionKey,
                    Exported = entity.Exported,
                    UpdateDate = entity.UpdateDate,
                    CreateDate = entity.CreateDate
                };
        }
    }
}