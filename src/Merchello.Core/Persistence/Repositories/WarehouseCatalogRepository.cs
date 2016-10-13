namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class WarehouseCatalogRepository : IWarehouseCategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseCatalogRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public WarehouseCatalogRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<IWarehouseCatalog> GetByWarehouseKey(Guid warehouseKey)
        {
            var query = Query.Where(x => x.WarehouseKey == warehouseKey);
            return this.GetByQuery(query);
        }

        /// <inheritdoc/>
        protected override WarehouseCatalogFactory GetFactoryInstance()
        {
            return new WarehouseCatalogFactory();
        }
    }
}