namespace Merchello.Core.Persistence.Repositories
{
    using System;

    using Merchello.Core.Models;

    /// <summary>
    /// Represents a product option repository.
    /// </summary>
    public interface IProductOptionRepository : INPocoEntityRepository<IProductOption>
    {
        /// <summary>
        /// Updates product options that have detached content association with a detached content item that is being deleted.
        /// </summary>
        /// <param name="detachedContentTypeKey">
        /// The detached content type key.
        /// </param>
        /// <remarks>
        /// Sets the detachedContentTypeKey = NULL
        /// </remarks>
        void UpdateForDetachedContentDelete(Guid detachedContentTypeKey);
    }
}