namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Web.UI.WebControls;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Persistence;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IInvoiceRepository
    {
        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var sql = BuildSearchSql(searchTerm)
                        .Where<InvoiceDto>(x => x.InvoiceStatusKey == invoiceStatusKey)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotInvoiceStatus(string searchTerm, Guid invoiceStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var sql = BuildSearchSql(searchTerm)
                        .Where<InvoiceDto>(x => x.InvoiceStatusKey != invoiceStatusKey)
                        .AppendOrderExpression(orderExpression, direction);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<OrderDto>(x => x.InvoiceKey)
                                .From<OrderDto>()
                                .Where<OrderDto>(x => x.OrderStatusKey == orderStatusKey);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql);

            if (orderStatusKey.Equals(Constants.OrderStatus.NotFulfilled))
            {
                var noInvoiceSql = Sql().SelectDistinct<InvoiceDto>(x => x.Key).From<OrderDto>();
                sql.OrNotIn<InvoiceDto>(x => x.Key, noInvoiceSql);
            }

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<OrderDto>(x => x.InvoiceKey)
                                .From<OrderDto>()
                                .Where<OrderDto>(x => x.OrderStatusKey == orderStatusKey);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql);

            if (orderStatusKey.Equals(Constants.OrderStatus.NotFulfilled))
            {
                var noInvoiceSql = Sql().SelectDistinct<InvoiceDto>(x => x.Key).From<OrderDto>();
                sql.OrNotIn<InvoiceDto>(x => x.Key, noInvoiceSql);
            }

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<OrderDto>(x => x.InvoiceKey)
                                .From<OrderDto>()
                                .Where<OrderDto>(x => x.OrderStatusKey != orderStatusKey);

            var sql = Sql().SelectAll()
                        .From<InvoiceDto>()
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <inheritdoc/>
        public PagedCollection<IInvoice> GetInvoicesMatchingTermNotOrderStatus(string searchTerm, Guid orderStatusKey, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var innerSql = Sql().SelectDistinct<OrderDto>(x => x.InvoiceKey)
                                .From<OrderDto>()
                                .Where<OrderDto>(x => x.OrderStatusKey != orderStatusKey);

            var sql = BuildSearchSql(searchTerm)
                        .SingleWhereIn<InvoiceDto>(x => x.Key, innerSql);

            return Database.Page<InvoiceDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
