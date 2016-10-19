namespace Merchello.Core
{
    using System;
    using System.Collections.Generic;

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
    }
}
