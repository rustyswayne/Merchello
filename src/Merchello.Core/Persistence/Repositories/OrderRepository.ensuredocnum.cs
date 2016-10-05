namespace Merchello.Core.Persistence.Repositories
{
    using System;

    /// <inheritdoc/>
    internal partial class OrderRepository : IEnsureDocumentNumberRepository
    {
        /// <inheritdoc/>
        public int GetMaxDocumentNumber()
        {
            return Database.ExecuteScalar<int>("SELECT MAX([merchOrder].[orderNumber]) FROM [merchOrder]");
        }
    }
}
