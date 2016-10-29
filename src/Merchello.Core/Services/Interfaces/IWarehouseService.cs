namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a data service for <see cref="IWarehouse"/>.
    /// </summary>
    public interface IWarehouseService : IWarehouseCatalogService
    {
        /// <summary>
        /// Gets a <see cref="IWarehouse"/> by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IWarehouse"/>.
        /// </returns>
        IWarehouse GetByKey(Guid key);

        /// <summary>
        /// Gets all <see cref="IWarehouse"/>s.
        /// </summary>
        /// <param name="keys">
        /// Optional keys parameter to limit results.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IWarehouse"/>.
        /// </returns>
        IEnumerable<IWarehouse> GetAll(params Guid[] keys);

        /// <summary>
        /// Saves a single <see cref="IWarehouse"/> object
        /// </summary>
        /// <param name="warehouse">The <see cref="IWarehouse"/> to save</param>
        void Save(IWarehouse warehouse);

        /// <summary>
        /// Saves a collection of <see cref="IWarehouse"/> objects
        /// </summary>
        /// <param name="warehouseList">Collection of <see cref="IWarehouse"/> to save</param>
        void Save(IEnumerable<IWarehouse> warehouseList);


        /// <summary>
        /// Gets the default <see cref="IWarehouse"/>
        /// </summary>
        /// <returns>
        /// The <see cref="IWarehouse"/>.
        /// </returns>
        IWarehouse GetDefaultWarehouse();
    }
}