namespace Merchello.Tests.Base
{
    using Merchello.Core.DI;
    using Merchello.Core.Persistence;

    using NPoco;

    public abstract class MerchelloDatabaseTestBase : UmbracoApplicationContextTestBase
    {
        protected Database Database;

        public override void Initialize()
        {
            base.Initialize();

            var schemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.UninstallDatabaseSchema();
            schemaManager.InstallDatabaseSchema();

            this.Database = MC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
        }

        public override void TearDown()
        {
            base.TearDown();
        }
    }
}