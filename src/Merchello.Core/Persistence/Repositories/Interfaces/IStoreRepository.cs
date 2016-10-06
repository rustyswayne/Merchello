namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IStore"/> entities.
    /// </summary>
    public interface IStoreRepository : INPocoEntityRepository<IStore>
    {
    }
}