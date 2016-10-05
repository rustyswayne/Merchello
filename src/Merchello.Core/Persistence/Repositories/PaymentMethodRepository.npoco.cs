namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class PaymentMethodRepository : NPocoEntityRepositoryBase<IPaymentMethod, PaymentMethodDto, PaymentMethodFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<PaymentMethodDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchPaymentMethod.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchPaymentMethod WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(IPaymentMethod entity)
        {
            _paymentRepository.UpdateForPaymentMethodDelete(entity.Key);
            base.PersistDeletedItem(entity);
        }
    }
}
