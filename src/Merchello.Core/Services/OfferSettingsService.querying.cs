namespace Merchello.Core.Services
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class OfferSettingsService : IOfferSettingsService
    {
        /// <inheritdoc/>
        public IEnumerable<IOfferSettings> GetAllActive(bool excludeExpired = true)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public bool OfferCodeIsUnique(string offerCode)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public PagedCollection<IOfferSettings> Search(string searchTerm, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending)
        {
            throw new System.NotImplementedException();
        }
    }
}
