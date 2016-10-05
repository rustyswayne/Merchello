namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class PaymentRepository : NPocoEntityRepositoryBase<IPayment, PaymentDto, PaymentFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<PaymentDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchPayment.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
                {
                    "DELETE FROM merchAppliedPayment WHERE paymentKey = @Key",
                    "DELETE FROM merchPayment WHERE pk = @Key"
                };

            return list;
        }
    }
}
