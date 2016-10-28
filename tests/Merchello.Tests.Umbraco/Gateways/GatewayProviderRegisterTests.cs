namespace Merchello.Tests.Umbraco.Gateways
{
    using Merchello.Core.DI;
    using Merchello.Core.Gateways;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class GatewayProviderRegisterTests : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;
        

        protected IGatewayProviderRegister GatewayProviderRegister;

        public override void Initialize()
        {
            base.Initialize();

            GatewayProviderRegister = MC.Container.GetInstance<IGatewayProviderRegister>();
            Assert.NotNull(GatewayProviderRegister);
        }

        public void GetAllGatewayProviders()
        {
            var expected = 4;

            var providers = GatewayProviderRegister.GetAllProviders();


        }

    }
}