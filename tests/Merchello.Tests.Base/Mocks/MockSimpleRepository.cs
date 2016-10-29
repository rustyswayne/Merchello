namespace Merchello.Tests.Base.Mocks
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Persistence.Querying;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    internal class MockSimpleRepository<TEntity> : RepositoryWritableBase<TEntity>
        where TEntity : Entity, new()
    {
        public MockSimpleRepository(IUnitOfWork work, ICacheHelper cache, ILogger logger)
            : base(work, cache, logger)
        {
        }

        protected override TEntity PerformGet(Guid key)
        {
            return new TEntity();
        }

        protected override IEnumerable<TEntity> PerformGetAll(params Guid[] keys)
        {
            for (var i = 0; i < 10; i++) yield return new TEntity();
        }

        protected override bool PerformExists(Guid key)
        {
            return true;
        }

        public override IQuery<TEntity> Query { get; }

        public override IQueryFactory QueryFactory { get; }

        protected override IEnumerable<TEntity> PerformGetByQuery(IQuery<TEntity> query)
        {
            throw new NotImplementedException();
        }

        protected override int PerformCount(IQuery<TEntity> query)
        {
            return 10;
        }

        protected override void PersistNewItem(TEntity entity)
        {
            ((Entity)entity).AddingEntity();
            this.AddOrUpdate(entity);
            entity.ResetDirtyProperties();
        }

        protected override void PersistUpdatedItem(TEntity entity)
        {
            ((Entity)entity).UpdatingEntity();
            this.AddOrUpdate(entity);
            entity.ResetDirtyProperties();
        }

        protected override void PersistDeletedItem(TEntity entity)
        {
            entity.Key = Guid.Empty;
        }
    }
}