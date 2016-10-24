namespace Merchello.Tests.Base
{
    using LightInject;

    using Merchello.Core.DI;
    using Merchello.Tests.Base.Boot;

    public abstract class UmbracoRuntimeTestBase : UmbracoPluginManagerTestBase
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