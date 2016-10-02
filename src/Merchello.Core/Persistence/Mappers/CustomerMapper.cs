namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;


    /// <summary>
    /// Represents a <see cref="Merchello.Core.Models.Customer"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Customer))]
    [MapperFor(typeof(ICustomer))]
    internal sealed class CustomerMapper : BaseMapper
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

            CacheMap<Customer, CustomerDto>(src => src.Key, dto => dto.Key);
            CacheMap<Customer, CustomerDto>(src => src.FirstName, dto => dto.FirstName);
            CacheMap<Customer, CustomerDto>(src => src.LastName, dto => dto.LastName);
            CacheMap<Customer, CustomerDto>(src => src.Email, dto => dto.Email);
            CacheMap<Customer, CustomerDto>(src => src.LoginName, dto => dto.LoginName);
            CacheMap<Customer, CustomerDto>(src => src.TaxExempt, dto => dto.TaxExempt);
            CacheMap<Customer, CustomerDto>(src => src.LastActivityDate, dto => dto.LastActivityDate);
            CacheMap<Customer, CustomerDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Customer, CustomerDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}
