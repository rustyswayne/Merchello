namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchello.Core.Acquired.Persistence;
    using Merchello.Core.Cache;
    using Merchello.Core.Logging;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;
    using Merchello.Core.Persistence.Mappers;
    using Merchello.Core.Persistence.UnitOfWork;

    using NPoco;

    /// <inheritdoc/>
    internal partial class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="work">
        /// The <see cref="IDatabaseUnitOfWork"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheHelper"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="mappingResolver">
        /// The <see cref="IMappingResolver"/>.
        /// </param>
        public CustomerRepository(IDatabaseUnitOfWork work, ICacheHelper cache, ILogger logger, IMappingResolver mappingResolver)
            : base(work, cache, logger, mappingResolver)
        {
        }

        /// <summary>
        /// Builds customer search SQL.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <returns>
        /// The <see cref="Sql{TContext}"/>.
        /// </returns>
        protected Sql<SqlContext> BuildSearchSql(string searchTerm)
        {
            var invidualTerms = searchTerm.Split(' ');

            var terms = invidualTerms.Where(x => !x.IsNullOrWhiteSpace()).ToList();

            var sql = Sql().SelectAll().From<CustomerDto>();

            if (terms.Any())
            {
                var preparedTerms = string.Format("%{0}%", string.Join("% ", terms)).Trim();

                sql.Where("lastName LIKE @ln OR firstName LIKE @fn OR email LIKE @email", new { @ln = preparedTerms, @fn = preparedTerms, @email = preparedTerms });
            }

            return sql;
        }
    }
}