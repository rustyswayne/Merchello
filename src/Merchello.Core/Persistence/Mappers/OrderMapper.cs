namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Order"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Order))]
    [MapperFor(typeof(IOrder))]
    internal sealed class OrderMapper : BaseMapper
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

            CacheMap<Order, OrderDto>(src => src.Key, dto => dto.Key);
            CacheMap<Order, OrderDto>(src => src.InvoiceKey, dto => dto.InvoiceKey);
            CacheMap<Order, OrderDto>(src => src.OrderNumber, dto => dto.OrderNumber);
            CacheMap<Order, OrderDto>(src => src.OrderNumberPrefix, dto => dto.OrderNumberPrefix);
            CacheMap<Order, OrderDto>(src => src.OrderDate, dto => dto.OrderDate);
            CacheMap<Order, OrderDto>(src => src.OrderStatusKey, dto => dto.OrderStatusKey);
            CacheMap<Order, OrderDto>(src => src.VersionKey, dto => dto.VersionKey);
            CacheMap<Order, OrderDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<Order, OrderDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Order, OrderDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}