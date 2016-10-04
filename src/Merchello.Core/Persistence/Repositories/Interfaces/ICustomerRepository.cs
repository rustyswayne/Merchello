namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="ICustomer"/> entities.
    /// </summary>
    public interface ICustomerRepository : INPocoEntityRepository<ICustomer>, IEntityCollectionEntityRepository<ICustomer>, ISearchTermRepository<ICustomer>
    {
    }
}