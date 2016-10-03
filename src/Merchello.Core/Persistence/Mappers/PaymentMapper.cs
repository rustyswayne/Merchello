namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Payment"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Payment))]
    [MapperFor(typeof(IPayment))]
    internal sealed class PaymentMapper : BaseMapper
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

            CacheMap<Payment, PaymentDto>(src => src.Key, dto => dto.Key);                        
            CacheMap<Payment, PaymentDto>(src => src.CustomerKey, dto => dto.CustomerKey);
            CacheMap<Payment, PaymentDto>(src => src.PaymentMethodKey, dto => dto.PaymentMethodKey);
            CacheMap<Payment, PaymentDto>(src => src.PaymentTypeFieldKey, dto => dto.PaymentTfKey);                        
            CacheMap<Payment, PaymentDto>(src => src.PaymentMethodName, dto => dto.PaymentMethodName);
            CacheMap<Payment, PaymentDto>(src => src.ReferenceNumber, dto => dto.ReferenceNumber);
            CacheMap<Payment, PaymentDto>(src => src.Amount, dto => dto.Amount);
            CacheMap<Payment, PaymentDto>(src => src.Authorized, dto => dto.Authorized);
            CacheMap<Payment, PaymentDto>(src => src.Collected, dto => dto.Collected);
            CacheMap<Payment, PaymentDto>(src => src.Voided, dto => dto.Voided);
            CacheMap<Payment, PaymentDto>(src => src.Exported, dto => dto.Exported);
            CacheMap<Payment, PaymentDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Payment, PaymentDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
