namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IOfferSettings"/> entities.
    /// </summary>
    public interface IOfferSettingsRepository : INPocoEntityRepository<IOfferSettings>, ISearchTermRepository<IOfferSettings>
    {
    }
}