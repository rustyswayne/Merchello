namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IInvoice"/>.
    /// </summary>
    public interface IInvoiceService : IEntityCollectionEntityService<IInvoice>, IService<IInvoice>
    {
         
    }
}