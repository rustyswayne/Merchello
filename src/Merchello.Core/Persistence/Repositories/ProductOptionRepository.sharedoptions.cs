namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : IProductOptionRepository
    {
        /// <inheritdoc/>
        public IEnumerable<IProductOption> GetProductOptions(Guid[] optionKeys, bool sharedOnly = false)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductOptionUseCount GetProductOptionUseCount(IProductOption option)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int GetSharedProductOptionCount(Guid optionKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int GetSharedProductAttributeCount(Guid attributeKey)
        {
            throw new NotImplementedException();
        }
    }
}
