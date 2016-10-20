namespace Merchello.Tests.Umbraco.Contexts
{
    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class MerchelloContextTests : UmbracoApplicationContextTestBase
    {
        public override void Initialize()
        {
            base.Initialize();

            // Ensure the database is deleted so we can test how this behaves on installs
            var schemaManager = IoC.Container.GetInstance<IDatabaseSchemaManager>();
            schemaManager.UninstallDatabaseSchema();
        }

        [Test]
        public void Can_Be_Setup_As_NotConfigured()
        {
            Assert.That(MerchelloContext.Current.IsConfigured, Is.False);
        }
    }
}