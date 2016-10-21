namespace Merchello.Tests.Base.Boot
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Cache;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Plugins;

    using LightInject;

    using Merchello.Umbraco.Boot;

    internal class TestBoot : UmbracoBoot
    {
        public TestBoot(IServiceContainer container, DatabaseContext databaseContext, CacheHelper applicationCache, ProfilingLogger profilingLogger, PluginManager pluginManager)
            : base(container, databaseContext, applicationCache, profilingLogger, pluginManager)
        {
        }
    }
}