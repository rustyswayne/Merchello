namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using System.Linq;

    /// <inheritdoc/>
    internal partial class CustomerRepository : ISearchTermRepository<ICustomer>
    {
        /// <summary>
        /// The valid sort fields.
        /// </summary>
        private static readonly string[] ValidSortFields = { "firstname", "lastname", "loginname", "email", "lastactivitydate" };

        /// <inheritdoc/>
        public PagedCollection<ICustomer> SearchForTerm(
            string searchTerm,
            long page,
            long itemsPerPage,
            string orderExpression,
            Direction direction = Direction.Descending)
        {
            var sql = this.BuildSearchSql(searchTerm)
                .AppendOrderExpression<CustomerDto>(ValidateSortByField(orderExpression), direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql).Map(MapDtoCollection);
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
            return ValidSortFields.Contains(sortBy.ToLower()) ? sortBy : "loginName";
        }
    }
}
