namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class ProductOptionService : IProductOptionService
    {
        /// <inheritdoc/>
        public void Save(IProductOption entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IProductOption> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IProductOption entity)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IProductOption> entities)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductOption Create(string name, bool shared = false, bool required = true)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IProductOption CreateProductOptionWithKey(string name, bool shared = false, bool required = true)
        {
            throw new NotImplementedException();
        }
    }
}
