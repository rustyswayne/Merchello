namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductRepository : ISearchTermRepository<IProduct>
    {
        /// <inheritdoc/>
        public PagedCollection<IProduct> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var sql = BuildSearchSql(searchTerm)
                    .AppendOrderExpression(orderExpression, direction);

            return Database.Page<ProductDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <summary>
        /// Builds the product search SQL.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <returns>
        /// The <see cref="Sql{SqlContext}"/>.
        /// </returns>
        private Sql<SqlContext> BuildSearchSql(string searchTerm)
        {
            searchTerm = searchTerm.Replace(",", " ");
            var invidualTerms = searchTerm.Split(' ');

            var terms = invidualTerms.Where(x => !string.IsNullOrEmpty(x)).ToList();


            var sql = GetBaseQuery(false);

            if (terms.Any())
            {
                var preparedTerms = string.Format("%{0}%", string.Join("% ", terms)).Trim();

                sql.Where("[merchProductVariant].[sku] LIKE @sku OR [merchProductVariant].[name] LIKE @name", new { @sku = preparedTerms, @name = preparedTerms });
            }

            return sql;
        }
    }
}
