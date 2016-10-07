namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.Querying;
    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Querying;

    using NPoco;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : NPocoRepositoryBase<IInvoice>
    {
        /// <inheritdoc/>
        protected override IInvoice PerformGet(Guid key)
        {
            var sql = GetBaseQuery(false).Where(GetBaseWhereClause(), new { Key = key });

            var dto = Database.Fetch<InvoiceDto>(sql).FirstOrDefault();

            if (dto == null) return null;

            var lineItems = _invoiceLineItemRepository.GetLineItemCollection(key);
            var orders = _orderRepository.GetOrderCollection(key);
            var notes = _noteRepository.GetNotes(key);
            var factory = new InvoiceFactory(lineItems, orders, notes);
            return factory.BuildEntity(dto);
        }

        /// <inheritdoc/>
        protected override IEnumerable<IInvoice> PerformGetAll(params Guid[] keys)
        {
            if (keys.Any())
            {
                return Database.Fetch<InvoiceDto>("WHERE pk in (@keys)", new { keys = keys })
                    .Select(x => Get(x.Key));
            }

            return Database.Fetch<InvoiceDto>().Select(x => Get(x.Key));
        }

        /// <inheritdoc/>
        protected override IEnumerable<IInvoice> PerformGetByQuery(IQuery<IInvoice> query)
        {
            var sqlClause = GetBaseQuery(false);
            var translator = new SqlTranslator<IInvoice>(sqlClause, query);
            var sql = translator.Translate();

            var dtos = Database.Fetch<InvoiceDto>(sql);

            return GetAll(dtos.Select(x => x.Key).ToArray());
        }

        /// <inheritdoc/>
        protected override void PersistNewItem(IInvoice entity)
        {
            ((Entity)entity).AddingEntity();

            var factory = new InvoiceFactory(entity.Items, new OrderCollection(), entity.Notes);
            var dto = factory.BuildDto(entity);

            Database.Insert(dto);
            entity.Key = dto.Key;

            SaveNotes(entity);

            _invoiceLineItemRepository.SaveLineItem(entity.Items, entity.Key);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override void PersistUpdatedItem(IInvoice entity)
        {
            SaveNotes(entity);

            ((Entity)entity).UpdatingEntity();

            var factory = new InvoiceFactory(entity.Items, entity.Orders, entity.Notes);
            var dto = factory.BuildDto(entity);

            Database.Update(dto);

            _invoiceLineItemRepository.SaveLineItem(entity.Items, entity.Key);

            entity.ResetDirtyProperties();
        }

        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            var sql = Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<InvoiceDto>()
                .InnerJoin<InvoiceStatusDto>()
                .On<InvoiceDto, InvoiceStatusDto>(left => left.InvoiceStatusKey, right => right.Key);

            return sql;
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchInvoice.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchNote WHERE entityKey = @Key",
                    "DELETE FROM merchAppliedPayment WHERE invoiceKey = @Key",
                    "DELETE FROM merchInvoiceItem WHERE invoiceKey = @Key",
                    "DELETE FROM merchInvoiceIndex WHERE invoiceKey = @Key",
                    "DELETE FROM merchOfferRedeemed WHERE invoiceKey = @Key",
                    "DELETE FROM merchInvoice2EntityCollection WHERE invoiceKey = @Key",
                    "DELETE FROM merchInvoice WHERE pk = @Key"
                };

            return list;
        }
    }
}
