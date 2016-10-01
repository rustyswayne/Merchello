namespace Merchello.Tests.Umbraco.TestHelpers.Base
{
    using Merchello.Core.DependencyInjection;
    using Merchello.Core.Persistence;

    using NPoco;

    public abstract class MerchelloDatabaseTestBase : UmbracoApplicationContextTestBase
    {
        protected Database Database;

        public override void Initialize()
        {
            base.Initialize();

            var schemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.InstallDatabaseSchema();

            Database = IoC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
        }

        public override void TearDown()
        {
            var schemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.UninstallDatabaseSchema();
            base.TearDown();
        }
    }
}