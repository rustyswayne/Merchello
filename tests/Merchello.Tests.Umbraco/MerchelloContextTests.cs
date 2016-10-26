namespace Merchello.Tests.Umbraco
{
    using Merchello.Core;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class MerchelloContextTests : UmbracoRuntimeTestBase
    {
        [Test]
        public void Current()
        {
            Assert.That(MerchelloContext.Current, Is.Not.Null);
        }

        [Test]
        public void Services()
        {
            var services = MerchelloContext.Current.Services;
            Assert.That(services, Is.Not.Null);
            Assert.That(services.AuditLogService, Is.Not.Null);
            Assert.That(services.CustomerService, Is.Not.Null);
            Assert.That(services.EntityCollectionService, Is.Not.Null);
            Assert.That(services.MigrationStatusService, Is.Not.Null);
            Assert.That(services.StoreSettingService, Is.Not.Null);
        }

        [Test]
        public void Cache()
        {
            var cache = MerchelloContext.Current.Cache;
            Assert.That(cache, Is.Not.Null);
            Assert.That(cache.RequestCache, Is.Not.Null);
            Assert.That(cache.RuntimeCache, Is.Not.Null);
            Assert.That(cache.StaticCache, Is.Not.Null);
            Assert.That(cache.IsolatedRuntimeCache, Is.Not.Null);
        }
    }
}