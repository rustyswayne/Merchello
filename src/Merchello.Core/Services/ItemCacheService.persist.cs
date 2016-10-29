namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ItemCacheService : IItemCacheService
    {
        /// <inheritdoc/>
        public IItemCache GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IItemCache entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IItemCache> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IItemCache entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IItemCache> entities)
        {
            throw new NotImplementedException();
        }
    }
}
