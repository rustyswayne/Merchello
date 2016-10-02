namespace Merchello.Core.Persistence.Mappers
{
    using System.Collections.Concurrent;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <summary>
    /// Represents a <see cref="Note"/> to DTO mapper used to translate the property
    /// implementation to that of the database's DTO as sql: [tableName].[columnName].
    /// </summary>
    [MapperFor(typeof(Note))]
    [MapperFor(typeof(INote))]
    internal sealed class NoteMapper : BaseMapper
    {
        /// <summary>
        /// The mapper specific instance of the the property info cache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, DtoMapModel> PropertyInfoCacheInstance = new ConcurrentDictionary<string, DtoMapModel>();

        /// <inheritdoc/>
        internal override ConcurrentDictionary<string, DtoMapModel> PropertyInfoCache => PropertyInfoCacheInstance;

        /// <inheritdoc/>
        protected override void BuildMap()
        {
            if (!PropertyInfoCache.IsEmpty) return;

            CacheMap<Note, NoteDto>(src => src.Key, dto => dto.Key);
            CacheMap<Note, NoteDto>(src => src.EntityKey, dto => dto.EntityKey);
            CacheMap<Note, NoteDto>(src => src.EntityTfKey, dto => dto.EntityTfKey);
            CacheMap<Note, NoteDto>(src => src.Author, dto => dto.Author);
            CacheMap<Note, NoteDto>(src => src.InternalOnly, dto => dto.InternalOnly);
            CacheMap<Note, NoteDto>(src => src.Message, dto => dto.Message);
            CacheMap<Note, NoteDto>(src => src.CreateDate, dto => dto.CreateDate);
            CacheMap<Note, NoteDto>(src => src.UpdateDate, dto => dto.UpdateDate);
        }
    }
}