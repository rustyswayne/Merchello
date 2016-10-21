namespace Merchello.Tests.Umbraco.ServiceContainer
{
    using Merchello.Core.Cache;
    using Merchello.Core.DI;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.SqlSyntax;
    using Merchello.Core.Plugins;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class AdapterTests : UmbracoApplicationContextTestBase
    {
        [Test]
        public void UmbracoDatabaseContext()
        {
            Assert.NotNull(MC.Container.GetInstance<global::Umbraco.Core.DatabaseContext>());
        }

        [Test]
        public void UmbracoLogger()
        {
            Assert.NotNull(MC.Container.GetInstance<global::Umbraco.Core.Logging.ILogger>());
        }

        [Test]
        public void UmbracoDatabase()
        {
            Assert.NotNull(MC.Container.GetInstance<global::Umbraco.Core.Persistence.UmbracoDatabase>());
        }

        [Test]
        public void DatabaseSchemaHelper()
        {
            Assert.NotNull(MC.Container.GetInstance<global::Umbraco.Core.Persistence.DatabaseSchemaHelper>());
        }

        [Test]
        public void IPluginManager()
        {
            Assert.NotNull(MC.Container.GetInstance<IPluginManager>());
        }

        [Test]
        public void ISqlSyntaxProvider()
        {
            Assert.NotNull(MC.Container.GetInstance<ISqlSyntaxProviderAdapter>());
        }

        [Test]
        public void ICacheHelper()
        {
            Assert.NotNull(MC.Container.GetInstance<ICacheHelper>());
        }

        public void ILogger()
        {
            Assert.NotNull(MC.Container.GetInstance<ILogger>());
        }
    }
}