namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NodaMoney;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// The <see cref="IInvoiceLineItemRepository"/>.
        /// </summary>
        private readonly IInvoiceLineItemRepository _invoiceLineItemRepository;

        /// <summary>
        /// The <see cref="IOrderRepository"/>.
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        /// <summary>
        /// The <see cref="INoteRepository"/>.
        /// </summary>
        private readonly INoteRepository _noteRepository;

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
        /// <param name="mappers">
        /// The <see cref="IMapperRegister"/>.
        /// </param>
        /// <param name="invoiceLineItemRepository">
        /// The <see cref="IInvoiceLineItemRepository"/>
        /// </param>
        /// <param name="orderRepository">
        /// The <see cref="IOrderRepository"/>
        /// </param>
        /// <param name="noteRepository">
        /// The <see cref="INoteRepository"/>
        /// </param>
        public InvoiceRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMapperRegister mappers, IInvoiceLineItemRepository invoiceLineItemRepository, IOrderRepository orderRepository, INoteRepository noteRepository)
            : base(work, cache, logger, mappers)
        {
            Ensure.ParameterNotNull(invoiceLineItemRepository, nameof(invoiceLineItemRepository));
            Ensure.ParameterNotNull(orderRepository, nameof(orderRepository));
            Ensure.ParameterNotNull(noteRepository, nameof(noteRepository));

            _invoiceLineItemRepository = invoiceLineItemRepository;
            _orderRepository = orderRepository;
            _noteRepository = noteRepository;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetDistinctCurrencyCodes()
        {
            var sql = Sql()
                        .SelectDistinct<InvoiceDto>(x => x.CurrencyCode)
                        .From<InvoiceDto>();

            return Database.Fetch<DistinctCurrencyCodeDto>(sql)
                    .Select(x => x.CurrencyCode);
        }

        /// <inheritdoc/>
        public Money SumInvoiceTotals(DateTime startDate, DateTime endDate, string currencyCode)
        {
            var sql = Sql().SelectSum<InvoiceDto>(x => x.Total)
                            .From<InvoiceDto>()
                            .Where<InvoiceDto>(x => x.CurrencyCode == currencyCode)
                            .WhereBetween<InvoiceDto>(x => x.InvoiceDate, startDate, endDate);
                          

            return new Money(Database.ExecuteScalar<decimal>(sql), currencyCode);
        }

        /// <inheritdoc/>
        public Money SumLineItemTotalsBySku(DateTime startDate, DateTime endDate, string currencyCode, string sku)
        {
            var sumExp = string.Format(
                    "{0}.{1} * {0}.{2}",
                    SqlSyntax.GetQuotedTableName("merchInvoiceItem"),
                    SqlSyntax.GetQuotedColumnName("quantity"),
                    SqlSyntax.GetQuotedColumnName("price"));


            var sql = Sql().SelectSum(sumExp)
                        .From<InvoiceDto>()
                        .InnerJoin<InvoiceItemDto>()
                        .On<InvoiceDto, InvoiceItemDto>(left => left.Key, right => right.ContainerKey)
                        .Where<InvoiceItemDto>(x => x.Sku == sku)
                        .Where<InvoiceDto>(x => x.CurrencyCode == currencyCode)
                        .WhereBetween<InvoiceDto>(x => x.InvoiceDate, startDate, endDate);

            return new Money(Database.ExecuteScalar<decimal>(sql), currencyCode);
        }

        /// <summary>
        /// Maps a collection of <see cref="InvoiceDto"/> to <see cref="IInvoice"/>.
        /// </summary>
        /// <param name="dtos">
        /// The collection of DTOs.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IInvoice}"/>.
        /// </returns>
        protected virtual IEnumerable<IInvoice> MapDtoCollection(IEnumerable<InvoiceDto> dtos)
        {
            return GetAll(dtos.Select(dto => dto.Key).ToArray());
        }
    }
}