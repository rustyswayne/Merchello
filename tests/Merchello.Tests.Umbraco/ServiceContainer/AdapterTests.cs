﻿namespace Merchello.Tests.Umbraco.ServiceContainer
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
            Assert.NotNull(IoC.Container.GetInstance<global::Umbraco.Core.DatabaseContext>());
        }

        [Test]
        public void UmbracoLogger()
        {
            Assert.NotNull(IoC.Container.GetInstance<global::Umbraco.Core.Logging.ILogger>());
        }

        [Test]
        public void UmbracoDatabase()
        {
            Assert.NotNull(IoC.Container.GetInstance<global::Umbraco.Core.Persistence.UmbracoDatabase>());
        }

        [Test]
        public void DatabaseSchemaHelper()
        {
            Assert.NotNull(IoC.Container.GetInstance<global::Umbraco.Core.Persistence.DatabaseSchemaHelper>());
        }

        [Test]
        public void IPluginManager()
        {
            Assert.NotNull(IoC.Container.GetInstance<IPluginManager>());
        }

        [Test]
        public void ISqlSyntaxProvider()
        {
            Assert.NotNull(IoC.Container.GetInstance<ISqlSyntaxProviderAdapter>());
        }

        [Test]
        public void ICacheHelper()
        {
            Assert.NotNull(IoC.Container.GetInstance<ICacheHelper>());
        }

        public void ILogger()
        {
            Assert.NotNull(IoC.Container.GetInstance<ILogger>());
        }
    }
}