namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    internal partial class ProductVariantRepository : IBulkOperationRepository<IProductVariant>
    {
        /// <inheritdoc/>
        public void PersistNewItems(IEnumerable<IProductVariant> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void PersistUpdatedItems(IEnumerable<IProductVariant> entities)
        {
            throw new NotImplementedException();
        }
    }
}
