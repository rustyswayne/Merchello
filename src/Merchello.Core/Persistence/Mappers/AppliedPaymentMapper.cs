namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Merchello.Core.Models.AppliedPayment"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(AppliedPayment))]
    [MapperFor(typeof(IAppliedPayment))]
    internal sealed class AppliedPaymentMapper : BaseMapper
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

            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.Key, dto => dto.Key);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.PaymentKey, dto => dto.PaymentKey);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.InvoiceKey, dto => dto.InvoiceKey);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.AppliedPaymentTfKey, dto => dto.AppliedPaymentTfKey);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.Description, dto => dto.Description);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.Amount, dto => dto.Amount);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<AppliedPayment, AppliedPaymentDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
