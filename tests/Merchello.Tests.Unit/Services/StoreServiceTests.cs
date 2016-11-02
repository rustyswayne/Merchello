namespace Merchello.Tests.Unit.Services
{
    using Merchello.Core.Services;

    using NUnit.Framework;

    [TestFixture]
    public class StoreServiceTests
    {
        private string[] aliases = new[] { "dup", "dup1", "test", "test1", "test2", "test21", "one" };

        [TestCase("dup", "dup2")]
        [TestCase("dup1", "dup11")]
        [TestCase("test", "test3")]
        [TestCase("test1", "test11")]
        [TestCase("test2", "test22")]
        [TestCase("one", "one1")]
        [TestCase("fine", "fine")]
        [TestCase("SOmeThing-Else", "somethingelse")]
        [TestCase("Chars()&^%$#@", "chars")]
        [Test]
        public void EnsureUniqueStoreAlias(string alias, string expected)
        {
            var check = StoreService.EnsureUniqueStoreAlias(alias, aliases);
            Assert.That(check, Is.EqualTo(expected));
        }
    }
}