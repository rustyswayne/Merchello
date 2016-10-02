namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="GatewayProviderSettings"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(GatewayProviderSettings))]
    [MapperFor(typeof(IGatewayProviderSettings))]
    internal sealed class GatewayProviderSettingsMapper : BaseMapper
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

            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.Key, dto => dto.Key);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.ProviderTfKey, dto => dto.ProviderTfKey);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.Name, dto => dto.Name);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.Description, dto => dto.Description);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.EncryptExtendedData, dto => dto.EncryptExtendedData);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.UpdateDate, dto => dto.UpdateDate);
            CacheMap<GatewayProviderSettings, GatewayProviderSettingsDto>(src => src.CreateDate, dto => dto.CreateDate);
        }
    }
}