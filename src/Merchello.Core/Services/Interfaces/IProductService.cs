namespace Merchello.Core.Services
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a service that is responsible for operations related to <see cref="IProduct"/>.
    /// </summary>
    public interface IProductService : IEntityCollectionEntityService<IProduct>, IGetAllService<IProduct>, IProductVariantService
    {    
    }
}