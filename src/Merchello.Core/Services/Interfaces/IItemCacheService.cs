namespace Merchello.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.TypeFields;

    /// <summary>
    /// Represents a data service for <see cref="IItemCache"/>.
    /// </summary>
    public interface IItemCacheService : IService<IItemCache>
    {
        /// <summary>
        /// Creates an item cache (or retrieves an existing one) based on type and saves it to the database
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="itemCache">
        /// The item Cache.
        /// </param>
        /// <returns>
        /// The <see cref="IItemCache"/>.
        /// </returns>
        IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCache);

        /// <summary>
        /// Creates an item cache (or retrieves an existing one) based on type and saves it to the database
        /// </summary>
        /// <param name="customer">
        /// The customer.
        /// </param>
        /// <param name="itemCacheType">
        /// The item Cache Type.
        /// </param>
        /// <param name="versionKey">
        /// The version Key.
        /// </param>
        /// <returns>
        /// The <see cref="IItemCache"/>.
        /// </returns>
        IItemCache GetItemCacheWithKey(ICustomerBase customer, ItemCacheType itemCacheType, Guid versionKey);

        /// <summary>
        /// Gets a collection of <see cref="IItemCache"/> for by an entity Key.
        /// </summary>
        /// <param name="entityKey">
        /// The entity key (usually the customer key).
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IItemCache}"/>.
        /// </returns>
        IEnumerable<IItemCache> GetItemCaches(Guid entityKey);


        /// <summary>
        /// Gets a collection of <see cref="IItemCache"/> for by an entity Key (usually the customer key).
        /// </summary>
        /// <param name="entityKey">
        /// The entity key (usually the customer key).
        /// </param>
        /// <param name="itemCacheTfKey">
        /// The item cache type field Key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IItemCache}"/>.
        /// </returns>
        IEnumerable<IItemCache> GetEntityItemCaches(Guid entityKey, Guid itemCacheTfKey);


        /// <summary>
        /// Gets a collection of <see cref="IItemCache"/> objects by the <see cref="ICustomerBase"/>
        /// </summary>
        /// <param name="customer">The customer associated with the <see cref="IItemCache"/></param>
        /// <returns>A collection of <see cref="IItemCache"/></returns>
        IEnumerable<IItemCache> GetItemCacheByCustomer(ICustomerBase customer);

        /// <summary>
        /// Returns the consumer's registry of a given type
        /// </summary>
        /// <param name="customer">The <see cref="ICustomerBase"/></param>
        /// <param name="itemCacheTfKey"><see cref="ITypeField"/>.TypeKey</param>
        /// <returns><see cref="IItemCache"/></returns>
        /// <remarks>
        /// Public use of this method is intended to access ItemCacheType.Custom records
        /// </remarks>
        IItemCache GetItemCacheByCustomer(ICustomerBase customer, Guid itemCacheTfKey);

        /// <summary>
        /// Gets an <see cref="IItemCache"/> object by the <see cref="ICustomerBase"/>
        /// </summary>
        /// <param name="customer">The <see cref="ICustomerBase"/> object</param>
        /// <param name="itemCacheType">The type of the <see cref="IItemCache"/></param>
        /// <returns><see cref="IItemCache"/></returns>
        IItemCache GetItemCacheByCustomer(ICustomerBase customer, ItemCacheType itemCacheType);


        /// <summary>
        /// Gets a page of <see cref="IItemCache"/>
        /// </summary>
        /// <param name="itemCacheType">
        /// The item cache type.
        /// </param>
        /// <param name="startDate">
        /// The start Date.
        /// </param>
        /// <param name="endDate">
        /// The end Date.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="sortBy">
        /// The sort by.
        /// </param>
        /// <param name="direction">
        /// The sort direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection{IItemCache}"/>.
        /// </returns>
        PagedCollection<IItemCache> GetCustomerItemCachePage(ItemCacheType itemCacheType, DateTime startDate, DateTime endDate, long page, long itemsPerPage, string sortBy = "", Direction direction = Direction.Descending);


        /// <summary>
        /// Gets the count of of item caches for a customer type.
        /// </summary>
        /// <param name="itemCacheType">
        /// The item cache type.
        /// </param>
        /// <param name="customerType">
        /// The customer type.
        /// </param>
        /// <returns>
        /// The count of item caches.
        /// </returns>
        int Count(ItemCacheType itemCacheType, CustomerType customerType);

        /// <summary>
        /// Gets the count of of item caches for a customer type for a given date range.
        /// </summary>
        /// <param name="itemCacheType">
        /// The item cache type.
        /// </param>
        /// <param name="customerType">
        /// The customer type.
        /// </param>
        /// <param name="startDate">
        /// The start Date.
        /// </param>
        /// <param name="endDate">
        /// The end Date.
        /// </param>
        /// <returns>
        /// The count of item caches.
        /// </returns>
        int Count(ItemCacheType itemCacheType, CustomerType customerType, DateTime startDate, DateTime endDate);
    }
}