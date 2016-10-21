namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.EntityCollections;
    using Merchello.Core.Persistence.Mappers;
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
