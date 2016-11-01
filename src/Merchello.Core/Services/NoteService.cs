namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Events;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;
    using Merchello.Core.Persistence.Repositories;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    public class NoteService : RepositoryServiceBase<IDatabaseUnitOfWorkProvider>, INoteService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteService"/> class.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IDatabaseBulkUnitOfWorkProvider"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="eventMessagesFactory">
        /// The <see cref="IEventMessagesFactory"/>.
        /// </param>
        public NoteService(IDatabaseUnitOfWorkProvider provider, ILogger logger, IEventMessagesFactory eventMessagesFactory)
            : base(provider, logger, eventMessagesFactory)
        {
        }

        /// <summary>
        /// Occurs before create.
        /// </summary>
        public static event TypedEventHandler<INoteService, NewEventArgs<INote>> Creating;

        /// <summary>
        /// Occurs after create.
        /// </summary>
        public static event TypedEventHandler<INoteService, NewEventArgs<INote>> Created;

        /// <inheritdoc/>
        public INote CreateNote(Guid entityKey, EntityType entityType, string message)
        {
            var entityTfKey = EnumTypeFieldConverter.EntityType.GetTypeField(entityType).TypeKey;

            return CreateNote(entityKey, entityTfKey, message);
        }

        /// <inheritdoc/>
        public INote CreateNote(Guid entityKey, Guid entityTfKey, string message)
        {
            var note = new Note(entityKey, entityTfKey)
            {
                Message = message
            };

            if (Creating.IsRaisedEventCancelled(new Events.NewEventArgs<INote>(note), this))
            {
                note.WasCancelled = true;
                return note;
            }

            Created.RaiseEvent(new Events.NewEventArgs<INote>(note), this);

            return note;
        }

        /// <inheritdoc/>
        public IEnumerable<INote> GetNotesByEntityKey(Guid entityKey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<INoteRepository>();
                var notes = repo.GetByQuery(repo.Query.Where(x => x.EntityKey == entityKey));
                uow.Complete();
                return notes;
            }
        }

        /// <inheritdoc/>
        public PagedCollection<INote> GetNotesByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repo = uow.CreateRepository<INoteRepository>();
                var notes = repo.GetNotesByEntityTfKey(entityTfKey, page, itemsPerPage, sortBy, direction);
                uow.Complete();
                return notes;
            }
        }
    }
}