namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="INoteService"/>.
    /// </summary>
    public interface INoteService : IService
    {
        /// <summary>
        /// Creates a note.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityType">
        /// The entity type.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="INote"/>.
        /// </returns>
        INote CreateNote(Guid entityKey, EntityType entityType, string message);

        /// <summary>
        /// Creates a note without saving it to the database.
        /// </summary>
        /// <param name="entityKey">
        /// The entity Key.
        /// </param>
        /// <param name="entityTfKey">
        /// The entity Type field Key.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="INote"/>.
        /// </returns>
        INote CreateNote(Guid entityKey, Guid entityTfKey, string message);


        /// <summary>
        /// Gets a collection of <see cref="INote"/> for a particular entity
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{INote}"/>.
        /// </returns>
        IEnumerable<INote> GetNotesByEntityKey(Guid entityKey);

        /// <summary>
        /// Gets a collection of <see cref="INote"/> for an entity type
        /// </summary>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{INote}"/>.
        /// </returns>
        PagedCollection<INote> GetNotesByEntityTfKey(Guid entityTfKey, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);
    }
}