namespace Merchello.Core.Persistence.Repositories
{
    using System;

    /// <inheritdoc/>
    internal partial class InvoiceRepository : IEnsureDocumentNumberRepository
    {
        /// <inheritdoc/>
        public int GetMaxDocumentNumber()
        {
            return Database.ExecuteScalar<int>("SELECT MAX([merchInvoice].[invoiceNumber]) FROM [merchInvoice]");
        }
    }
}
