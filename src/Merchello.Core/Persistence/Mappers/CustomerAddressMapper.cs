namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Merchello.Core.Models.CustomerAddress"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(CustomerAddress))]
    [MapperFor(typeof(ICustomerAddress))]
    internal sealed class CustomerAddressMapper : BaseMapper
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

            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Key, dto => dto.Key);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.CustomerKey, dto => dto.CustomerKey);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Label, dto => dto.Label);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.FullName, dto => dto.FullName);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Company, dto => dto.Company);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.AddressTypeFieldKey, dto => dto.AddressTfKey);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Address1, dto => dto.Address1);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Address2, dto => dto.Address2);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Locality, dto => dto.Locality);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Region, dto => dto.Region);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.PostalCode, dto => dto.PostalCode);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.CountryCode, dto => dto.CountryCode);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.Phone, dto => dto.Phone);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.IsDefault, dto => dto.IsDefault);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<CustomerAddress, CustomerAddressDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
