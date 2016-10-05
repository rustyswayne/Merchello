namespace Merchello.Core.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;

    using Merchello.Core.Models;
    using Merchello.Core.Models.EntityBase;

    /// <summary>
    /// Represents a repository responsible for <see cref="ILineItem"/> entities.
    /// </summary>
    /// <typeparam name="TLineItem">
    /// The type of the line item
    /// </typeparam>
    public interface ILineItemRepository<TLineItem>
        where TLineItem : ILineItem
    {
        /// <summary>
        /// Gets the line items by a line item container key.
        /// </summary>
        /// <param name="containerKey">
        /// The container key.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{ILineItem}"/>.
        /// </returns>
        IEnumerable<TLineItem> GetByContainerKey(Guid containerKey);

        /// <summary>
        /// Gets a <see cref="LineItemCollection"/>.
        /// </summary>
        /// <param name="containerKey">
        /// The container key.
        /// </param>
        /// <returns>
        /// The <see cref="LineItemCollection"/>.
        /// </returns>
        LineItemCollection GetLineItemCollection(Guid containerKey);

        /// <summary>
        /// Saves a <see cref="LineItemCollection"/>.
        /// </summary>
        /// <param name="items">
        /// The <see cref="LineItemCollection"/>.
        /// </param>
        /// <param name="containerKey">
        /// The container key.
        /// </param>
        void SaveLineItem(LineItemCollection items, Guid containerKey);

        /// <summary>
        /// Saves an individual line item.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        void SaveLineItem(TLineItem item);
    }
}