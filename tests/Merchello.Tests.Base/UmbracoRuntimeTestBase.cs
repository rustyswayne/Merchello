namespace Merchello.Tests.Base
{
    using LightInject;

    using Merchello.Core.Boot;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base.Boot;

    using NPoco;

    public abstract class UmbracoRuntimeTestBase : UmbracoPluginManagerTestBase
    {
        protected Database Database;

        protected virtual bool UninistallDatabaseOnTearDown => true;


        public override void Initialize()
        {
            base.Initialize();


            // Boot Merchello
            var container = new ServiceContainer();
            container.EnableAnnotatedConstructorInjection();
            container.EnableAnnotatedPropertyInjection();

            var bootSettings = new BootSettings { AutoInstall = AutoInstall, IsTest = true, RequiresConfig = BootManagerLoadConfig };

            var loader = new TestBoot(container, bootSettings, this.DatabaseContext, this.CacheHelper, this.ProfileLogger, this.PluginManager)
                {
                    // set the configuration file name.
                    MerchelloSettingsPath = MerchelloSettingsFileName
                };
            
            loader.Boot();

            // we need to manually refresh the configuration after the installation.
            if (AutoInstall) RefreshConfiguration();

            this.Database = MC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
        }


        public override void OneTimeTearDown()
        {
            if (UninistallDatabaseOnTearDown)
            {
                var manager = MC.Container.GetInstance<IDatabaseSchemaManager>();
                manager.UninstallDatabaseSchema();
            }

            MC.Container.Dispose();
            base.OneTimeTearDown();  
        }
    }
}