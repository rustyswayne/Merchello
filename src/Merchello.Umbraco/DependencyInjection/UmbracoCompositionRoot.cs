﻿namespace Merchello.Umbraco.DependencyInjection
{
    using LightInject;

    using Merchello.Core.Cache;
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence;
    using Merchello.Core.Persistence.SqlSyntax;
    using Merchello.Core.Plugins;
    using Merchello.Umbraco.Adapters;
    using Merchello.Umbraco.Mapping;

    using global::Umbraco.Core;

    using global::Umbraco.Core.Cache;

    using Merchello.Umbraco.Adapters.Logging;
    using Merchello.Umbraco.Adapters.Persistence;

    /// <summary>
    /// Adds Umbraco native class mappings to the container
    /// </summary>
    public class UmbracoCompositionRoot : ICompositionRoot
    {
        /// <inheritdoc/>
        public void Compose(IServiceRegistry container)
        {
            // AutoMapper mappings used in adapters
            container.Register<UmbracoAdapterAutoMapperMappings>();

            container.Register<global::Umbraco.Core.Persistence.UmbracoDatabase>(factory => factory.GetInstance<DatabaseContext>().Database);
            container.Register<global::Umbraco.Core.Persistence.DatabaseSchemaHelper>();

            // Adapters
            container.Register<IDatabaseAdapter, UmbracoDatabaseAdapter>();
            container.Register<IDatabaseSchemaManager, DatabaseSchemaHelperAdapter>();
            container.RegisterSingleton<IPluginManager, PluginManagerAdapter>();
            container.RegisterSingleton<ISqlSyntaxProviderAdapter, SqlSyntaxProviderAdapter>();
            container.RegisterSingleton<ICacheHelper, CacheHelperAdapter>();
            container.RegisterSingleton<IProfilingLogger, ProfilingLoggerAdapter>();
            container.RegisterSingleton<ILogger, LoggerAdapter>();

            container.RegisterSingleton<ICacheHelper>(factory => new CacheHelperAdapter(CacheHelper.CreateDisabledCacheHelper()), Core.Constants.Repository.DisabledCache);
        }
    }
}