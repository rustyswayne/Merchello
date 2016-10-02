namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="InvoiceLineItem"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(InvoiceLineItem))]
    [MapperFor(typeof(IInvoiceLineItem))]
    internal sealed class InvoiceLineItemMapper : BaseMapper
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

            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Key, dto => dto.Key);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.ContainerKey, dto => dto.ContainerKey);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.LineItemTfKey, dto => dto.LineItemTfKey);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Sku, dto => dto.Sku);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Name, dto => dto.Name);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Quantity, dto => dto.Quantity);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Price, dto => dto.Price);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<InvoiceLineItem, InvoiceItemDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
