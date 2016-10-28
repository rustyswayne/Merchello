namespace Merchello.Tests.Umbraco.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Events;
    using Merchello.Core.Models;
    using Merchello.Core.Services;
    using Merchello.Tests.Base;

    using NUnit.Framework;

    public abstract class EventsServiceTestBase<TService, TEntity> : UmbracoRuntimeTestBase
    {
        protected override bool AutoInstall => true;

        protected static TEntity CreatingEntity, CreatedEntity;
        protected static IEnumerable<TEntity> SavingEntities, SavedEntities, DeletingEntities, DeletedEntities;

        protected TypedEventHandler<TService, NewEventArgs<TEntity>> CreatingHandler = (sender, args) =>
        { CreatingEntity = args.Entity; };

        protected TypedEventHandler<TService, NewEventArgs<TEntity>> CreatedHandler = (sender, args) =>
        { CreatedEntity = args.Entity; };

        protected TypedEventHandler<TService, SaveEventArgs<TEntity>> SavingHandler = (sender, args) =>
            { SavingEntities = args.SavedEntities; };

        protected TypedEventHandler<TService, SaveEventArgs<TEntity>> SavedHandler = (sender, args) =>
        { SavedEntities = args.SavedEntities; };

        protected TypedEventHandler<TService, DeleteEventArgs<TEntity>> DeletingHandler = (sender, args) =>
        { DeletingEntities = args.DeletedEntities; };

        protected TypedEventHandler<TService, DeleteEventArgs<TEntity>> DeletedHandler = (sender, args) =>
        { DeletedEntities = args.DeletedEntities; };



        [TearDown]
        public virtual void ClearEventData()
        {
            CreatingEntity = default(TEntity);
            CreatedEntity = default(TEntity);
            SavingEntities = null;
            SavedEntities = null;
            DeletingEntities = null;
            DeletedEntities = null;
        }
    }
}