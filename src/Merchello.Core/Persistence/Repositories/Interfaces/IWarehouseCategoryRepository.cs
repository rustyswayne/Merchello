namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a repository responsible for <see cref="IWarehouseCatalog"/> entities.
    /// </summary>
    public interface IWarehouseCategoryRepository : INPocoEntityRepository<IWarehouseCatalog>
    {
        /// <summary>
        /// Gets a collection of <see cref="IWarehouseCatalog"/> by a warehouse key.
        /// </summary>
        /// <param name="warehouseKey">
        /// The warehouse key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IWarehouseCatalog"/>.
        /// </returns>
        IEnumerable<IWarehouseCatalog> GetByWarehouseKey(Guid warehouseKey);
    }
}