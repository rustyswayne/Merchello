namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IItemCacheLineItem"/> entities.
    /// </summary>
    public interface IItemCacheLineItemRepository : INPocoEntityRepository<IItemCacheLineItem>, ILineItemRepository<IItemCacheLineItem>
    {
    }
}