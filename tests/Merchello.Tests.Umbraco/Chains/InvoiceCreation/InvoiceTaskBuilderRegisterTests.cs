namespace Merchello.Tests.Umbraco.Chains.InvoiceCreation
{
    using System;
    using System.Linq;

    using Merchello.Core.Chains;
    using Merchello.Core.Chains.InvoiceCreation;
    using Merchello.Core.Checkout;
    using Merchello.Core.DI;
    using Merchello.Core.Models;
    using Merchello.Tests.Base;
    using Merchello.Web.Chains.InvoiceCreation;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class InvoiceTaskBuilderRegisterTests : UmbracoRuntimeTestBase
    {
        protected override bool RequiresMerchelloConfig => true;

        [Test]
        public void CanInstantiateInvoiceTaskBuilderRegister()
        {
            //// Arrange
            var expected = 7;
            var container = MC.Container;
            var checkoutManager = Mock.Of<ICheckoutManagerBase>();

            //// Act
            var register = container.GetInstance<ICheckoutManagerBase, IAttemptChainTaskRegister<IInvoice>>(checkoutManager);

            //// Assert
            Assert.NotNull(register);
            Assert.AreEqual(7, register.TaskCount);
        }

        [Test]
        public void TasksAreInOrder()
        {
           //// Arrange
            var expected = new[]
                               {
                                   typeof(AddBillingInfoToInvoiceTask),
                                   typeof(ConvertItemCacheItemsToInvoiceItemsTask),
                                   typeof(AddCouponDiscountsToInvoiceTask),
                                   typeof(ApplyTaxesToInvoiceTask),
                                   typeof(ValidateCommonCurrency),
                                   typeof(AddInvoiceNumberPrefixTask),
                                   typeof(AddNotesToInvoiceTask)
                               };

            var container = MC.Container;
            var checkoutManager = Mock.Of<ICheckoutManagerBase>();

            //// Act
            var register = container.GetInstance<ICheckoutManagerBase, IAttemptChainTaskRegister<IInvoice>>(checkoutManager);
            var taskChain = register.GetTaskChain().ToArray();

            //// Assert
            for (var i = 0; i < register.TaskCount; i++)
            {
                Console.WriteLine(taskChain[i].TaskType.FullName);
                Assert.That(expected[i], Is.EqualTo(taskChain[i].TaskType));
            }
        }
    }
}