namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class EntityCollectionService : IEntityCollectionService
    {
        /// <inheritdoc/>
        public IEntityCollection Create(EntityType entityType, Guid providerKey, string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEntityCollection Create(Guid entityTfKey, Guid providerKey, string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEntityCollection CreateWithKey(EntityType entityType, Guid providerKey, string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEntityCollection CreateWithKey(Guid entityTfKey, Guid providerKey, string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEntityCollection entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IEntityCollection> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEntityCollection entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IEntityCollection> entities)
        {
            throw new NotImplementedException();
        }
    }
}
