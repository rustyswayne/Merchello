namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
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
        public InvoiceRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetDistinctCurrencyCodes()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public decimal SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public decimal SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku)
        {
            throw new NotImplementedException();
        }
    }
}