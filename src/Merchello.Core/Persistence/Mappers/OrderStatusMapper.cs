namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="OrderStatus"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(OrderStatus))]
    [MapperFor(typeof(IOrderStatus))]
    internal sealed class OrderStatusMapper : BaseMapper
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

            CacheMap<OrderStatus, OrderStatusDto>(src => src.Key, dto => dto.Key);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.Name, dto => dto.Name);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.Alias, dto => dto.Alias);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.Reportable, dto => dto.Reportable);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.Active, dto => dto.Active);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.SortOrder, dto => dto.SortOrder);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<OrderStatus, OrderStatusDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}