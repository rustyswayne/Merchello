namespace Merchello.Tests.Base.Boot
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Plugins;

    using Merchello.Umbraco.Boot;

    internal class TestBoot : UmbracoBoot
    {
        public TestBoot(ApplicationContext appContext, PluginManager pluginManager)
            : base(new BootSettings { IsForTesting = true }, appContext, pluginManager)
        {
        }
    }
}