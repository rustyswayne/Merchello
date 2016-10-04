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
    internal partial class CustomerRepository : ISearchTermRepository<ICustomer>
    {
        /// <inheritdoc/>
        public PagedCollection<ICustomer> SearchForTerm(string searchTerm, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending)
        {
            var sql = this.BuildSearchSql(searchTerm).AppendOrderExpression(orderExpression, direction);

            return Database.Page<CustomerDto>(page, itemsPerPage, sql)
                        .Map(MapDtoCollection);
        }
    }
}
