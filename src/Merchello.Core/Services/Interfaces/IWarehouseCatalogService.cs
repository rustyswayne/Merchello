namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IWarehouseCatalog"/>.
    /// </summary>
    public interface IWarehouseCatalogService : IService
    {
        /// <summary>
        /// Gets a <see cref="IWarehouseCatalog"/> by it's unique key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IWarehouseCatalog"/>.
        /// </returns>
        IWarehouseCatalog GetWarehouseCatalogByKey(Guid key);

        /// <summary>
        /// Get a collection of <see cref="IWarehouseCatalog"/> by warehouse key.
        /// </summary>
        /// <param name="warehouseKey">
        /// The warehouse key.
        /// </param>
        /// <returns>
        /// A collection of <see cref="IWarehouseCatalog"/>.
        /// </returns>
        IEnumerable<IWarehouseCatalog> GetByWarehouseKey(Guid warehouseKey);

        /// <summary>
        /// Gets a collection of all <see cref="IWarehouseCatalog"/>.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="IWarehouseCatalog"/>.
        /// </returns>
        IEnumerable<IWarehouseCatalog> GetAllWarehouseCatalogs();

        /// <summary>
        /// Creates warehouse catalog and persists it to the database.
        /// </summary>
        /// <param name="warehouseKey">
        /// The warehouse key.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="IWarehouseCatalog"/>.
        /// </returns>
        IWarehouseCatalog CreateWarehouseCatalogWithKey(Guid warehouseKey, string name, string description = "");

        /// <summary>
        /// Saves a single <see cref="IWarehouseCatalog"/>.
        /// </summary>
        /// <param name="warehouseCatalog">
        /// The warehouse catalog.
        /// </param>
        void Save(IWarehouseCatalog warehouseCatalog);

        /// <summary>
        /// Saves a collection of <see cref="IWarehouseCatalog"/>.
        /// </summary>
        /// <param name="warehouseCatalogs">
        /// The warehouse catalogs.
        /// </param>
        void Save(IEnumerable<IWarehouseCatalog> warehouseCatalogs);

        /// <summary>
        /// Deletes a single <see cref="IWarehouseCatalog"/>.
        /// </summary>
        /// <param name="warehouseCatalog">
        /// The warehouse catalog.
        /// </param>
        /// <remarks>
        /// Cannot delete the default catalog in the default warehouse
        /// </remarks>
        void Delete(IWarehouseCatalog warehouseCatalog);

        /// <summary>
        /// Deletes a collection of <see cref="IWarehouseCatalog"/>.
        /// </summary>
        /// <param name="warehouseCatalogs">
        /// The warehouse catalogs.
        /// </param>
        /// <remarks>
        /// Cannot delete the default catalog in the default warehouse
        /// </remarks>
        void Delete(IEnumerable<IWarehouseCatalog> warehouseCatalogs);
    }
}