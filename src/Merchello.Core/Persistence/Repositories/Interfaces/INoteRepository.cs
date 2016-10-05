namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="INote"/> entities.
    /// </summary>
    public interface INoteRepository : INPocoEntityRepository<INote>, ISearchTermRepository<INote>
    {
    }
}