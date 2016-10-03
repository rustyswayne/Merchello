namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="PaymentMethod"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(PaymentMethod))]
    [MapperFor(typeof(IPaymentMethod))]
    internal sealed class PaymentMethodMapper : BaseMapper
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

            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.Key, dto => dto.Key);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.ProviderKey, dto => dto.ProviderKey);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.Name, dto => dto.Name);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.Description, dto => dto.Description);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.PaymentCode, dto => dto.PaymentCode);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<PaymentMethod, PaymentMethodDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}