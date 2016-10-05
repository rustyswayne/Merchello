namespace Merchello.Core.Persistence.Repositories
{
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Models.Rdbms;
    using Merchello.Core.Persistence.Factories;

    using NPoco;

    /// <inheritdoc/>
    internal partial class ProductOptionRepository : NPocoEntityRepositoryBase<IProductOption, ProductOptionDto, ProductOptionFactory>
    {
        /// <inheritdoc/>
        protected override Sql<SqlContext> GetBaseQuery(bool isCount)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        protected override string GetBaseWhereClause()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        protected override IEnumerable<string> GetDeleteClauses()
        {
            throw new System.NotImplementedException();
        }
    }
}
