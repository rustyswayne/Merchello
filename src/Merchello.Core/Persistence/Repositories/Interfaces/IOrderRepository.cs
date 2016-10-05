namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOrder"/> entities.
    /// </summary>
    public interface IOrderRepository : INPocoEntityRepository<IOrder>, IEnsureDocumentNumberRepository
    {
    }
}