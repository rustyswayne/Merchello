namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="InvoiceStatus"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(InvoiceStatus))]
    [MapperFor(typeof(IInvoiceStatus))]
    internal sealed class InvoiceStatusMapper : BaseMapper
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

            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.Key, dto => dto.Key);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.Name, dto => dto.Name);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.Alias, dto => dto.Alias);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.Reportable, dto => dto.Reportable);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.Active, dto => dto.Active);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.SortOrder, dto => dto.SortOrder);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<InvoiceStatus, InvoiceStatusDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
