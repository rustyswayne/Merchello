namespace Merchello.Tests.Umbraco.Builders
{
    using Merchello.Core.Builders;
    using Merchello.Core.Chains;
    using Merchello.Core.Checkout;
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Tests.Base;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceBuilderChainTests : UmbracoRuntimeTestBase
    {
        protected override bool RequiresMerchelloConfig => true;

        private InvoiceBuilderChain _invoiceBuilder;

        public override void Initialize()
        {
            base.Initialize();

            var checkoutManager = Mock.Of<ICheckoutManagerBase>();
            var register = MC.Container.GetInstance<ICheckoutManagerBase, IAttemptChainTaskRegister<IInvoice>>(checkoutManager);
            _invoiceBuilder =
                (InvoiceBuilderChain)MC.Container
                    .GetInstance<IAttemptChainTaskRegister<IInvoice>, ICheckoutManagerBase, IBuilderChain<IInvoice>>(
                        register,
                        checkoutManager);
        }

        [Test]
        public void TaskCount()
        {
            //// Arrange
            var expected = 7;

            //// Act
            
            //// Assert
            Assert.AreEqual(expected, _invoiceBuilder.TaskCount);
        }
    }
}