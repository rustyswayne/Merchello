namespace Merchello.Tests.Base
{
    using global::Umbraco.Core.Plugins;

    public abstract class UmbracoPluginManagerTestBase : UmbracoDataContextTestBase
    {

        protected PluginManager PluginManager { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            // Umbraco's Plugin Manager
            this.PluginManager = new PluginManager(this.CacheHelper.RuntimeCache, this.ProfileLogger, true);
        }

        public override void TearDown()
        {
            base.TearDown();

            
        }
    }
}