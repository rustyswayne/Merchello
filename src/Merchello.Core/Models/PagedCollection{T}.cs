namespace Merchello.Core.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// The paged collection.
    /// </summary>
    /// <typeparam name="TResultType">
    /// The type to be paged
    /// </typeparam>
    public class PagedCollection<TResultType> : PagedCollection
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public IEnumerable<TResultType> Items { get; set; }

        /// <summary>
        /// Returns an empty page for defaults.
        /// </summary>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        public static PagedCollection<TResultType> Empty()
        {
            return new PagedCollection<TResultType>
            {
                CurrentPage = 1,
                Items = Enumerable.Empty<TResultType>(),
                PageSize = 20,
                TotalItems = 0,
                TotalPages = 0
            };
        }
    }
}