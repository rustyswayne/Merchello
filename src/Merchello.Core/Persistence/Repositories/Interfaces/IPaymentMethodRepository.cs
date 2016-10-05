namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IPaymentMethod"/> entities.
    /// </summary>
    public interface IPaymentMethodRepository : INPocoEntityRepository<IPaymentMethod>
    {
    }
}