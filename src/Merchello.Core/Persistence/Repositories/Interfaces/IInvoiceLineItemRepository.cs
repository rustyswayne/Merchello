namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;


    /// <summary>
    /// Represents a repository responsible for <see cref="IInvoiceLineItem"/> entities.
    /// </summary>
    public interface IInvoiceLineItemRepository : INPocoEntityRepository<IInvoiceLineItem>, ILineItemRepository<IInvoiceLineItem>
    {
    }
}