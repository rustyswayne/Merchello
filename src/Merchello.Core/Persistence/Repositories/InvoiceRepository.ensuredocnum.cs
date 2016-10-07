namespace Merchello.Core.Persistence.Repositories
{
    using System;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IEnsureDocumentNumberRepository
    {
        /// <inheritdoc/>
        public int GetMaxDocumentNumber()
        {
            throw new NotImplementedException();
        }
    }
}
