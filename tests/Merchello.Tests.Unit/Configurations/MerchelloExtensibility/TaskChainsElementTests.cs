namespace Merchello.Tests.Unit.Configurations.MerchelloExtensibility
{
    using System.Linq;

    using Merchello.Core;

    using NUnit.Framework;

    [TestFixture]
    public class TaskChainsElementTests : MerchelloExtensibilityTests
    {
        [Test]
        public void TaskChains()
        {
            var taskChains = ExtensibilitySection.TaskChains;

            Assert.NotNull(taskChains);
        }

        [Test]
        public void CheckoutManagerInvoiceCreate()
        {
            //// Arrange
            var taskChains = ExtensibilitySection.TaskChains;

            //// Act
            var types = taskChains[Constants.TaskChainAlias.CheckoutManagerInvoiceCreate];

            //// Assert
            Assert.NotNull(types);
            Assert.IsFalse(types.Any(x => x == null));
        }
    }
}