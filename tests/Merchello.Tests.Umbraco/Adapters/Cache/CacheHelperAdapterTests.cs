namespace Merchello.Tests.Umbraco.Adapters.Cache
{
    using Merchello.Tests.Base;
    using Merchello.Umbraco.Adapters;

    using NUnit.Framework;

    [TestFixture]
    public class CacheHelperAdapterTests : UmbracoRuntimeTestBase
    {
        protected override bool EnableCache { get { return false; } }

        [Test]
        public void CacheHelperAdapter()
        {
            var adapted = new CacheHelperAdapter(this.CacheHelper);

            Assert.NotNull(adapted.RuntimeCache);
            Assert.NotNull(adapted.StaticCache);
            Assert.NotNull(adapted.RequestCache);
            Assert.NotNull(adapted.IsolatedRuntimeCache);
        }
    }
}