namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using LightInject;

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
    internal class InvoiceLineItemRepository : NPocoLineItemRespositoryBase<IInvoiceLineItem, InvoiceItemDto, InvoiceLineItemFactory>, IInvoiceLineItemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceLineItemRepository"/> class.
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
        public InvoiceLineItemRepository(IDatabaseUnitOfWork work, [Inject(Constants.Repository.DisabledCache)] ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<InvoiceItemDto>();
        }


        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchInvoiceItem.pk = @Key";
        }


        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchInvoiceItem WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        protected override InvoiceLineItemFactory GetFactoryInstance()
        {
            return new InvoiceLineItemFactory();
        }
    }
}
