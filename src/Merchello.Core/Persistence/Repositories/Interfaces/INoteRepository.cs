namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="INote"/> entities.
    /// </summary>
    public interface INoteRepository : INPocoEntityRepository<INote>, ISearchTermRepository<INote>
    {
        /// <summary>
        /// The get notes.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{INote}"/>.
        /// </returns>
        IEnumerable<INote> GetNotes(Guid entityKey);

        /// <summary>
        /// Gets notes associated with a specific entity.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key.
        /// </param>
        /// <param name="entityTfKey">
        /// The entity type field key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{INote}"/>.
        /// </returns>
        IEnumerable<INote> GetNotes(Guid entityKey, Guid entityTfKey);
    }
}