namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="OrderLineItem"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(OrderLineItem))]
    [MapperFor(typeof(IOrderLineItem))]
    internal sealed class OrderLineItemMapper : BaseMapper
    {
        /// <summary>
        /// The mapper specific instance of the the property info cache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, DtoMapModel> PropertyInfoCacheInstance = new ConcurrentDictionary<string, DtoMapModel>();

        /// <inheritdoc/>
        internal override ConcurrentDictionary<string, DtoMapModel> PropertyInfoCache => PropertyInfoCacheInstance;

        /// <inheritdoc/>
        protected override void BuildMap()
        {
            if (!PropertyInfoCache.IsEmpty) return;

            CacheMap<OrderLineItem, OrderItemDto>(src => src.Key, dto => dto.Key);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.ShipmentKey, dto => dto.ShipmentKey);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.ContainerKey, dto => dto.ContainerKey);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.LineItemTfKey, dto => dto.LineItemTfKey);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.Sku, dto => dto.Sku);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.Name, dto => dto.Name);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.Quantity, dto => dto.Quantity);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.Price, dto => dto.Price);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.BackOrder, dto => dto.BackOrder);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<OrderLineItem, OrderItemDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}