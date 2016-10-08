namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;

    /// <inheritdoc/>
    internal partial class NoteRepository : ISearchTermRepository<INote>
    {
        /// <summary>
        /// The valid sort fields.
        /// </summary>
        private static readonly string[] ValidSortFields = { "createdate" };

        /// <inheritdoc/>
        public PagedCollection<INote> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var terms = searchTerm.Split(' ');

            var sql = GetBaseQuery(false);

            if (terms.Any())
            {
                var preparedTerms = string.Format("%{0}%", string.Join("%", terms));

                sql.Where("message LIKE @msg", new { @msg = preparedTerms });
            }

            sql.AppendOrderExpression<NoteDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<NoteDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
        }

        /// <summary>
        /// Validates the sort by field
        /// </summary>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ValidateSortByField(string sortBy)
        {
            return ValidSortFields.Contains(sortBy.ToLower()) ? sortBy : "createdate";
        }
    }
}
