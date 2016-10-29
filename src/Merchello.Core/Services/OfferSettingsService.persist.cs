namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class OfferSettingsService : IOfferSettingsService
    {
        /// <inheritdoc/>
        public IOfferSettings Create(string name, string offerCode, Guid offerProviderKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOfferSettings Create(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IOfferSettings CreateWithKey(string name, string offerCode, Guid offerProviderKey, OfferComponentDefinitionCollection componentDefinitions)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IOfferSettings entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IOfferSettings> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IOfferSettings entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IOfferSettings> entities)
        {
            throw new NotImplementedException();
        }
    }
}
