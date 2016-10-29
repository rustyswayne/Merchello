namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Counting;

    using NPoco;

    /// <summary>
    /// Represents a data service for <see cref="IProductOption"/>.
    /// </summary>
    public interface IProductOptionService : IGetAllService<IProductOption>
    {
        /// <summary>
        /// Creates a <see cref="IProductOption"/> without saving it to the database.
        /// </summary>
        /// <param name="name">
        /// The option name.
        /// </param>
        /// <param name="shared">
        /// A value indicating whether or not this is a shared option (usable by multiple products).
        /// </param>
        /// <param name="required">
        /// A value indicating whether or not the option is required (currently not implemented).
        /// </param>
        /// <returns>
        /// The <see cref="IProductOption"/>.
        /// </returns>
        IProductOption Create(string name, bool shared = false, bool required = true);

        /// <summary>
        /// Creates a <see cref="IProductOption"/> and saves it to the database.
        /// </summary>
        /// <param name="name">
        /// The option name.
        /// </param>
        /// <param name="shared">
        /// A value indicating whether or not this is a shared option (usable by multiple products).
        /// </param>
        /// <param name="required">
        /// A value indicating whether or not the option is required (currently not implemented).
        /// </param>
        /// <returns>
        /// The <see cref="IProductOption"/>.
        /// </returns>
        IProductOption CreateProductOptionWithKey(string name, bool shared = false, bool required = true);

        /// <summary>
        /// Gets a product attribute by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IProductAttribute"/>.
        /// </returns>
        IProductAttribute GetProductAttributeByKey(Guid key);

        /// <summary>
        /// Gets <see cref="IProductAttribute"/> by a an array of keys.
        /// </summary>
        /// <param name="keys">
        /// The collection attribute keys.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IEnumerable{IProductAttribute}"/>.
        /// </returns>
        IEnumerable<IProductAttribute> GetProductAttributes(IEnumerable<Guid> keys);

        /// <summary>
        /// Gets the usage information about the product option, including the attribute usage.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="IProductOptionUseCount"/>.
        /// </returns>
        IProductOptionUseCount GetProductOptionUseCount(IProductOption option);

        /// <summary>
        /// The get product option share count.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int GetProductOptionShareCount(IProductOption option);

        /// <summary>
        /// Gets a paged collection of <see cref="IProductOption"/>.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="sortDirection">
        /// The sort direction.
        /// </param>
        /// <param name="sharedOnly">
        /// The shared Only.
        /// </param>
        /// <returns>
        /// The <see cref="Page{IProductOption}"/>.
        /// </returns>
        PagedCollection<IProductOption> GetPagedCollection(long page, long itemsPerPage, string sortBy = "", Direction sortDirection = Direction.Descending, bool sharedOnly = true);


        /// <summary>
        /// Gets a paged collection of <see cref="IProductOption"/>.
        /// </summary>
        /// <param name="term">
        /// A search term to filter by
        /// </param>
        /// <param name="page">
        /// The page requested.
        /// </param>
        /// <param name="itemsPerPage">
        /// The number of items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by field.
        /// </param>
        /// <param name="sortDirection">
        /// The sort direction.
        /// </param>
        /// <param name="sharedOnly">
        /// Indicates whether or not to only include shared option.
        /// </param>
        /// <returns>
        /// The <see cref="Page{IProductOption}"/>.
        /// </returns>
        PagedCollection<IProductOption> GetPagedCollection(string term, long page, long itemsPerPage, string sortBy = "", Direction sortDirection = Direction.Descending, bool sharedOnly = true);
    }
}