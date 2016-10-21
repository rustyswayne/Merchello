﻿namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal class AnonymousCustomerRepository : NPocoEntityRepositoryBase<IAnonymousCustomer, AnonymousCustomerDto, AnonymousCustomerFactory>, IAnonymousCustomerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousCustomerRepository"/> class.
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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        public AnonymousCustomerRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers)
         : base(work, cache, logger, mappers)
        {
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<AnonymousCustomerDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchAnonymousCustomer.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchItemCacheItem WHERE itemCacheKey IN (SELECT pk FROM merchItemCache WHERE entityKey = @Key)",
                    "DELETE FROM merchItemCache WHERE entityKey = @Key",
                    "DELETE FROM merchAnonymousCustomer WHERE pk = @Key",
                };

            return list;
        }

        /// <inheritdoc/>
        protected override AnonymousCustomerFactory GetFactoryInstance()
        {
            return new AnonymousCustomerFactory();
        }
    }
}