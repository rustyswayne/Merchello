namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Models.TypeFields;

    /// <summary>
    /// Represents a <see cref="TypeField"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(TypeField))]
    [MapperFor(typeof(ITypeField))]
    public class TypeFieldMapper : BaseMapper
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
            CacheMap<TypeField, TypeFieldDto>(src => src.TypeKey, dto => dto.Key);
            CacheMap<TypeField, TypeFieldDto>(src => src.Name, dto => dto.Name);
            CacheMap<TypeField, TypeFieldDto>(src => src.Alias, dto => dto.Alias);
        }
    }
}