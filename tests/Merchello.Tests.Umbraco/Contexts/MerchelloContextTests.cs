namespace Merchello.Tests.Umbraco.Contexts
{
    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class MerchelloContextTests : UmbracoRuntimeTestBase
    {
        public override void Initialize()
        {
            base.Initialize();

            // Ensure the database is deleted so we can test how this behaves on installs
            var schemaManager = MC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.UninstallDatabaseSchema();
        }
    }
}