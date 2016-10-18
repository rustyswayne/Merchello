namespace Merchello.Core.EntityCollections
{
    using Merchello.Core.Models;

    /// <summary>
    /// Represents an EntityCollectionProvider.
    /// </summary>
    public interface IEntityCollectionProvider
    {
        /// <summary>
        /// Gets the entity collection.
        /// </summary>
        IEntityCollection EntityCollection { get; }
    }
}