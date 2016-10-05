namespace Merchello.Core.Persistence.Repositories
{
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class OfferRedeemedRepository : ISearchTermRepository<IOfferRedeemed>
    {
        /// <inheritdoc/>
        public PagedCollection<IOfferRedeemed> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            searchTerm = searchTerm.Replace(",", " ");
            var terms = searchTerm.Split(' ').Select(x => x.Trim()).ToArray();

            var sql = Sql().SelectAll().From<OfferRedeemedDto>();
            sql.Where("offerCode LIKE @term", new { @term = string.Format("%{0}%", string.Join("% ", terms)).Trim() })
                .AppendOrderExpression(orderExpression, direction);

            return Database.Page<OfferRedeemedDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }
    }
}
