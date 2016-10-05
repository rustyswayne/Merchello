namespace Merchello.Core.Persistence.Repositories
{
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : ISearchTermRepository<IProductOption>
    {
        /// <summary>
        /// Valid sort fields.
        /// </summary>
        private static readonly string[] _validSortFields = { "name" };

        /// <inheritdoc/>
        public PagedCollection<IProductOption> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            return SearchForTerm(searchTerm, page, itemsPerPage, orderExpression, true, direction);
        }

        /// <inheritdoc/>
        public PagedCollection<IProductOption> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, bool sharedOnly, Direction direction = Direction.Descending)
        {
            var sql = BuildSearchSql(searchTerm, sharedOnly)
                .AppendOrderExpression(ValidateSortField(orderExpression), direction);

            return Database.Page<ProductOptionDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <summary>
        /// Validates the orderExpression field.
        /// </summary>
        /// <param name="orderExpression">
        /// The orderExpression field.
        /// </param>
        /// <returns>
        /// A validated value.
        /// </returns>
        private static string ValidateSortField(string orderExpression)
        {
            return _validSortFields.Contains(orderExpression) ? orderExpression : "name";
        }

        /// <summary>
        /// Builds the product option search SQL.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="sharedOnly">
        /// The shared Only.
        /// </param>
        /// <returns>
        /// The <see cref="Sql"/>.
        /// </returns>
        private Sql<SqlContext> BuildSearchSql(string searchTerm, bool sharedOnly)
        {
            searchTerm = searchTerm.Replace(",", " ");
            var invidualTerms = searchTerm.Split(' ');

            var terms = invidualTerms.Where(x => !string.IsNullOrEmpty(x)).ToList();


            var sql = Sql();
            sql.Select("*").From<ProductOptionDto>();

            if (terms.Any())
            {
                var preparedTerms = string.Format("%{0}%", string.Join("% ", terms)).Trim();

                sql.Where("name LIKE @name", new { @name = preparedTerms });
            }

            if (sharedOnly) sql.Where("shared = @shared", new { @shared = true });

            return sql;
        }
    }
}
