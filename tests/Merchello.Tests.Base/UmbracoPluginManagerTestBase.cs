namespace Merchello.Tests.Base
{
    using System;

    using global::Umbraco.Core.Plugins;

    using Merchello.Providers;

    //using global::Umbraco.Core.Plugins;

    public abstract class UmbracoPluginManagerTestBase : UmbracoDataContextTestBase
    {

        protected PluginManager PluginManager { get; private set; }

      //  protected Temp.PluginManager TPluginManager { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            Console.WriteLine($"Hack for provider resolution: {Providers.Constants.Braintree.TransactionChannel}");

            // Umbraco's Plugin Manager
            this.PluginManager = new PluginManager(this.CacheHelper.RuntimeCache, this.ProfileLogger, true);

           // this.TPluginManager = new Temp.PluginManager(this.CacheHelper.RuntimeCache, this.ProfileLogger, true);
        }

        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();

            
        }
    }
}