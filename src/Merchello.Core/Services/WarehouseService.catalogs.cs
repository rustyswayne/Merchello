namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <inheritdoc/>
    public partial class WarehouseService : IWarehouseCatalogService
    {
        /// <inheritdoc/>
        public IWarehouseCatalog GetWarehouseCatalogByKey(Guid key)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IWarehouseCatalog> GetByWarehouseKey(Guid warehouseKey)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IWarehouseCatalog> GetAllWarehouseCatalogs()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IWarehouseCatalog CreateWarehouseCatalogWithKey(Guid warehouseKey, string name, string description = "")
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IWarehouseCatalog warehouseCatalog)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Save(IEnumerable<IWarehouseCatalog> warehouseCatalogs)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IWarehouseCatalog warehouseCatalog)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(IEnumerable<IWarehouseCatalog> warehouseCatalogs)
        {
            throw new NotImplementedException();
        }
    }
}
