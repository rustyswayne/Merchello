namespace Merchello.Core.Models
{
    /// <summary>
    /// The paged collection.
    /// </summary>
    public class PagedCollection
    {
        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the sort field.
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets a value indicating whether is first page.
        /// </summary>
        public bool IsFirstPage
        {
            get
            {
                return this.CurrentPage <= 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is last page.
        /// </summary>
        public bool IsLastPage
        {
            get
            {
                return this.CurrentPage >= this.TotalPages;
            }
        }
    }
}