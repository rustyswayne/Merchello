namespace Merchello.Tests.Umbraco.Persistence.Repositories
{
    using Merchello.Core;
    using Merchello.Core.DI;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    [TestFixture]
    public class StoreSettingRepositoryTests : MerchelloRepositoryTestBase
    {
         protected override bool AutoInstall => true;

        [Test]
        public void GetByKey()
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                uow.ReadLock(Constants.Locks.Settings);
                var repo = uow.CreateRepository<IStoreSettingRepository>();
                var setting = repo.Get(Constants.StoreSetting.CurrencyCodeKey);
                uow.Complete();

                Assert.NotNull(setting);
            }

        }
    }
}