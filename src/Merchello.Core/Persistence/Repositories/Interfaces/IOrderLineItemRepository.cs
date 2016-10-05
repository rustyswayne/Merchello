namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOrderLineItem"/> entities.
    /// </summary>
    public interface IOrderLineItemRepository : INPocoEntityRepository<IOrderLineItem>, ILineItemRepository<IOrderLineItem>
    {
    }
}