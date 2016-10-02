namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Invoice"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Invoice))]
    [MapperFor(typeof(IInvoice))]
    internal sealed class InvoiceMapper : BaseMapper
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

            CacheMap<Invoice, InvoiceDto>(src => src.Key, dto => dto.Key);
            CacheMap<Invoice, InvoiceDto>(src => src.CustomerKey, dto => dto.CustomerKey);
            CacheMap<Invoice, InvoiceDto>(src => src.InvoiceNumberPrefix, dto => dto.InvoiceNumberPrefix);
            CacheMap<Invoice, InvoiceDto>(src => src.InvoiceNumber, dto => dto.InvoiceNumber);
            CacheMap<Invoice, InvoiceDto>(src => src.PoNumber, dto => dto.PoNumber);
            CacheMap<Invoice, InvoiceDto>(src => src.InvoiceDate, dto => dto.InvoiceDate);
            CacheMap<Invoice, InvoiceDto>(src => src.InvoiceStatusKey, dto => dto.InvoiceStatusKey);
            CacheMap<Invoice, InvoiceDto>(src => src.VersionKey, dto => dto.VersionKey);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToName, dto => dto.BillToName);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToAddress1, dto => dto.BillToAddress1);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToAddress2, dto => dto.BillToAddress2);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToLocality, dto => dto.BillToLocality);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToRegion, dto => dto.BillToRegion);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToPostalCode, dto => dto.BillToPostalCode);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToCountryCode, dto => dto.BillToCountryCode);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToEmail, dto => dto.BillToEmail);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToPhone, dto => dto.BillToPhone);
            CacheMap<Invoice, InvoiceDto>(src => src.BillToCompany, dto => dto.BillToCompany);
            CacheMap<Invoice, InvoiceDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<Invoice, InvoiceDto>(src => src.Archived, dto => dto.Archived);
            CacheMap<Invoice, InvoiceDto>(src => src.CurrencyCode, dto => dto.CurrencyCode);
            CacheMap<Invoice, InvoiceDto>(src => src.Total, dto => dto.Total);
            CacheMap<Invoice, InvoiceDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Invoice, InvoiceDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}