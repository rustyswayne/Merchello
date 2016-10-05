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
    internal partial class OfferSettingsRepository : NPocoEntityRepositoryBase<IOfferSettings, OfferSettingsDto, OfferSettingsFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            return Sql().Select(isCount ? "COUNT(*)" : "*")
                .From<OfferSettingsDto>();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            return "merchOfferSettings.pk = @Key";
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            var list = new List<string>
            {
                "DELETE FROM merchOfferSettings WHERE pk = @Key"
            };

            return list;
        }

        /// <inheritdoc/>
        protected override void PersistDeletedItem(IOfferSettings entity)
        {
            _offerRedeemedRepository.UpdateForOfferSettingDelete(entity.Key);
            base.PersistDeletedItem(entity);
        }
    }
}
