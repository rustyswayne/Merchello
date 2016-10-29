namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class StoreService : IStoreService
    {
        /// <inheritdoc/>
        public IStore GetByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IStore entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IStore> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IStore entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IStore> entities)
        {
            throw new NotImplementedException();
        }
    }
}
