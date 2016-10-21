namespace Merchello.Tests.Base
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Services;

    using LightInject;

    using Merchello.Core.Boot;
    using Merchello.Core.DI;
    using Merchello.Tests.Base.Boot;

    public abstract class UmbracoApplicationContextTestBase : UmbracoPluginManagerTestBase
    {

        public override void Initialize()
        {
            base.Initialize();

            // Boot Merchello
            var container = new ServiceContainer();
            container.EnableAnnotatedConstructorInjection();
            container.EnableAnnotatedPropertyInjection();
            var loader = new TestBoot(container, this.DatabaseContext, this.CacheHelper, this.ProfileLogger, this.PluginManager);
            loader.Boot();
        }


        public override void TearDown()
        {
            MC.Container.Dispose();
            base.TearDown();  

        }
    }
}