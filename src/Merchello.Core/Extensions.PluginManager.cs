namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.EntityCollections;
    using Merchello.Core.Gateways;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.Migrations;
    using Merchello.Core.Plugins;

    /// <summary>
    /// Extensions for <see cref="IPluginManager"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Resolves the <see cref="BaseMapper"/> types for mapping entities to DTO classes by attribute.
        /// </summary>
        /// <param name="pluginManager">
        /// The implementation of <see cref="IPluginManager"/>.
        /// </param>
        /// <returns>
        /// The resolved types.
        /// </returns>
        internal static IEnumerable<Type> ResolveBaseMappers(this IPluginManager pluginManager)
        {
            return pluginManager.ResolveTypesWithAttribute<BaseMapper, MapperForAttribute>();
        }

        /// <summary>
        /// Resolves the <see cref="IGatewayProvider"/> types for use in the <see cref="IGatewayContext"/>
        /// </summary>
        /// <param name="pluginManager">
        /// The plugin Manager.
        /// </param>
        /// <returns>
        /// The collection of gateway providers resolved
        /// </returns>
        internal static IEnumerable<Type> ResolveGatewayProviders(this IPluginManager pluginManager)
        {
            return pluginManager.ResolveTypesWithAttribute<IGatewayProvider, GatewayProviderActivationAttribute>();
        }

        /// <summary>
        /// Resolves the <see cref="IMerchelloMigration"/> types for Merchello upgrades.
        /// </summary>
        /// <param name="pluginManager">
        /// The plugin manager.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Type}"/>.
        /// </returns>
        internal static IEnumerable<Type> ResolveMigrations(this IPluginManager pluginManager)
        {
            return pluginManager.ResolveTypesWithAttribute<IMerchelloMigration, MigrationAttribute>();
        }

        /// <summary>
        /// Resolves the <see cref="IEntityCollectionProvider"/> type for mapping entity collection providers.
        /// </summary>
        /// <param name="pluginManager">
        /// The plugin manager.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Type}"/>.
        /// </returns>
        internal static IEnumerable<Type> ResolveEnityCollectionProviders(this IPluginManager pluginManager)
        {
            return pluginManager.ResolveTypesWithAttribute<IEntityCollectionProvider, EntityCollectionProviderAttribute>();
        }
    }
}
