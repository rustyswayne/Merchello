namespace Merchello.Tests.Base
{
    using System;

    using Merchello.Core.DI;
    using Merchello.Core.Persistence;

    using NPoco;

    [Obsolete("Use settings override")]
    public abstract class MerchelloDatabaseTestBase : UmbracoRuntimeTestBase
    {
        protected Database Database;

        protected override bool AutoInstall => true;


        public override void Initialize()
        {
            base.Initialize();

            var schemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.UninstallDatabaseSchema();
            schemaManager.InstallDatabaseSchema();

            this.Database = MC.Container.GetInstance<IDatabaseFactory>().GetDatabase().Database;
        }

        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
        }
    }
}