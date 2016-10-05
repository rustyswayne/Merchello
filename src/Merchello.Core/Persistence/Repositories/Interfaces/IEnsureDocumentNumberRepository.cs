namespace Merchello.Core.Persistence.Repositories
{
    /// <summary>
    /// Represents a repository that ensures document numbers are valid.
    /// </summary>
    public interface IEnsureDocumentNumberRepository
    {
        /// <summary>
        /// Ensures document numbers such as invoice number, order number and shipment number are unique and above previously
        /// offered numbers.
        /// </summary>
        /// <returns>
        /// The next document number.
        /// </returns>
        int GetMaxDocumentNumber();
    }
}