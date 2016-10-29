namespace Merchello.Core.Persistence.Repositories
{
    using System;
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
    internal class NotificationMethodRepository : NPocoEntityRepositoryBase<INotificationMethod, NotificationMethodDto, NotificationMethodFactory>, INotificationMethodRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMethodRepository"/> class.
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
        public NotificationMethodRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers)
            : base(work, cache, logger, mappers)
        {
        }

        /// <inheritdoc/>
        public bool Exists(Guid providerKey, string serviceCode)
        {
            var query = Query.Where(x => x.ProviderKey == providerKey && x.ServiceCode == serviceCode);
            return Count(query) > 0;
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<NotificationMethodDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchNotificationMethod.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchNotificationMessage WHERE methodKey = @Key",
                "DELETE FROM merchNotificationMethod WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        protected override NotificationMethodFactory GetFactoryInstance()
        {
            return new NotificationMethodFactory();
        }
    }
}