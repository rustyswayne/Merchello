namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Acquired.Persistence.DatabaseModelDefinitions;
    using Merchello.Core.Models;
    using Merchello.Core.Models.Interfaces;
    using Merchello.Core.Persistence.Querying;

    /// <summary>
    /// Represents an entity collection repository.
    /// </summary>
    public interface IEntityCollectionRepository : INPocoEntityRepository<IEntityCollection>
    {
        /// <summary>
        /// Gets a collection of <see cref="IEntityCollection"/> by a product key.
        /// </summary>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <param name="isFilter">
        /// A value indicating if the query should return filter collection.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityCollection}"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetEntityCollectionsByProductKey(Guid productKey, bool isFilter = false);

        /// <summary>
        /// Gets a collection of <see cref="IEntityCollection"/> by an invoice key.
        /// </summary>
        /// <param name="invoiceKey">
        /// The invoice key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityCollection}"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetEntityCollectionsByInvoiceKey(Guid invoiceKey);

        /// <summary>
        ///  Gets a collection of <see cref="IEntityCollection"/> by a customer key.
        /// </summary>
        /// <param name="customerKey">
        /// The customer key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityCollection}"/>.
        /// </returns>
        IEnumerable<IEntityCollection> GetEntityCollectionsByCustomerKey(Guid customerKey);

        /// <summary>
        /// Gets a <see cref="PagedCollection"/> result of <see cref="IEntityCollection"/>.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="itemsPerPage">
        /// The items per page.
        /// </param>
        /// <param name="orderExpression">
        /// The order expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="PagedCollection"/>.
        /// </returns>
        PagedCollection<IEntityCollection> GetPage(IQuery<IEntityCollection> query, long page, long itemsPerPage, string orderExpression, Direction direction = Direction.Descending);

        /// <summary>
        /// Gets an <see cref="IEntityFilterGroup"/> by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityFilterGroup"/>.
        /// </returns>
        IEntityFilterGroup GetEntityFilterGroup(Guid key);

        /// <summary>
        /// Gets a collection of <see cref="IEntityFilterGroup"/> by the managing provider key.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityCollection}"/>.
        /// </returns>
        IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsByProviderKeys(Guid[] keys);

        /// <summary>
        /// Gets a collection of <see cref="IEntityFilterGroup"/> that contain a product based.
        /// </summary>
        /// <param name="keys">
        /// The collection keys that represent the filtering context.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityFilterGroup}"/>.
        /// </returns>
        IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsContainingProduct(Guid[] keys, Guid productKey);

        /// <summary>
        /// Gets a collection of <see cref="IEntityFilterGroup"/> that do not contain a product based.
        /// </summary>
        /// <param name="keys">
        /// The collection keys that represent the filtering context.
        /// </param>
        /// <param name="productKey">
        /// The product key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IEntityFilterGroup}"/>.
        /// </returns>
        IEnumerable<IEntityFilterGroup> GetEntityFilterGroupsNotContainingProduct(Guid[] keys, Guid productKey);
    }
}