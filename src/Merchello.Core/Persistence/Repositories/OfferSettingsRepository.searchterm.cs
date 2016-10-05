namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class OfferSettingsRepository : ISearchTermRepository<IOfferSettings>
    {
        /// <inheritdoc/>
        public PagedCollection<IOfferSettings> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            searchTerm = searchTerm.Replace(",", " ");
            var terms = searchTerm.Split(' ').Select(x => x.Trim()).ToArray();

            var sql = Sql().SelectAll().From<OfferSettingsDto>();
            if (terms.Any())
            {
                sql.Where("name LIKE @term OR offerCode LIKE @offerCode", new { @term = string.Format("%{0}%", string.Join("% ", terms)).Trim(), offerCode = string.Format("%{0}%", string.Join("% ", terms)).Trim() });
            }
            else
            {
                sql.Where("name LIKE @term OR offerCode LIKE @term", new { @term = string.Format("%{0}%", string.Join("% ", terms)).Trim() });
            }

            sql.AppendOrderExpression(orderExpression, direction);

            return Database.Page<OfferSettingsDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
