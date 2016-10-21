namespace Merchello.Core.DI
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a register collection, e.g. an immutable enumeration of items.
    /// </summary>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public interface IRegister<out TItem> : IEnumerable<TItem>
    {
        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        int Count { get; }
    }
}