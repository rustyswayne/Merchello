using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchello.Core.Persistence.Repositories
{
    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;

    internal partial class OrderRepository : ISearchTermRepository<IOrder>
    {
        public PagedCollection<IOrder> SearchForTerm(
            string searchTerm,
            long page,
            long itemsPerPage,
            string orderExpression,
            Direction direction = Direction.Descending)
        {
            throw new NotImplementedException();
        }
    }
}
