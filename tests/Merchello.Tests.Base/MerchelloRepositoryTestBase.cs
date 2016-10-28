namespace Merchello.Tests.Base
{
    using Merchello.Core.DI;
    using Merchello.Core.Persistence.UnitOfWork;

    public abstract class MerchelloRepositoryTestBase : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;

        protected IDatabaseUnitOfWorkProvider UowProvider;

        public override void Initialize()
        {
            base.Initialize();
            UowProvider = MC.Container.GetInstance<IDatabaseUnitOfWorkProvider>();
        }
    }
}